using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Biob.Services.Data.Repositories;
using AutoMapper;
using Biob.Services.Data.DtoModels;
using Biob.Data.Models;
using Biob.Web.Helpers;
using Microsoft.AspNetCore.JsonPatch;

namespace Biob.Web.Controllers
{
    [Route("/api/v1/Movies")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;

        public MovieController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMovies()
        {
            var entities = await _movieRepository.GetAllMoviesAsync();
            var mappedEntities = Mapper.Map<IEnumerable<MovieDto>>(entities);
            return Ok(mappedEntities);
        }

        [HttpGet("{movieId}", Name = "GetMovie")]
        public async Task<IActionResult> GetOneMovie([FromRoute]Guid movieId)
        {
            if (movieId == Guid.Empty)
            {
                return BadRequest();
            }

            var foundMovie = await _movieRepository.GetMovieAsync(movieId);

            if (foundMovie == null)
            {
                return NotFound();
            }

            var movieToReturn = Mapper.Map<MovieDto>(foundMovie);
            return Ok(movieToReturn);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovie([FromBody] MovieToCreateDto movieToCreate)
        {
            if (movieToCreate == null)
            {
                return BadRequest();
            }

            if (movieToCreate.Id  == null)
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
                //  TODO: consider adding logging
                //  instead of using expensive exceptions
                throw new Exception("Failed to create a new movie");
            }

            return CreatedAtRoute("GetMovie", new { movieId = movieToAdd.Id }, movieToAdd);
        }

        [HttpPut("{movieId}")]
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
                    //  TODO: consider adding logging
                    //  instead of using expensive exceptions
                    throw new Exception("Failed to upsert a new movie");
                }

                var movieToReturn = Mapper.Map<MovieDto>(movieEntity);

                return CreatedAtRoute("GetMovie", new { movieId = movieToReturn.Id }, movieToReturn);
            }

            Mapper.Map(movieToUpdate, movieFromDb);

            _movieRepository.UpdateMovie(movieFromDb);

            if (!await _movieRepository.SaveChangesAsync())
            {
                //  TODO: consider adding logging
                //  instead of using expensive exceptions
                throw new Exception("Failed to update a movie");
            }

            return NoContent();
        }

        [HttpPatch("{movieId}")]
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
                    throw new Exception("Upserting movie failed");
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
                throw new Exception("partially updating movie failed");
            }

            return NoContent();
        }
    }
}
