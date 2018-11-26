using AutoMapper;
using Biob.Data.Models;
using Biob.Services.Data.DtoModels;
using Biob.Services.Data.Helpers;
using Biob.Services.Data.Repositories;
using Biob.Services.Web.PropertyMapping;
using Biob.Web.Helpers;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;

namespace Biob.Web.Controllers
{
    [Route("/api/v1/movies/{movieId}/showtimes")]
    [ApiController]
    public class ShowtimeController : ControllerBase
    {
        private readonly ILogger<ShowtimeController> _logger;
        private readonly IShowtimeRepository _showtimeRepository;
        private readonly ITypeHelperService _typeHelperService;
        private readonly IUrlHelper _urlHelper;
        private readonly IPropertyMappingService _propertyMappingService;

        public ShowtimeController(IShowtimeRepository showtimeRepository, IPropertyMappingService propertyMappingService, 
                                    ITypeHelperService typeHelperService, IUrlHelper urlHelper, ILogger<ShowtimeController> logger)
        {
            _logger = logger;
            _showtimeRepository = showtimeRepository;
            _typeHelperService = typeHelperService;
            _urlHelper = urlHelper;
            _propertyMappingService = propertyMappingService;

            _propertyMappingService.AddPropertyMapping<ShowtimeDto, Showtime>(new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                { "Id", new PropertyMappingValue(new List<string>() { "Id" })},
                { "MovieId", new PropertyMappingValue(new List<string>() { "MovieId" })},
                { "HallId", new PropertyMappingValue(new List<string>() { "HallId" })},
                { "TimeOfPlaying", new PropertyMappingValue(new List<string>() { "TimeOfPlaying" })},
                { "ThreeDee", new PropertyMappingValue(new List<string>() { "ThreeDee" })}
            });
        }

