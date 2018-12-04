using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Biob.Services.Data.Repositories;
using AutoMapper;
using Biob.Services.Data.DtoModels;
using Biob.Web.Helpers;
using Biob.Data.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;
using Biob.Services.Data.Helpers;
using System.Dynamic;
using System.Linq;

namespace Biob.Web.Controllers
{
    [Route("api/v1/halls/{hallId}/seats")]
    [ApiController]
    public class SeatController : ControllerBase
    {
        private readonly ISeatRepository _seatRepository;
        private readonly ILogger<SeatController> _logger;
        private readonly IHallRepository _hallRepository;
        private readonly IUrlHelper _urlHelper;
       

        public SeatController(ISeatRepository seatRepository, ILogger<SeatController> logger,
                              IHallRepository hallRepository, IUrlHelper urlHelper)
        {
            _seatRepository = seatRepository;
            _logger = logger;
            _hallRepository = hallRepository;
            _urlHelper = urlHelper;
        }


        [HttpGet(Name = "GetSeats")]
        public async Task<IActionResult> GetAllSeats([FromRoute] Guid hallId, [FromQuery]RequestParameters requestParameters, [FromHeader(Name = "Accept")] string mediaType)
        {

            //var hallExists = _hallRepository.GetHallAsync(hallId);

            //if (hallExists == null)
            //{
            //    return BadRequest();
            //}

            if (!string.IsNullOrWhiteSpace(requestParameters.Fields))
            {
                return BadRequest();
            }

            var seatsPagedList = await _seatRepository.GetAllSeatsByHallIdAsync(hallId, requestParameters.PageNumber, requestParameters.PageSize);

            var seats = Mapper.Map<IEnumerable<SeatDto>>(seatsPagedList);

            if (mediaType == "application/vnd.biob.json+hateoas")
            {
                return Ok(CreateHateoasResponse(seatsPagedList, requestParameters));
            }
            else
            {
                var previousPageLink = seatsPagedList.HasPrevious ? CreateUrlForResource(requestParameters, PageType.PreviousPage) : null;
                var nextPageLink = seatsPagedList.HasNext ? CreateUrlForResource(requestParameters, PageType.NextPage) : null;
                var paginationMetadata = new PaginationMetadata()
                {
                    TotalCount = seatsPagedList.TotalCount,
                    PageSize = seatsPagedList.PageSize,
                    CurrentPage = seatsPagedList.CurrentPage,
                    TotalPages = seatsPagedList.TotalPages,
                    PreviousPageLink = previousPageLink,
                    NextPageLink = nextPageLink
                };

                Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));

                if (requestParameters.IncludeMetadata)
                {
                    var seatsWithMetadata = new EntityWithPaginationMetadataDto<SeatDto>(paginationMetadata, seats);
                    return Ok(seatsWithMetadata);
                }

