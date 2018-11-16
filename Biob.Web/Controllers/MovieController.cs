using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biob.Services.Data.Repositories;
using AutoMapper;
using Biob.Services.Data.DtoModels;

namespace Biob.Web.Controllers
{
    
    [ApiController]
    [Route("/api/v1/Movies")]
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
    }
}