        [HttpGet(Name = "GetShowtimes")]
        public async Task<IActionResult> GetAllShowtimes([FromQuery]RequestParameters requestParameters)
        {

            if (string.IsNullOrWhiteSpace(requestParameters.OrderBy))
            {
                requestParameters.OrderBy = "Id"; // MovieId?
            }


            if (!_propertyMappingService.ValidMappingExistsFor<ShowtimeDto, Showtime>(requestParameters.Fields))
            {
                return BadRequest();
            }

            if (!_typeHelperService.TypeHasProperties<ShowtimeDto>(requestParameters.Fields))
            {
                return BadRequest();
            }

            var showtimesPagedList = await _showtimeRepository.GetAllShowtimesAsync(requestParameters.OrderBy, requestParameters.PageNumber, requestParameters.PageSize);

            var previousPageLink = showtimesPagedList.HasPrevious ? CreateUrlForResource(requestParameters, PageType.PreviousPage) : null;
            var nextPageLink = showtimesPagedList.HasNext ? CreateUrlForResource(requestParameters, PageType.NextPage) : null;

            var paginationMetadata = new PaginationMetadata()
            {
                TotalCount = showtimesPagedList.TotalCount,
                PageSize = showtimesPagedList.PageSize,
                CurrentPage = showtimesPagedList.CurrentPage,
                TotalPages = showtimesPagedList.TotalPages,
                PreviousPageLink = previousPageLink,
                NextPageLink = nextPageLink
            };

            Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));

            var showtimes = Mapper.Map<IEnumerable<ShowtimeDto>>(showtimesPagedList);

            if (requestParameters.IncludeMetadata)
            {
                var showtimesWithMetadata = new EntityWithPaginationMetadataDto<ShowtimeDto>(paginationMetadata, showtimes);
                return Ok(showtimesWithMetadata);
            }

            return Ok(showtimes);
        }

        [HttpGet("{showtimeId}", Name = "GetShowtime")]
        public async Task<IActionResult> GetOneShowtime([FromRoute]Guid showtimeId, [FromRoute]Guid movieId)
        {
            if (showtimeId == Guid.Empty)
            {
                return BadRequest();
            }

            if (movieId == Guid.Empty)
            {
                return BadRequest();
            }

            if (!await _showtimeRepository.MovieExists(movieId))
            {
                return NotFound();
            }

            var foundShowtime = await _showtimeRepository.GetShowtimeAsync(showtimeId, movieId);

            if (foundShowtime == null)
            {
                return NotFound();
            }

            var showtimeToReturn = Mapper.Map<ShowtimeDto>(foundShowtime);
            return Ok(showtimeToReturn);
        }

        [HttpPost]
        public async Task<IActionResult> CreateShowtime([FromRoute]Guid movieId, [FromBody] ShowtimeToCreateDto showtimeToCreate)
        {
            if (showtimeToCreate == null)
            {
                return BadRequest();
            }

            if (showtimeToCreate.Id == null)
            {
                showtimeToCreate.Id = Guid.NewGuid();
            }

            if (!ModelState.IsValid)
            {
                return new ProccessingEntityObjectResultErrors(ModelState);
            }

            var showtimeToAdd = Mapper.Map<Showtime>(showtimeToCreate);
            _showtimeRepository.AddShowtime(movieId, showtimeToAdd);

            if (!await _showtimeRepository.SaveChangesAsync())
            {
                _logger.LogError("Saving changes to database while creating a showtime failed");
            }

            return CreatedAtRoute("GetShowtime", new { movieId, showtimeId = showtimeToAdd.Id }, showtimeToAdd);
        }

        [HttpPut("{showtimeId}")]
        public async Task<IActionResult> UpdateShowtime([FromRoute]Guid movieId, [FromRoute] Guid showtimeId, [FromBody] ShowtimeToUpdateDto showtimeToUpdate)
        {
            if (movieId == Guid.Empty)
            {
                return BadRequest();
            }

            if (showtimeId == Guid.Empty)
            {
                return BadRequest();
            }

            if (!await _showtimeRepository.MovieExists(movieId))
            {
                return NotFound();
            }

            if (showtimeToUpdate == null)
            {
                return BadRequest();
            }

            var showtimeFromDb = await _showtimeRepository.GetShowtimeAsync(showtimeId, movieId);

            if (showtimeFromDb == null)
            {
                var showtimeEntity = Mapper.Map<Showtime>(showtimeToUpdate);
                showtimeEntity.Id = showtimeId;
                _showtimeRepository.AddShowtime(movieId, showtimeEntity);

                if (!await _showtimeRepository.SaveChangesAsync())
                {
                    _logger.LogError($"Upserting showtime: {showtimeId} failed on save");
                }

                var showtimeToReturn = Mapper.Map<ShowtimeDto>(showtimeEntity);

                return CreatedAtRoute("GetShowtime", new { movieId, showtimeId = showtimeToReturn.Id }, showtimeToReturn);
            }

            Mapper.Map(showtimeToUpdate, showtimeFromDb);

            _showtimeRepository.UpdateShowtime(showtimeFromDb);

            if (!await _showtimeRepository.SaveChangesAsync())
            {
                _logger.LogError($"Updating showtime: {showtimeId} failed on save");
            }

            return NoContent();
        }

        [HttpPatch("{showtimeId}")]
        public async Task<IActionResult> PartiuallyUpdateShowtime([FromRoute]Guid movieId, [FromRoute] Guid showtimeId, JsonPatchDocument<ShowtimeToUpdateDto> patchDoc)
        {

            if (movieId == Guid.Empty)
            {
                return BadRequest();
            }

            if (showtimeId == Guid.Empty)
            {
                return BadRequest();
            }

            if (!await _showtimeRepository.MovieExists(movieId))
            {
                return NotFound();
            }

            if (patchDoc == null)
            {
                return BadRequest();
            }

            var showtimeFromDb = await _showtimeRepository.GetShowtimeAsync(showtimeId, movieId);

            if (showtimeFromDb == null)
            {
                var showtimeToCreate = new ShowtimeToUpdateDto();

                patchDoc.ApplyTo(showtimeToCreate, ModelState);

                if (!ModelState.IsValid)
                {
                    new ProccessingEntityObjectResultErrors(ModelState);
                }

                var showtimeToAddToDb = Mapper.Map<Showtime>(showtimeToCreate);
                showtimeToAddToDb.Id = showtimeId;
                _showtimeRepository.AddShowtime(movieId, showtimeToAddToDb);

                if (!await _showtimeRepository.SaveChangesAsync())
                {
                    _logger.LogError($"Upserting showtime: {showtimeId} failed on save");
                }

                var showtimeToReturn = Mapper.Map<ShowtimeDto>(showtimeToAddToDb);

                return CreatedAtRoute("GetShowtime", new { movieId, showtimeId = showtimeToReturn.Id }, showtimeToReturn);
            }

            var showtimeToPatch = Mapper.Map<ShowtimeToUpdateDto>(showtimeFromDb);

            patchDoc.ApplyTo(showtimeToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                new ProccessingEntityObjectResultErrors(ModelState);
            }

            Mapper.Map(showtimeToPatch, showtimeFromDb);

            _showtimeRepository.UpdateShowtime(showtimeFromDb);

            if (!await _showtimeRepository.SaveChangesAsync())
            {
                _logger.LogError($"Partially updating {showtimeId} failed");
            }

            return NoContent();
        }

        [HttpDelete("{showtimeId}")]
        public async Task<IActionResult> DeleteShowtime([FromRoute] Guid showtimeId, [FromRoute]Guid movieId)
        {
            if (movieId == Guid.Empty)
            {
                return BadRequest();
            }

            if (showtimeId == Guid.Empty)
            {
                return BadRequest();
            }

            if (!await _showtimeRepository.MovieExists(movieId))
            {
                return NotFound();
            }


            var showtimeToDelete = await _showtimeRepository.GetShowtimeAsync(showtimeId, movieId);

            if (showtimeToDelete == null)
            {
                return NotFound();
            }

            _showtimeRepository.DeleteShowtime(showtimeToDelete);

            if (!await _showtimeRepository.SaveChangesAsync())
            {
                _logger.LogError($"Deleting {showtimeId} failed");
            }

            return NoContent();
        }

        private string CreateUrlForResource(RequestParameters requestParameters, PageType pageType)
        {
            switch (pageType)
            {
                case PageType.PreviousPage:
                    return _urlHelper.Link("GetShowtimes", new
                    {
                        orderBy = requestParameters.OrderBy,
                        searchQuery = requestParameters.SearchQuery,
                        pageNumber = requestParameters.PageNumber - 1,
                        pageSize = requestParameters.PageSize

                    });
                case PageType.NextPage:
                    return _urlHelper.Link("GetShowtimes", new
                    {
                        orderBy = requestParameters.OrderBy,
                        searchQuery = requestParameters.SearchQuery,
                        pageNumber = requestParameters.PageNumber + 1,
                        pageSize = requestParameters.PageSize
                    });
                default:
                    return _urlHelper.Link("GetShowtimes", new
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
