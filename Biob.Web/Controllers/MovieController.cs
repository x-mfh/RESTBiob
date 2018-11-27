using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Biob.Services.Data.Repositories;
using AutoMapper;
using Biob.Services.Data.DtoModels;
using Biob.Data.Models;
using Biob.Web.Helpers;
using Microsoft.AspNetCore.JsonPatch;
using Biob.Web.Filters;
using Biob.Services.Web.PropertyMapping;
using System.Collections.Generic;
using Biob.Services.Data.Helpers;
using System.Dynamic;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Biob.Web.Controllers
{
    [Route("/api/v1/movies")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly ITypeHelperService _typeHelperService;
        private readonly IUrlHelper _urlHelper;
        private readonly ILogger<MovieController> _logger;

        public MovieController(IMovieRepository movieRepository, IPropertyMappingService propertyMappingService,
                                ITypeHelperService typeHelperService, IUrlHelper urlHelper, ILogger<MovieController> logger)
        {
            _movieRepository = movieRepository;
            _propertyMappingService = propertyMappingService;
            _typeHelperService = typeHelperService;
            _urlHelper = urlHelper;
            _logger = logger;
            _propertyMappingService.AddPropertyMapping<MovieDto, Movie>(new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                { "Id", new PropertyMappingValue(new List<string>() { "Id" })},
                { "Title", new PropertyMappingValue(new List<string>() { "Title" })},
                { "Description", new PropertyMappingValue(new List<string>() { "Description" })},
                { "Length", new PropertyMappingValue(new List<string>() { "LengthInSeconds" })},
                { "Poster", new PropertyMappingValue(new List<string>() { "Poster" })},
                { "Producer", new PropertyMappingValue(new List<string>() { "Producer" })},
                { "Actors", new PropertyMappingValue(new List<string>() { "Actors" })},
                { "Genre", new PropertyMappingValue(new List<string>() { "Genre" })},
                { "Released", new PropertyMappingValue(new List<string>() { "Released" })},
                { "ThreeDee", new PropertyMappingValue(new List<string>() { "ThreeDee" })},
                { "AgeRestriction", new PropertyMappingValue(new List<string>() { "AgeRestriction" })},
            });
        }

        [HttpGet(Name = "GetMovies")]
        public async Task<IActionResult> GetAllMovies([FromQuery]RequestParameters requestParameters,
                                                      [FromHeader(Name = "Accept")] string mediaType)
        {

            if (string.IsNullOrWhiteSpace(requestParameters.OrderBy))
            {
                requestParameters.OrderBy = "Title";
            }

            if (!_propertyMappingService.ValidMappingExistsFor<MovieDto, Movie>(requestParameters.Fields))
            {
                return BadRequest();
            }

            if (!_typeHelperService.TypeHasProperties<MovieDto>(requestParameters.Fields))
            {
                return BadRequest();
            }

            var moviesPagedList = await _movieRepository.GetAllMoviesAsync(requestParameters.OrderBy,
                                                                    requestParameters.SearchQuery,
                                                                    requestParameters.PageNumber, requestParameters.PageSize);

            var movies = Mapper.Map<IEnumerable<MovieDto>>(moviesPagedList);

            if (mediaType == "application/vnd.biob.json+hateoas" || mediaType == "application/vnd.biob.xml+hateoas")
            {  
                return Ok(CreateHateoasResponse(moviesPagedList, requestParameters));
            }
            else
            {
                var previousPageLink = moviesPagedList.HasPrevious ? CreateUrlForResource(requestParameters, PageType.PreviousPage) : null;
                var nextPageLink = moviesPagedList.HasNext ? CreateUrlForResource(requestParameters, PageType.NextPage) : null;
                var paginationMetadata = new PaginationMetadata()
                {
                    TotalCount = moviesPagedList.TotalCount,
                    PageSize = moviesPagedList.PageSize,
                    CurrentPage = moviesPagedList.CurrentPage,
                    TotalPages = moviesPagedList.TotalPages,
                    PreviousPageLink = previousPageLink,
                    NextPageLink = nextPageLink
                };

                Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));

                if (requestParameters.IncludeMetadata)
                {
                    var shapedMovies = movies.ShapeData(requestParameters.Fields);
                    var moviesWithMetadata = new EntityWithPaginationMetadataDto<ExpandoObject>(paginationMetadata, shapedMovies);
                    return Ok(moviesWithMetadata);
                }

                return Ok(movies.ShapeData(requestParameters.Fields));
            }
        }

        [HttpGet("{movieId}", Name = "GetMovie")]
        //[MovieParameterValidationFilter]
        public async Task<IActionResult> GetOneMovie([FromRoute]Guid movieId, [FromQuery] string fields, [FromHeader(Name = "Accept")] string mediaType)
        {
            if (!_typeHelperService.TypeHasProperties<MovieDto>(fields))
            {
                return BadRequest();
            }

            var foundMovie = await _movieRepository.GetMovieAsync(movieId);

            var movie = Mapper.Map<MovieDto>(foundMovie);

            if (foundMovie == null)
            {
                return NotFound();
            }

            if (mediaType == "application/vnd.biob.json+hateoas")
            {
                var links = CreateLinksForMovies(movieId, fields);

                var linkedMovie = movie.ShapeData(fields) as IDictionary<string, object>;
                linkedMovie.Add("links", links);
                return Ok(linkedMovie);
            }
            else
            {
                return Ok(movie.ShapeData(fields));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovie([FromBody] MovieToCreateDto movieToCreate)
        {
            if (movieToCreate == null)
            {
                return BadRequest();
            }

            if (movieToCreate.Id == null)
            {
                movieToCreate.Id = Guid.NewGuid();
            }

            if (!ModelState.IsValid)
            {
                return new ProccessingEntityObjectResultErrors(ModelState);
            }


            var movieToAdd = Mapper.Map<Movie>(movieToCreate);
            _movieRepository.AddMovie(movieToAdd);

            if (!await _movieRepository.SaveChangesAsync())
            {
                _logger.LogError("Saving changes to database while creating a movie failed");
            }
            var movieDto = Mapper.Map<MovieDto>(movieToAdd);

            var links = CreateLinksForMovies(movieDto.Id, null);

            var linkedMovie = movieDto as IDictionary<string, object>;

            linkedMovie.Add("links", links);
            
            return CreatedAtRoute("GetMovie", new { movieId = movieToAdd.Id }, linkedMovie);
        }

        [HttpPut("{movieId}",Name = "UpdateMovie")]
        [MovieParameterValidationFilter]
        public async Task<IActionResult> UpdateMovie([FromRoute] Guid movieId, [FromBody] MovieToUpdateDto movieToUpdate)
        {
            if (movieToUpdate == null)
            {
                return BadRequest();
            }

            var movieFromDb = await _movieRepository.GetMovieAsync(movieId);

            //  upserting if movie does not already exist
            if (movieFromDb == null)
            {
                var movieEntity = Mapper.Map<Movie>(movieToUpdate);
                movieEntity.Id = movieId;
                _movieRepository.AddMovie(movieEntity);

                if (!await _movieRepository.SaveChangesAsync())
                {
                    _logger.LogError($"Upserting movie: {movieId} failed on save");
                }

                var movieToReturn = Mapper.Map<MovieDto>(movieEntity);

                return CreatedAtRoute("GetMovie", new { movieId = movieToReturn.Id }, movieToReturn);
            }

            Mapper.Map(movieToUpdate, movieFromDb);

            _movieRepository.UpdateMovie(movieFromDb);

            if (!await _movieRepository.SaveChangesAsync())
            {
                _logger.LogError($"Updating movie: {movieId} failed on save");
            }

            return NoContent();
        }

        [HttpPatch("{movieId}", Name = "PartiallyUpdateMovie")]
        [MovieParameterValidationFilter]
        public async Task<IActionResult> PartiuallyUpdateMovie([FromRoute] Guid movieId, JsonPatchDocument<MovieToUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var movieFromDb = await _movieRepository.GetMovieAsync(movieId);

            //  upserting if movie does not already exist
            //  TODO:   research if upserting is neccesary in patching
            if (movieFromDb == null)
            {
                var movieToCreate = new MovieToUpdateDto();

                patchDoc.ApplyTo(movieToCreate, ModelState);

                if (!ModelState.IsValid)
                {
                    new ProccessingEntityObjectResultErrors(ModelState);
                }

                var movieToAddToDb = Mapper.Map<Movie>(movieToCreate);
                movieToAddToDb.Id = movieId;

                _movieRepository.AddMovie(movieToAddToDb);

                if (!await _movieRepository.SaveChangesAsync())
                {
                    _logger.LogError($"Upserting movie: {movieId} failed on save");
                }

                var movieToReturn = Mapper.Map<MovieDto>(movieToAddToDb);

                return CreatedAtRoute("GetMovie", new { movieId = movieToReturn.Id }, movieToReturn);
            }

            var movieToPatch = Mapper.Map<MovieToUpdateDto>(movieFromDb);

            patchDoc.ApplyTo(movieToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                new ProccessingEntityObjectResultErrors(ModelState);
            }

            Mapper.Map(movieToPatch, movieFromDb);

            _movieRepository.UpdateMovie(movieFromDb);

            if (!await _movieRepository.SaveChangesAsync())
            {
                _logger.LogError($"Partially updating movie: {movieId} failed on save");
            }

            return NoContent();
        }

        [HttpDelete("{movieId}", Name = "DeleteMovie")]
        [MovieParameterValidationFilter]
        public async Task<IActionResult> DeleteMovie([FromRoute] Guid movieId)
        {
            var movieFromDb = await _movieRepository.GetMovieAsync(movieId);

            if (movieFromDb == null)
            {
                return NotFound();
            }

            _movieRepository.DeleteMovie(movieFromDb);

            if (!await _movieRepository.SaveChangesAsync())
            {
                _logger.LogError($"Deleting movie: {movieId} failed on save");
            }

            return NoContent();
        }

        private ExpandoObject CreateHateoasResponse(PagedList<Movie> moviesPagedList, RequestParameters requestParameters)
        {

            var movies = Mapper.Map<IEnumerable<MovieDto>>(moviesPagedList);

            var paginationMetadataWithLinks = new
            {
                moviesPagedList.TotalCount,
                moviesPagedList.PageSize,
                moviesPagedList.CurrentPage,
                moviesPagedList.TotalPages
            };

            Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadataWithLinks));

            var links = CreateLinksForMovies(requestParameters, moviesPagedList.HasNext, moviesPagedList.HasPrevious);

            

            var shapedMovies = movies.ShapeData(requestParameters.Fields);

            var shapedMoviesWithLinks = shapedMovies.Select(movie =>
            {
                var movieDictionary = movie as IDictionary<string, object>;
                var movieLinks = CreateLinksForMovies((Guid)movieDictionary["Id"], requestParameters.Fields);

                movieDictionary.Add("links", movieLinks);

                return movieDictionary;
            });
            if (requestParameters.IncludeMetadata)
            {
                var moviesWithMetadata = new ExpandoObject();
                ((IDictionary<string, object>)moviesWithMetadata).Add("Metadata", paginationMetadataWithLinks);
                ((IDictionary<string, object>)moviesWithMetadata).Add("movies", shapedMoviesWithLinks);
                ((IDictionary<string, object>)moviesWithMetadata).Add("links", links);
                return moviesWithMetadata;
            }
            else
            {
                var linkedCollection = new ExpandoObject();
                ((IDictionary<string, object>)linkedCollection).Add("movies", shapedMoviesWithLinks);
                ((IDictionary<string, object>)linkedCollection).Add("links", links);
                return linkedCollection;
            }
            
        }

        private IEnumerable<LinkDto> CreateLinksForMovies(Guid id, string fields)
        {
            var links = new List<LinkDto>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                links.Add(new LinkDto(_urlHelper.Link("GetMovie", new { movieId = id }), "self", "GET"));
            }
            else
            {
                links.Add(new LinkDto(_urlHelper.Link("GetMovie", new { movieId = id, fields }), "self", "GET"));
            }
            links.Add(
                new LinkDto(_urlHelper.Link("DeleteMovie", new { movieId = id }), "delete_movie", "DELETE")
                );
            links.Add(
                new LinkDto(_urlHelper.Link("UpdateMovie", new { movieId = id }), "update_book", "PUT")
                );
            links.Add(
                new LinkDto(_urlHelper.Link("PartiallyUpdateMovie", new { movieId = id }), "partially_update_book", "PATCH")
                );
            return links;
        }

        private IEnumerable<LinkDto> CreateLinksForMovies(RequestParameters requestParameters, bool hasNext, bool hasPRevious)
        {
            var links = new List<LinkDto>
            {
                new LinkDto(CreateUrlForResource(requestParameters, PageType.Current), "self", "GET")
            };

            if (hasNext)
            {
                new LinkDto(CreateUrlForResource(requestParameters, PageType.NextPage), "self", "GET");
            }

            if (hasPRevious)
            {
                new LinkDto(CreateUrlForResource(requestParameters, PageType.PreviousPage), "self", "GET");
            }

            return links;

        }


        //  TODO: consider making this a helper method instead, and reuseable
        private string CreateUrlForResource(RequestParameters requestParameters, PageType pageType)
        {
            switch (pageType)
            {
                case PageType.PreviousPage:
                    return _urlHelper.Link("GetMovies", new
                    {
                        fields = requestParameters.Fields,
                        orderBy = requestParameters.OrderBy,
                        searchQuery = requestParameters.SearchQuery,
                        pageNumber = requestParameters.PageNumber - 1,
                        pageSize = requestParameters.PageSize

                    });
                case PageType.NextPage:
                    return _urlHelper.Link("GetMovies", new
                    {
                        fields = requestParameters.Fields,
                        orderBy = requestParameters.OrderBy,
                        searchQuery = requestParameters.SearchQuery,
                        pageNumber = requestParameters.PageNumber + 1,
                        pageSize = requestParameters.PageSize
                    });
                case PageType.Current:
                default:
                    return _urlHelper.Link("GetMovies", new
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
