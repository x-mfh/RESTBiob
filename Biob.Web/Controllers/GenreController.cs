using AutoMapper;
using Biob.Data.Models;
using Biob.Services.Data.DtoModels;
using Biob.Services.Data.Repositories;
using Biob.Web.Helpers;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biob.Web.Controllers
{
    [Route("api/v1/genres")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreRepository _genreRepository;
        private readonly ILogger<GenreController> _logger;

        public GenreController(IGenreRepository genreRepository, ILogger<GenreController> logger)
        {
            _genreRepository = genreRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGenres()
        {
            var genresFound = await _genreRepository.GetAllGenresAsync();

            var mappedGenres = Mapper.Map<IEnumerable<GenreDto>>(genresFound);

            return Ok(mappedGenres);
        }

        [HttpGet("{genreId}", Name = "GetGenre")]
        public async Task<IActionResult> GetGenreById([FromRoute] Guid genreId)
        {
            var genreFound = await _genreRepository.GetGenreByIdAsync(genreId);

            var mappedGenre = Mapper.Map<GenreDto>(genreFound);

            return Ok(mappedGenre);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGenre(GenreToCreateDto genreToCreate)
        {
            if (genreToCreate == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return new ProccessingEntityObjectResultErrors(ModelState);
            }

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

        [HttpPatch("{genreId}")]
        public async Task<IActionResult> UpdateGenre([FromRoute] Guid genreId, JsonPatchDocument<GenreToUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var genreFromDb = await _genreRepository.GetGenreByIdAsync(genreId);

            

            if (!await _genreRepository.SaveChangesAsync())
            {
                _logger.LogError("Saving changes to database while creating a genre failed");
            }

            var genreToPatch = Mapper.Map<GenreToUpdateDto>(genreFromDb);

            patchDoc.ApplyTo(genreToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                new ProccessingEntityObjectResultErrors(ModelState);
            }

            Mapper.Map(genreToPatch, genreFromDb);

            _genreRepository.UpdateGenre(genreFromDb);

            if (!await _genreRepository.SaveChangesAsync())
            {
                _logger.LogError($"Partially updating movie: {genreId} failed on save");
            }

            return CreatedAtRoute("GetGenre", new { genreId }, genreToPatch);

        }


        [HttpDelete("{genreId}")]
        public async Task<IActionResult> DeleteGenre([FromRoute] Guid genreId)
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
    }
}
