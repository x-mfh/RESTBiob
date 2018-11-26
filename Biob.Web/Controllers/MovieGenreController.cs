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

namespace Biob.Web.Controllers
{
    [Route("api/v1/movies/{movieId}/genres")]
    [ApiController]
    public class MovieGenreController : ControllerBase
    {
        private readonly IMovieGenreRepository _movieGenreRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly ILogger<MovieGenreController> _logger;

        public MovieGenreController(IMovieGenreRepository movieGenreRepository, IGenreRepository genreRepository,
                                  IMovieRepository movieRepository, ILogger<MovieGenreController> logger)
        {
            _movieGenreRepository = movieGenreRepository;
            _genreRepository = genreRepository;
            _movieRepository = movieRepository;
            _logger = logger;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAllMovieGenresAsync()
        //{
        //    var entities = await _movieGenreRepository.GetAllMovieGenresAsync();
        //    var mappedEntities = Mapper.Map<IEnumerable<MovieGenreDto>>(entities);

        //    return Ok(mappedEntities);
        //}

        //[HttpGet("{movieGenreId}", Name = "GetMovieGenre")]
        //public async Task<IActionResult> GetOneMovieGenre([FromRoute] int movieGenreId)
        //{
        //    var foundMovieGenre = await _movieGenreRepository.GetMovieGenreAsync(movieGenreId);

        //    if (foundMovieGenre == null)
        //    {
        //        return NotFound();
        //    }

        //    var movieGenreToReturn = Mapper.Map<MovieGenreDto>(foundMovieGenre);
        //    return Ok(movieGenreToReturn);
        //}

        [HttpGet]
        public async Task<IActionResult> GetMovieGenresByMovieId([FromRoute] Guid movieId)
        {
            var movieExists = await _movieRepository.GetMovieAsync(movieId);

            if (movieExists == null)
            {
                return BadRequest();
            }

            var foundMovieGenre = await _movieGenreRepository.GetAllMovieGenresByMovieIdAsync(movieId);

            if (foundMovieGenre == null)
            {
                return NotFound();
            }

            var movieGenreToReturn = Mapper.Map<IEnumerable<MovieGenreDto>>(foundMovieGenre);
            return Ok(movieGenreToReturn);
        }

        [HttpGet("{genreId}")]
        public async Task<IActionResult> GetMovieGenreByMovieIdGenreId([FromRoute] Guid movieId, [FromRoute] Guid genreId)
        {
            var movieExists = await _movieRepository.GetMovieAsync(movieId);

            if (movieExists == null)
            {
                return BadRequest();
            }

            var foundMovieGenre = await _movieGenreRepository.GetMovieGenreByMovieIdGenreIdAsync(movieId, genreId);

            if (foundMovieGenre == null)
            {
                return NotFound();
            }

            var movieGenreToReturn = Mapper.Map<IEnumerable<MovieGenreDto>>(foundMovieGenre);
            return Ok(movieGenreToReturn);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovieGenre([FromRoute] Guid movieId, [FromBody] MovieGenreToCreateDto movieGenreToCreate)
        {
            if (movieGenreToCreate == null)
            {
                return BadRequest();
            }

            var movieExists = await _movieRepository.GetMovieAsync(movieId);

            if (movieExists == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return new ProccessingEntityObjectResultErrors(ModelState);
            }

            // check if genre already exists
            var genreToAdd = await _genreRepository.GetGenreByGenreNameAsync(movieGenreToCreate.GenreName);

            // upserting if genre doesn't exist
            if (genreToAdd == null)
            {
                // use mapping instead
                //genreToAdd.RowNo = movieGenreToCreate.RowNo;
                //genreToAdd.GenreNo = movieGenreToCreate.GenreNo;
                genreToAdd = Mapper.Map<Genre>(movieGenreToCreate);

                _genreRepository.AddGenre(genreToAdd);
            }

            // check if genre combination exist in movie
            var genreExistsInMovie = await _movieGenreRepository.GetMovieGenreByMovieIdGenreIdAsync(movieId, genreToAdd.Id);

            // add new genre to movie
            if (genreExistsInMovie == null)
            {
                // create moviegenre object with genreToAdd id and movieId from route
                MovieGenre movieGenreToAdd = new MovieGenre { MovieId = movieId, GenreId = genreToAdd.Id };

                //var movieGenreToAdd = Mapper.Map<MovieGenre>();
                _movieGenreRepository.AddMovieGenre(movieGenreToAdd);

                if (!await _movieGenreRepository.SaveChangesAsync())
                {
                    _logger.LogError("Saving changes to database while creating a moviegenre failed");
                }

                return CreatedAtRoute("GetMovieGenre", new { movieGenreId = movieGenreToAdd.Id }, movieGenreToAdd);
            }

            // return something if genre already exists in movie
            // maybe redirect to / return object existing object
            return Conflict();

        }


        // TODO
        // check if movie exists, check if genre exists, upsert if not, take id from genre, update only genreId on moviegenre

        [HttpPut("{genreId}")]
        public async Task<IActionResult> UpdateMovieGenreById([FromRoute] Guid movieId, [FromRoute] Guid genreId, [FromRoute] int movieGenreId, [FromBody] MovieGenreToUpdateDto movieGenreToUpdate)
        {
            var movieExists = await _movieRepository.GetMovieAsync(movieId);

            if (movieExists == null)
            {
                return BadRequest();
            }

            if (movieGenreToUpdate == null)
            {
                return BadRequest();
            }

            var movieGenreFromDb = await _movieGenreRepository.GetMovieGenreByIdAsync(movieGenreId);

            // upserting if moviegenre does not already exist
            if (movieGenreFromDb == null)
            {
                var movieGenreEntity = Mapper.Map<MovieGenre>(movieGenreToUpdate);
                movieGenreEntity.Id = movieGenreId;
                _movieGenreRepository.AddMovieGenre(movieGenreEntity);

                if (!await _movieGenreRepository.SaveChangesAsync())
                {
                    //  TODO: consider adding logging
                    //  instead of using expensive exceptions
                    throw new Exception("Failed to upsert a new movieGenre");
                }

                var movieGenreToReturn = Mapper.Map<MovieGenreDto>(movieGenreEntity);

                return CreatedAtRoute("GetMovieGenre", new { movieGenreId = movieGenreToReturn.Id }, movieGenreToReturn);
            }

            Mapper.Map(movieGenreToUpdate, movieGenreFromDb);

            _movieGenreRepository.UpdateMovieGenre(movieGenreFromDb);

            if (!await _movieGenreRepository.SaveChangesAsync())
            {
                //  TODO: consider adding logging
                //  instead of using expensive exceptions
                throw new Exception("Failed to upsert a new movieGenre");
            }

            return NoContent();

        }

        [HttpPatch("{genreId}")]
        public async Task<IActionResult> PartiuallyUpdateMovieGenreById([FromRoute] int movieGenreId, JsonPatchDocument<MovieGenreToUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var movieGenreFromDb = await _movieGenreRepository.GetMovieGenreByIdAsync(movieGenreId);

            //  upserting if moviegenre does not already exist
            //  TODO: research if upserting is neccesary in patching
            if (movieGenreFromDb == null)
            {
                var movieGenreToCreate = new MovieGenreToUpdateDto();
                patchDoc.ApplyTo(movieGenreToCreate, ModelState);

                if (!ModelState.IsValid)
                {
                    new ProccessingEntityObjectResultErrors(ModelState);
                }

                var movieGenreToAddToDb = Mapper.Map<MovieGenre>(movieGenreToCreate);
                movieGenreToAddToDb.Id = movieGenreId;
                _movieGenreRepository.AddMovieGenre(movieGenreToAddToDb);

                if (!await _movieGenreRepository.SaveChangesAsync())
                {
                    throw new Exception("Upserting movieGenre failed");
                }

                var movieGenreToReturn = Mapper.Map<MovieGenreDto>(movieGenreToAddToDb);

                return CreatedAtRoute("GetMovieGenre", new { movieGenreId = movieGenreToReturn.Id }, movieGenreToReturn);
            }

            var movieGenreToPatch = Mapper.Map<MovieGenreToUpdateDto>(movieGenreFromDb);

            patchDoc.ApplyTo(movieGenreToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                new ProccessingEntityObjectResultErrors(ModelState);
            }

            Mapper.Map(movieGenreToPatch, movieGenreFromDb);
            _movieGenreRepository.UpdateMovieGenre(movieGenreFromDb);

            if (!await _movieGenreRepository.SaveChangesAsync())
            {
                throw new Exception("partially updating movieGenre failed");
            }

            return NoContent();
        }

        [HttpDelete("{genreId}")]
        public async Task<IActionResult> DeleteMovieGenreByMovieIdGenreId([FromRoute]Guid movieId, [FromRoute] Guid genreId)
        {
            // check if movie exists
            var movieExists = await _movieRepository.GetMovieAsync(movieId);

            if (movieExists == null)
            {
                return BadRequest();
            }

            // check if genre exists in movie
            var movieGenreToDelete = await _movieGenreRepository.GetMovieGenreByMovieIdGenreIdAsync(movieId, genreId);

            if (movieGenreToDelete == null)
            {
                return NotFound();
            }

            _movieGenreRepository.DeleteMovieGenre(movieGenreToDelete);

            if (!await _movieGenreRepository.SaveChangesAsync())
            {
                _logger.LogError($"Deleting genre {genreId} from movie {movieId} failed on save");
            }

            // check if ANY many-to-many relationships exist with deleted genreid, if not delete genre
            var deleteGenre = await _movieGenreRepository.GetAllMovieGenresByGenreIdAsync(genreId);
            if (deleteGenre == null)
            {
                var genreToDelete = await _genreRepository.GetGenreByIdAsync(genreId);
                _genreRepository.DeleteGenre(genreToDelete);

                if (!await _genreRepository.SaveChangesAsync())
                {
                    _logger.LogError($"Deleting genre {genreId} failed on save");
                }
            }

            return NoContent();
        }

    }
}