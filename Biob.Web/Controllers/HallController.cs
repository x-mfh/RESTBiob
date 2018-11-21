using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biob.Services.Data.Repositories;
using AutoMapper;
using Biob.Services.Data.DtoModels;
using Biob.Web.Helpers;
using Biob.Data.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace Biob.Web.Controllers
{
    [Route("/api/v1/halls")]
    [ApiController]
    public class HallController : ControllerBase
    {
        private readonly IHallRepository _hallRepository;

        public HallController(IHallRepository hallRepository)
        {
            _hallRepository = hallRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHallsAsync()
        {
            var entities = await _hallRepository.GetAllHallsAsync();
            var mappedEntities = Mapper.Map<IEnumerable<HallDto>>(entities);

            return Ok(mappedEntities);
        }

        [HttpGet("{hallId}", Name = "GetHall")]
        public async Task<IActionResult> GetOneHall([FromRoute] int hallId)
        {
            var foundHall = await _hallRepository.GetHallAsync(hallId);

            if (foundHall == null)
            {
                return NotFound();
            }

            var hallToReturn = Mapper.Map<HallDto>(foundHall);
            return Ok(hallToReturn);
        }

        [HttpPost]
        public async Task<IActionResult> CreateHall([FromBody] HallToCreateDto hallToCreate)
        {
            if (hallToCreate == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return new ProccessingEntityObjectResultErrors(ModelState);
            }

            var hallToAdd = Mapper.Map<Hall>(hallToCreate);
            _hallRepository.AddHall(hallToAdd);

            if (!await _hallRepository.SaveChangesAsync())
            {
                // TODO: consider adding logging
                // instead of using expensive exceptions
                throw new Exception("Failed to create new hall");
            }

            return CreatedAtRoute("GetHall", new { hallId = hallToAdd.Id }, hallToAdd);
        }

        [HttpPut("hallId")]
        public async Task<IActionResult> UpdateHall([FromRoute] int hallId, [FromBody] HallToUpdateDto hallToUpdate)
        {
            if (hallToUpdate == null)
            {
                return BadRequest();
            }

            var hallFromDb = await _hallRepository.GetHallAsync(hallId);

            // upserting if movie does not already exist
            if (hallFromDb == null)
            {
                var hallEntity = Mapper.Map<Hall>(hallToUpdate);
                hallEntity.Id = hallId;
                _hallRepository.AddHall(hallEntity);

                if (!await _hallRepository.SaveChangesAsync())
                {
                    //  TODO: consider adding logging
                    //  instead of using expensive exceptions
                    throw new Exception("Failed to upsert a new hall");
                }

                var hallToReturn = Mapper.Map<HallDto>(hallEntity);

                return CreatedAtRoute("GetHall", new { hallId = hallToReturn.Id }, hallToReturn);
            }

            Mapper.Map(hallToUpdate, hallFromDb);

            _hallRepository.UpdateHall(hallFromDb);

            if (!await _hallRepository.SaveChangesAsync())
            {
                //  TODO: consider adding logging
                //  instead of using expensive exceptions
                throw new Exception("Failed to upsert a new hall");
            }

            return NoContent();

        }

        [HttpPatch("hallId")]
        public async Task<IActionResult> PartiuallyUpdateHall([FromRoute] int hallId, JsonPatchDocument<HallToUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var hallFromDb = await _hallRepository.GetHallAsync(hallId);

            //  upserting if movie does not already exist
            //  TODO: research if upserting is neccesary in patching
            if (hallFromDb == null)
            {
                var hallToCreate = new HallToUpdateDto();
                patchDoc.ApplyTo(hallToCreate, ModelState);

                if (!ModelState.IsValid)
                {
                    new ProccessingEntityObjectResultErrors(ModelState);
                }

                var hallToAddToDb = Mapper.Map<Hall>(hallToCreate);
                hallToAddToDb.Id = hallId;
                _hallRepository.AddHall(hallToAddToDb);

                if (!await _hallRepository.SaveChangesAsync())
                {
                    throw new Exception("Upserting hall failed");
                }

                var hallToReturn = Mapper.Map<HallDto>(hallToAddToDb);

                return CreatedAtRoute("GetHall", new { hallId = hallToReturn.Id }, hallToReturn);
            }

            var hallToPatch = Mapper.Map<HallToUpdateDto>(hallFromDb);

            patchDoc.ApplyTo(hallToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                new ProccessingEntityObjectResultErrors(ModelState);
            }

            Mapper.Map(hallToPatch, hallFromDb);
            _hallRepository.UpdateHall(hallFromDb);

            if (!await _hallRepository.SaveChangesAsync())
            {
                throw new Exception("partially updating hall failed");
            }

            return NoContent();
        }

        [HttpDelete("{hallId}")]
        public async Task<IActionResult> DeleteHallById([FromRoute]int hallId)
        {
            var hallToDelete = await _hallRepository.GetHallAsync(hallId);

            if (hallToDelete == null)
            {
                return NotFound();
            }

            _hallRepository.DeleteHall(hallToDelete);

            if (!await _hallRepository.SaveChangesAsync())
            {
                throw new Exception("failed to delete hall");
            }

            return NoContent();
        }

    }
}
