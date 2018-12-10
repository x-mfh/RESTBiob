using AutoMapper;
using Biob.Data.Models;
using Biob.Services.Data.DtoModels.GenreDtos;
using Biob.Services.Data.Helpers;
using Biob.Services.Data.Repositories;
using Biob.Web.Api.Filters;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace Biob.Web.Api.Controllers
{
    [Route("api/v1/genres")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IUrlHelper _urlHelper;
        private readonly ILogger<GenreController> _logger;

        public GenreController(IGenreRepository genreRepository, IUrlHelper urlHelper, ILogger<GenreController> logger)
        {
            _genreRepository = genreRepository;
            _urlHelper = urlHelper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGenresAsync([FromHeader(Name = "Accept")] string mediaType)
        {
            var genres = await _genreRepository.GetAllGenresAsync();

            var mappedGenres = Mapper.Map<IEnumerable<GenreDto>>(genres);

            if (mediaType == "application/vnd.biob.json+hateoas")
            {
                return Ok(CreateLinksForGenres(mappedGenres));
            }

            return Ok(mappedGenres);
        }

        [HttpGet("{genreId}", Name = "GetGenre")]
        [GuidCheckActionFilter(new string[] { "genreId" })]
        public async Task<IActionResult> GetGenreByIdAsync([FromRoute] Guid genreId, [FromHeader(Name = "Accept")] string mediaType)
        {
            var genreFound = await _genreRepository.GetGenreByIdAsync(genreId);

            if (genreFound == null)
            {
                return NotFound();
            }

            var mappedGenre = Mapper.Map<GenreDto>(genreFound);

            if (mediaType == "application/vnd.biob.json+hateoas")
            {
                var links = CreateLinksForGenre(genreId);

                var linkedGenre = mappedGenre.ShapeData(null) as IDictionary<string, object>;

                linkedGenre.Add("links", links);

                return Ok(linkedGenre);
            }
            return Ok(mappedGenre);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGenreAsync(GenreToCreateDto genreToCreate)
        {

            var genreToAdd = Mapper.Map<Genre>(genreToCreate);
            _genreRepository.AddGenre(genreToAdd);


            // mapped created genre back to genreDto to remove unnecessary information
            var genreToReturn = Mapper.Map<GenreDto>(genreToAdd);

            if (!await _genreRepository.SaveChangesAsync())
            {
                _logger.LogError("Saving changes to database while creating a genre failed");
            }
            return CreatedAtRoute("GetGenre", new { genreId = genreToReturn.Id }, genreToReturn);
        }

        [HttpPatch("{genreId}", Name = "PartiallyUpdateGenre")]
        [GuidCheckActionFilter(new string[] { "genreId" })]
        public async Task<IActionResult> PartiallyUpdateGenreAsync([FromRoute] Guid genreId, JsonPatchDocument<GenreToUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var genreFromDb = await _genreRepository.GetGenreByIdAsync(genreId);

            var genreToPatch = Mapper.Map<GenreToUpdateDto>(genreFromDb);

            patchDoc.ApplyTo(genreToPatch, ModelState);

            Mapper.Map(genreToPatch, genreFromDb);

            _genreRepository.UpdateGenre(genreFromDb);

            if (!await _genreRepository.SaveChangesAsync())
            {
                _logger.LogError($"Partially updating movie: {genreId} failed on save");
            }

            return NoContent();

        }


        [HttpPut("{genreId}", Name = "UpdateGenre")]
        [GuidCheckActionFilter(new string[] { "genreId" })]
        public async Task<IActionResult> UpdateGenreAsync([FromRoute] Guid genreId, [FromBody] GenreToUpdateDto genreToUpdate)
        {
            var genreFromDb = await _genreRepository.GetGenreByIdAsync(genreId);

            Mapper.Map(genreToUpdate, genreFromDb);

            _genreRepository.UpdateGenre(genreFromDb);

            if (!await _genreRepository.SaveChangesAsync())
            {
                _logger.LogError($"Partially updating movie: {genreId} failed on save");
            }

            return NoContent();

        }


        [HttpDelete("{genreId}", Name = "DeleteGenre")]
        [GuidCheckActionFilter(new string[] { "genreId" })]
        public async Task<IActionResult> DeleteGenreAsync([FromRoute] Guid genreId)
        {
            var genreToDelete = await _genreRepository.GetGenreByIdAsync(genreId);

            if (genreToDelete == null)
            {
                NotFound();
            }

            _genreRepository.DeleteGenre(genreToDelete);

            if (!await _genreRepository.SaveChangesAsync())
            {
                _logger.LogError($"Deleting genre: {genreId} failed on save");
            }

            return NoContent();
        }

        [HttpOptions]
        public IActionResult GetGenresOptions()
        {
            Response.Headers.Add("Allow", "GET,POST,OPTIONS");
            return Ok();
        }

        [HttpOptions("{genreId}")]
        public IActionResult GetGenreOptions()
        {
            Response.Headers.Add("Allow", "GET,PATCH,PUT,OPTIONS");
            return Ok();
        }

        private IEnumerable<LinkDto> CreateLinksForGenre(Guid id)
        {
            var links = new List<LinkDto>
            {
                new LinkDto(_urlHelper.Link("GetGenre", new { genreId = id }), "self", "GET"),
                new LinkDto(_urlHelper.Link("UpdateGenre", new { genreId = id }), "update_genre", "PUT"),
                new LinkDto(_urlHelper.Link("PartiallyUpdateGenre", new { genreId = id }), "partially_update_genre", "PATCH"),
                new LinkDto(_urlHelper.Link("DeleteGenre", new { genreId = id }), "delete_genre", "DELETE")
            };

            return links;
        }

        private ExpandoObject CreateLinksForGenres(IEnumerable<GenreDto> genreList)
        {
            var shapedGenres = genreList.ShapeData(null);
            var genresWithLinks = shapedGenres.Select(genre =>
            {
                var genreDictionary = genre as IDictionary<string, object>;
                var genreLinks = CreateLinksForGenre((Guid)genreDictionary["Id"]);

                genreDictionary.Add("links", genreLinks);

                return genreDictionary;
            });
            var linkedCollection = new ExpandoObject();
            ((IDictionary<string, object>)linkedCollection).Add("genres", genresWithLinks);

            return linkedCollection;
        }
    }
}
