using AutoMapper;
using Biob.Data.Models;
using Biob.Services.Data.DtoModels;
using Biob.Services.Data.Repositories;
using Biob.Web.Helpers;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biob.Web.Controllers
{   
    [Route("/api/v1/Showtime")]
    [ApiController]
    public class ShowtimeController : ControllerBase
    {
        private readonly IShowtimeRepository _showtimeRepository;

        public ShowtimeController(IShowtimeRepository showtimeRepository)
        {
            _showtimeRepository = showtimeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllShowtime()
        {
            var entities = await _showtimeRepository.GetAllShowtimesAsync();
            var mappedEntities = Mapper.Map<IEnumerable<ShowtimeDto>>(entities);
            return Ok(mappedEntities);
        }

        [HttpGet("{showtimeId}", Name = "GetShowtime")]
        public async Task<IActionResult> GetOneShowtime([FromRoute]Guid showtimeId)
        {
            if (showtimeId == Guid.Empty)
            {
                return BadRequest();
            }

            var foundShowtime = await _showtimeRepository.GetShowtimeAsync(showtimeId);

            if (foundShowtime == null)
            {
                return NotFound();
            }

            var showtimeToReturn = Mapper.Map<ShowtimeDto>(foundShowtime);
            return Ok(showtimeToReturn);
        }

        [HttpPost]
        public async Task<IActionResult> CreateShowtime([FromBody] ShowtimeToCreateDto showtimeToCreate)
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
            _showtimeRepository.AddShowtime(showtimeToAdd);

            if (!await _showtimeRepository.SaveChangesAsync())
            {
                //TODO: add logging
                throw new Exception("Failed to create a new showtime");
            }

            return CreatedAtRoute("GetShowtime", new { showtimeId = showtimeToAdd.Id }, showtimeToAdd);
        }

        [HttpPut("showtimeId")]
        public async Task<IActionResult> UpdateShowtime([FromRoute] Guid showtimeId, [FromBody] ShowtimeToUpdateDto showtimeToUpdate)
        {
            if (showtimeToUpdate == null)
            {
                return BadRequest();
            }

            var showtimeFromDb = await _showtimeRepository.GetShowtimeAsync(showtimeId);

            if (showtimeFromDb == null)
            {
                var showtimeEntity = Mapper.Map<Showtime>(showtimeToUpdate);
                showtimeEntity.Id = showtimeId;
                _showtimeRepository.AddShowtime(showtimeEntity);

                if (!await _showtimeRepository.SaveChangesAsync())
                {
                    // TODO: add logging
                    throw new Exception("Failed to upsert a new showtime");
                }

                var showtimeToReturn = Mapper.Map<ShowtimeDto>(showtimeEntity);

                return CreatedAtRoute("GetShowtime", new { showtimeId = showtimeToReturn.Id }, showtimeToReturn);
            }

            Mapper.Map(showtimeToUpdate, showtimeFromDb);

            if (!await _showtimeRepository.SaveChangesAsync())
            {
                // TODO: add logging
                throw new Exception("Failed to update showtime");
            }

            return NoContent();
        }

        [HttpPatch("showtimeId")]
        public async Task<IActionResult> PartiuallyUpdateShowtime([FromRoute] Guid showtimeId, JsonPatchDocument<ShowtimeToUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var showtimeFromDb = await _showtimeRepository.GetShowtimeAsync(showtimeId);

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

                _showtimeRepository.AddShowtime(showtimeToAddToDb);

                if (!await _showtimeRepository.SaveChangesAsync())
                {
                    throw new Exception("Upserting showtime failed");
                }

                var showtimeToReturn = Mapper.Map<ShowtimeDto>(showtimeToAddToDb);

                return CreatedAtRoute("GetShowtime", new { showtimeId = showtimeToReturn.Id }, showtimeToReturn);
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
                throw new Exception("partially updating showtime failed");
            }

            return NoContent();
        }

        [HttpDelete("{showtimeId}")]
        public async Task<IActionResult> DeleteShowtime([FromRoute] Guid showtimeId)
        {
            var showtimeToDelete = await _showtimeRepository.GetShowtimeAsync(showtimeId);

            if (showtimeToDelete == null)
            {
                return NotFound();
            }

            _showtimeRepository.DeleteShowtime(showtimeToDelete);

            if (!await _showtimeRepository.SaveChangesAsync())
            {
                throw new Exception("deleting showtime failed");
            }

            return NoContent();
        }
    }
}   