                return Ok(seats);
            }
        }

        [HttpGet("{seatId}", Name = "GetSeat")]
        public async Task<IActionResult> GetOneSeat([FromRoute] Guid seatId, [FromHeader(Name = "Accept")] string mediaType)
        {
            var foundSeat = await _seatRepository.GetSeatAsync(seatId);

            if (foundSeat == null)
            {
                return NotFound();
            }

            var seatToReturn = Mapper.Map<SeatDto>(foundSeat);

            if (mediaType == "application/vnd.biob.json+hateoas")
            {
                var links = CreateLinksForSeats(seatId, null);

                var linkedSeat = seatToReturn.ShapeData(null) as IDictionary<string, object>;
                linkedSeat.Add("links", links);
                return Ok(linkedSeat);
            }
            else
            {
                return Ok(seatToReturn.ShapeData(null));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateSeat([FromRoute] Guid hallId, [FromBody] SeatToCreateDto seatToCreate, [FromHeader(Name = "Accept")] string mediaType)
        {
            if (seatToCreate == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return new ProccessingEntityObjectResultErrors(ModelState);
            }
            
            var seatToAdd = Mapper.Map<Seat>(seatToCreate);
            seatToAdd.Id = Guid.NewGuid();

            _seatRepository.AddSeat(hallId, seatToAdd);

            if (!await _seatRepository.SaveChangesAsync())
            {
                _logger.LogError("Saving changes to database while creating a seat failed");
            }

            var seat = Mapper.Map<Seat>(seatToAdd);

            if (mediaType == "application/vnd.biob.json+hateoas")
            {
                var links = CreateLinksForSeats(seat.Id, null);

                var linkedSeat = seat.ShapeData(null) as IDictionary<string, object>;
                linkedSeat.Add("links", links);

                return CreatedAtRoute("GetSeat", new { hallId, seatId = seatToAdd.Id }, linkedSeat);
            }
            else
            {
                return CreatedAtRoute("GetSeat", new { hallId, seatId = seatToAdd.Id }, seat);
            }
        }

        [HttpPut("{seatId}", Name = "UpdateSeat")]
        public async Task<IActionResult> UpdateSeatById([FromRoute] Guid hallId ,[FromRoute] Guid seatId, [FromBody] SeatToUpdateDto seatToUpdate, [FromHeader(Name = "Accept")] string mediaType)
        {
            if (seatToUpdate == null)
            {
                return BadRequest();
            }

            var seatFromDb = await _seatRepository.GetSeatAsync(seatId);

            // upserting if movie does not already exist
            if (seatFromDb == null)
            {
                var seatEntity = Mapper.Map<Seat>(seatToUpdate);
                seatEntity.Id = seatId;
                _seatRepository.AddSeat(hallId, seatEntity);

                if (!await _seatRepository.SaveChangesAsync())
                {
                    _logger.LogError($"Upserting seat: {seatId} failed on save");
                }

                var seatToReturn = Mapper.Map<SeatDto>(seatEntity);

                if (mediaType == "application/vnd.biob.json+hateoas")
                {
                    var links = CreateLinksForSeats(seatToReturn.Id, null);

                    var linkedSeat = seatToReturn.ShapeData(null) as IDictionary<string, object>;
                    linkedSeat.Add("links", links);

                    return CreatedAtRoute("GetSeat", new { hallId, seatId = seatToReturn.Id }, linkedSeat);
                }
                else
                {
                    return CreatedAtRoute("GetSeat", new { hallId, seatId = seatToReturn.Id }, seatToReturn);
                }
            }

            Mapper.Map(seatToUpdate, seatFromDb);

            _seatRepository.UpdateSeat(seatFromDb);

            if (!await _seatRepository.SaveChangesAsync())
            {
                _logger.LogError($"Updating seat: {seatId} failed on save");
            }

            return NoContent();

        }

        [HttpPatch("{seatId}", Name = "PartiallyUpdateSeat")]
        public async Task<IActionResult> PartiuallyUpdateSeatById([FromRoute] Guid hallId, [FromRoute] Guid seatId, JsonPatchDocument<SeatToUpdateDto> patchDoc, [FromHeader(Name = "Accept")] string mediaType)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var seatFromDb = await _seatRepository.GetSeatAsync(seatId);

            //  upserting if movie does not already exist
            if (seatFromDb == null)
            {
                var seatToCreate = new SeatToUpdateDto();
                patchDoc.ApplyTo(seatToCreate, ModelState);

                if (!ModelState.IsValid)
                {
                    new ProccessingEntityObjectResultErrors(ModelState);
                }

                var seatToAddToDb = Mapper.Map<Seat>(seatToCreate);
                seatToAddToDb.Id = seatId;
                _seatRepository.AddSeat(hallId, seatToAddToDb);

                if (!await _seatRepository.SaveChangesAsync())
                {
                    _logger.LogError($"Upserting seat: {seatId} failed on save");
                }

                var seatToReturn = Mapper.Map<SeatDto>(seatToAddToDb);

                if (mediaType == "application/vnd.biob.json+hateoas")
                {
                    var links = CreateLinksForSeats(seatToReturn.Id, null);

                    var linkedSeat = seatToReturn.ShapeData(null) as IDictionary<string, object>;
                    linkedSeat.Add("links", links);

                    return CreatedAtRoute("GetSeat", new { hallId, seatId = seatToReturn.Id }, linkedSeat);
                }
                else
                {
                    return CreatedAtRoute("GetSeat", new { hallId, seatId = seatToReturn.Id }, seatToReturn);
                }
            }

            var seatToPatch = Mapper.Map<SeatToUpdateDto>(seatFromDb);

            patchDoc.ApplyTo(seatToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                new ProccessingEntityObjectResultErrors(ModelState);
            }

            Mapper.Map(seatToPatch, seatFromDb);
            _seatRepository.UpdateSeat(seatFromDb);

            if (!await _seatRepository.SaveChangesAsync())
            {
                _logger.LogError($"Partially updating seat: {seatId} failed on save");
            }

            return NoContent();
        }

        [HttpDelete("{seatId}", Name = "DeleteSeat")]
        public async Task<IActionResult> DeleteSeatById([FromRoute]Guid seatId)
        {
            var seatToDelete = await _seatRepository.GetSeatAsync(seatId);

            if (seatToDelete == null)
            {
                return NotFound();
            }

            _seatRepository.DeleteSeat(seatToDelete);

            if (!await _seatRepository.SaveChangesAsync())
            {
                _logger.LogError($"Deleting seat: {seatId} failed on save");
            }

            return NoContent();
        }

        [HttpOptions]
        public IActionResult GetSeatsOptions()
        {
            Response.Headers.Add("Allow", "GET,POST,OPTIONS");
            return Ok();
        }

        [HttpOptions("{seatId}")]
        public IActionResult GetSeatOptions()
        {
            Response.Headers.Add("Allow", "GET,PATCH,PUT,OPTIONS");
            return Ok();
        }

        private ExpandoObject CreateHateoasResponse(PagedList<Seat> seatsPagedList, RequestParameters requestParameters)
        {
            var seats = Mapper.Map<IEnumerable<SeatDto>>(seatsPagedList);

            var paginationMetadataWithLinks = new
            {
                seatsPagedList.TotalCount,
                seatsPagedList.PageSize,
                seatsPagedList.CurrentPage,
                seatsPagedList.TotalPages
            };

            Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadataWithLinks));

            var links = CreateLinksForSeats(requestParameters, seatsPagedList.HasNext, seatsPagedList.HasPrevious);

            var shapedSeats = seats.ShapeData(requestParameters.Fields);

            var shapedSeatsWithLinks = shapedSeats.Select(seat =>
            {
                var seatDictionary = seat as IDictionary<string, object>;
                var seatLinks = CreateLinksForSeats((Guid)seatDictionary["Id"], requestParameters.Fields);

                seatDictionary.Add("links", seatLinks);

                return seatDictionary;
            });
            if (requestParameters.IncludeMetadata)
            {
                var seatsWithMetadata = new ExpandoObject();
                ((IDictionary<string, object>)seatsWithMetadata).Add("Metadata", paginationMetadataWithLinks);
                ((IDictionary<string, object>)seatsWithMetadata).Add("seats", shapedSeatsWithLinks);
                ((IDictionary<string, object>)seatsWithMetadata).Add("links", links);
                return seatsWithMetadata;
            }
            else
            {
                var linkedCollection = new ExpandoObject();
                ((IDictionary<string, object>)linkedCollection).Add("seats", shapedSeatsWithLinks);
                ((IDictionary<string, object>)linkedCollection).Add("links", links);
                return linkedCollection;
            }
        }

        private IEnumerable<LinkDto> CreateLinksForSeats(Guid id, string fields)
        {
            var links = new List<LinkDto>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                links.Add(new LinkDto(_urlHelper.Link("GetSeat", new { seatId = id }), "self", "GET"));
            }
            else
            {
                links.Add(new LinkDto(_urlHelper.Link("GetSeat", new { seatId = id, fields }), "self", "GET"));
            }
            links.Add(new LinkDto(_urlHelper.Link("DeleteSeat", new { seatId = id }), "delete_seat", "DELETE"));
            {
                links.Add(new LinkDto(_urlHelper.Link("UpdateSeat", new { seatId = id }), "update_seat", "PUT"));
            }
            links.Add(new LinkDto(_urlHelper.Link("PartiallyUpdateSeat", new { seatId = id }), "partially_update_seat", "PATCH"));
            return links;
        }

        private IEnumerable<LinkDto> CreateLinksForSeats(RequestParameters requestParameters, bool hasNext, bool hasPrevious)
        {
            var links = new List<LinkDto>
            {
                new LinkDto(CreateUrlForResource(requestParameters, PageType.Current), "self", "GET")
            };

            if (hasNext)
            {
                new LinkDto(CreateUrlForResource(requestParameters, PageType.NextPage), "self", "GET");
            }

            if (hasPrevious)
            {
                new LinkDto(CreateUrlForResource(requestParameters, PageType.PreviousPage), "self", "GET");
            }

            return links;
        }

        private string CreateUrlForResource(RequestParameters requestParameters, PageType pageType)
        {
            switch (pageType)
            {
                case PageType.PreviousPage:
                    return _urlHelper.Link("GetSeats", new
                    {
                        orderBy = requestParameters.OrderBy,
                        searchQuery = requestParameters.SearchQuery,
                        pageNumber = requestParameters.PageNumber - 1,
                        pageSize = requestParameters.PageSize

                    });
                case PageType.NextPage:
                    return _urlHelper.Link("GetSeats", new
                    {
                        orderBy = requestParameters.OrderBy,
                        searchQuery = requestParameters.SearchQuery,
                        pageNumber = requestParameters.PageNumber + 1,
                        pageSize = requestParameters.PageSize
                    });
                default:
                    return _urlHelper.Link("GetSeats", new
                    {
                        orderBy = requestParameters.OrderBy,
                        searchQuery = requestParameters.SearchQuery,
                        pageNumber = requestParameters.PageNumber,
                        pageSize = requestParameters.PageSize
                    });
            }
        }
    }
}
