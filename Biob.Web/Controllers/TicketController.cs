using Biob.Data.Models;
using Biob.Services.Data.Repositories;
using Biob.Services.Web.PropertyMapping;
using Biob.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biob.Services.Data.DtoModels;
using Microsoft.AspNetCore.JsonPatch;
using AutoMapper;
using Biob.Services.Data.Helpers;
using System.Dynamic;
using Microsoft.Extensions.Logging;

namespace Biob.Web.Controllers
{
    [Route("/api/v1/movies/{movieId}/showtimes/{showtimeId}/tickets")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly ITypeHelperService _typeHelperService;
        private readonly IUrlHelper _urlHelper;
        private readonly ILogger<TicketController> _logger;

        public TicketController(ITicketRepository ticketRepository, IPropertyMappingService propertyMappingService, 
                                ITypeHelperService typeHelperService, IUrlHelper urlHelper, ILogger<TicketController> logger)
        {
            _ticketRepository = ticketRepository;
            _propertyMappingService = propertyMappingService;
            _typeHelperService = typeHelperService;
            _urlHelper = urlHelper;
            _logger = logger;
            //Note: In TicketRepository, ApplySort is temprarily outcommented as it caused an error somehow. TODO: Could be fixed at some point
            _propertyMappingService.AddPropertyMapping<TicketDto, Ticket>(new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                { "Id", new PropertyMappingValue(new List<string>() { "Id" })},
                { "CustomerId", new PropertyMappingValue(new List<string>() { "CustomerId" })},
                { "ShowtimeId", new PropertyMappingValue(new List<string>() { "ShowtimeId" })},
                { "SeatId", new PropertyMappingValue(new List<string>() { "SeatId" })},
                { "Paid", new PropertyMappingValue(new List<string>() { "Paid" })},
                { "Price", new PropertyMappingValue(new List<string>() { "Price" })},
            });
        }

