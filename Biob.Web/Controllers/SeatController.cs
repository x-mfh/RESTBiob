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

namespace Biob.Web.Controllers
{
    [Route("/api/v1/seats")]
    [ApiController]
    public class SeatController : ControllerBase
    {
        private readonly ISeatRepository _seatRepository;

        public SeatController(ISeatRepository seatRepository)
        {
            _seatRepository = seatRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSeatsAsync()
        {
            var entities = await _seatRepository.GetAllSeatsAsync();
            var mappedEntities = Mapper.Map<IEnumerable<SeatDto>>(entities);

            return Ok(mappedEntities);
        }

        [HttpGet("{seatId}", Name = "GetSeat")]
        public async Task<IActionResult> GetOneSeat([FromRoute] int seatId)
        {
            var foundSeat = await _seatRepository.GetSeatAsync(seatId);

            if (foundSeat == null)
            {
                return NotFound();
            }

            var seatToReturn = Mapper.Map<SeatDto>(foundSeat);
            return Ok(seatToReturn);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSeat([FromBody] SeatToCreateDto seatToCreate)
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
            _seatRepository.AddSeat(seatToAdd);

            if (!await _seatRepository.SaveChangesAsync())
            {
                // TODO: consider adding logging
                // instead of using expensive exceptions
                throw new Exception("Failed to create new seat");
            }

            return CreatedAtRoute("GetSeat", new { seatId = seatToAdd.Id }, seatToAdd);
        }

        [HttpPut("seatId")]
        public async Task<IActionResult> UpdateSeat([FromRoute] int seatId, [FromBody] SeatToUpdateDto seatToUpdate)
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
                _seatRepository.AddSeat(seatEntity);

                if (!await _seatRepository.SaveChangesAsync())
                {
                    //  TODO: consider adding logging
                    //  instead of using expensive exceptions
                    throw new Exception("Failed to upsert a new seat");
                }

                var seatToReturn = Mapper.Map<SeatDto>(seatEntity);

                return CreatedAtRoute("GetSeat", new { seatId = seatToReturn.Id }, seatToReturn);
            }

            Mapper.Map(seatToUpdate, seatFromDb);

            _seatRepository.UpdateSeat(seatFromDb);

            if (!await _seatRepository.SaveChangesAsync())
            {
                //  TODO: consider adding logging
                //  instead of using expensive exceptions
                throw new Exception("Failed to upsert a new seat");
            }

            return NoContent();

        }

        [HttpPatch("seatId")]
        public async Task<IActionResult> PartiuallyUpdateSeat([FromRoute] int seatId, JsonPatchDocument<SeatToUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var seatFromDb = await _seatRepository.GetSeatAsync(seatId);

            //  upserting if movie does not already exist
            //  TODO: research if upserting is neccesary in patching
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
                _seatRepository.AddSeat(seatToAddToDb);

                if (!await _seatRepository.SaveChangesAsync())
                {
                    throw new Exception("Upserting seat failed");
                }

                var seatToReturn = Mapper.Map<SeatDto>(seatToAddToDb);

                return CreatedAtRoute("GetSeat", new { seatId = seatToReturn.Id }, seatToReturn);
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
                throw new Exception("partially updating seat failed");
            }

            return NoContent();
        }

        [HttpDelete("{seatId}")]
        public async Task<IActionResult> DeleteSeatById([FromRoute]int seatId)
        {
            var seatToDelete = await _seatRepository.GetSeatAsync(seatId);

            if (seatToDelete == null)
            {
                return NotFound();
            }

            _seatRepository.DeleteSeat(seatToDelete);

            if (!await _seatRepository.SaveChangesAsync())
            {
                throw new Exception("failed to delete seat");
            }

            return NoContent();
        }

    }
}