        [HttpGet(Name = "GetTickets")]
        public async Task<IActionResult> GetAllTickets([FromRoute] Guid showtimeId,[FromQuery]RequestParameters requestParameters)
        {
            
            if (string.IsNullOrWhiteSpace(requestParameters.OrderBy))
            {
                requestParameters.OrderBy = "Price";
            }

            if (!_propertyMappingService.ValidMappingExistsFor<TicketDto, Ticket>(requestParameters.Fields))
            {
                return BadRequest();
            }

            if (!_typeHelperService.TypeHasProperties<TicketDto>(requestParameters.Fields))
            {
                return BadRequest();
            }

            var ticketsPagedList = await _ticketRepository.GetAllTicketsAsync(showtimeId,
                                                                    requestParameters.OrderBy,
                                                                    requestParameters.SearchQuery,
                                                                    requestParameters.PageNumber, requestParameters.PageSize);

            var tickets = Mapper.Map<IEnumerable<TicketDto>>(ticketsPagedList);

            //if (mediaType == "application/vnd.biob.json+hateoas")
            //{
            //    return Ok(CreateHateoasResponse(ticketsPagedList, requestParameters));
            //}
            //else
            //{
            var previousPageLink = ticketsPagedList.HasPrevious ? CreateUrlForResource(requestParameters, PageType.PreviousPage) : null;
            var nextPageLink = ticketsPagedList.HasNext ? CreateUrlForResource(requestParameters, PageType.NextPage) : null;
            var paginationMetadata = new PaginationMetadata()
            {
                TotalCount = ticketsPagedList.TotalCount,
                PageSize = ticketsPagedList.PageSize,
                CurrentPage = ticketsPagedList.CurrentPage,
                TotalPages = ticketsPagedList.TotalPages,
                PreviousPageLink = previousPageLink,
                NextPageLink = nextPageLink
            };

            Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));

            if (requestParameters.IncludeMetadata)
            {
                var shapedTickets = tickets.ShapeData(requestParameters.Fields);
                var ticketsWithMetadata = new EntityWithPaginationMetadataDto<ExpandoObject>(paginationMetadata, shapedTickets);
                return Ok(ticketsWithMetadata);
            }

            return Ok(tickets.ShapeData(requestParameters.Fields));
            //}
        }


        [HttpGet("{ticketId}", Name = "GetTicket")]
        public async Task<IActionResult> GetOneTicket([FromRoute]Guid ticketId, [FromQuery] string fields)
        {
            if (ticketId == Guid.Empty)
            {
                return BadRequest();
            }

            var foundTicket = await _ticketRepository.GetTicketAsync(ticketId);

            if (foundTicket == null)
            {
                return NotFound();
            }

            return Ok(foundTicket.ShapeData(fields));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket([FromBody] TicketToCreateDto ticketToCreate)
        {
            if (ticketToCreate == null)
            {
                return BadRequest();
            }

            if (ticketToCreate.Id == null)
            {
                ticketToCreate.Id = Guid.NewGuid();
            }

            if (!ModelState.IsValid)
            {
                return new ProccessingEntityObjectResultErrors(ModelState);
            }


            var ticketToAdd = Mapper.Map<Ticket>(ticketToCreate);
            _ticketRepository.AddTicket(ticketToAdd);

            if (!await _ticketRepository.SaveChangesAsync())
            {
                _logger.LogError("Saving changes to database while creating a showtime failed");
            }

            return CreatedAtRoute("GetTicket", new { ticketId = ticketToAdd.Id }, ticketToAdd);
        }

        [HttpPut("{ticketId}")]
        public async Task<IActionResult> UpdateTicket([FromRoute] Guid ticketId, [FromBody] TicketToUpdateDto ticketToUpdate)
        {
            if (ticketToUpdate == null)
            {
                return BadRequest();
            }

            var ticketFromDb = await _ticketRepository.GetTicketAsync(ticketId);

            //  upserting if ticket does not already exist
            if (ticketFromDb == null)
            {
                var ticketEntity = Mapper.Map<Ticket>(ticketToUpdate);
                ticketEntity.Id = ticketId;
                _ticketRepository.AddTicket(ticketEntity);

                if (!await _ticketRepository.SaveChangesAsync())
                {
                    _logger.LogError("Failed to upsert a new ticket");
                }

                var ticketToReturn = Mapper.Map<TicketDto>(ticketEntity);

                return CreatedAtRoute("GetTicket", new { ticketId = ticketToReturn.Id }, ticketToReturn);
            }

            Mapper.Map(ticketToUpdate, ticketFromDb);

            _ticketRepository.UpdateTicket(ticketFromDb);

            if (!await _ticketRepository.SaveChangesAsync())
            {
                _logger.LogError("Failed to update a ticket");
            }

            return NoContent();
        }

        [HttpPatch("{ticketId}")]
        public async Task<IActionResult> PartiallyUpdateTicket([FromRoute] Guid ticketId, JsonPatchDocument<TicketToUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var ticketFromDb = await _ticketRepository.GetTicketAsync(ticketId);

            //  upserting if ticket does not already exist
            //  TODO:   research if upserting is neccesary in patching
            if (ticketFromDb == null)
            {
                var ticketToCreate = new TicketToUpdateDto();

                patchDoc.ApplyTo(ticketToCreate, ModelState);

                if (!ModelState.IsValid)
                {
                    new ProccessingEntityObjectResultErrors(ModelState);
                }

                var ticketToAddToDb = Mapper.Map<Ticket>(ticketToCreate);
                ticketToAddToDb.Id = ticketId;

                _ticketRepository.AddTicket(ticketToAddToDb);

                if (!await _ticketRepository.SaveChangesAsync())
                {
                    _logger.LogError("Upserting ticket failed");
                }

                var ticketToReturn = Mapper.Map<TicketDto>(ticketToAddToDb);

                return CreatedAtRoute("GetTicket", new { ticketId = ticketToReturn.Id }, ticketToReturn);
            }

            var ticketToPatch = Mapper.Map<TicketToUpdateDto>(ticketFromDb);

            patchDoc.ApplyTo(ticketToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                new ProccessingEntityObjectResultErrors(ModelState);
            }

            Mapper.Map(ticketToPatch, ticketFromDb);

            _ticketRepository.UpdateTicket(ticketFromDb);

            if (!await _ticketRepository.SaveChangesAsync())
            {
                _logger.LogError("partially updating ticket failed");
            }

            return NoContent();
        }

        private string CreateUrlForResource(RequestParameters requestParameters, PageType pageType)
        {
            switch (pageType)
            {
                case PageType.PreviousPage:
                    return _urlHelper.Link("GetTickets", new
                    {
                        fields = requestParameters.Fields,
                        orderBy = requestParameters.OrderBy,
                        searchQuery = requestParameters.SearchQuery,
                        pageNumber = requestParameters.PageNumber - 1,
                        pageSize = requestParameters.PageSize

                    });
                case PageType.NextPage:
                    return _urlHelper.Link("GetTickets", new
                    {
                        fields = requestParameters.Fields,
                        orderBy = requestParameters.OrderBy,
                        searchQuery = requestParameters.SearchQuery,
                        pageNumber = requestParameters.PageNumber + 1,
                        pageSize = requestParameters.PageSize
                    });
                default:
                    return _urlHelper.Link("GetTickets", new
                    {
                        fields = requestParameters.Fields,
                        orderBy = requestParameters.OrderBy,
                        searchQuery = requestParameters.SearchQuery,
                        pageNumber = requestParameters.PageNumber,
                        pageSize = requestParameters.PageSize
                    });
            }
        }
    }
}

