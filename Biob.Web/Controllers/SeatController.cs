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
    [Route("/api/v1/seats")]
    [ApiController]
    public class SeatController : ControllerBase
    {
        private readonly ISeatRepository _seatRepository;
        private readonly ILogger<SeatController> _logger;

        public SeatController(ISeatRepository seatRepository, ILogger<SeatController> logger)
        {
            _seatRepository = seatRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSeats()
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

        [HttpGet("seat/{seatNo}")]
        public async Task<IActionResult> GetSeatsBySeatNo([FromRoute] int seatNo)
        {
            var foundSeats = await _seatRepository.GetSeatsBySeatNoAsync(seatNo);

            if (foundSeats == null)
            {
                return NotFound();
            }

            var seatToReturn = Mapper.Map<IEnumerable<SeatDto>>(foundSeats);
            return Ok(seatToReturn);
        }

        [HttpGet("row/{rowNo}")]
        public async Task<IActionResult> GetSeatsByRowNo([FromRoute] int rowNo)
        {
            var foundSeats = await _seatRepository.GetSeatsByRowNoAsync(rowNo);

            if (foundSeats == null)
            {
                return NotFound();
            }

            var seatToReturn = Mapper.Map<IEnumerable<SeatDto>>(foundSeats);
            return Ok(seatToReturn);
        }

        // change so you can search with query
        [HttpGet("row/{rowNo}/seat/{seatNo}")]
        [HttpGet("seat/{seatNo}/row/{rowNo}")]
        public async Task<IActionResult> GetSeatByRowNoSeatNo([FromRoute] int rowNo, [FromRoute] int seatNo)
        {
            var foundSeat = await _seatRepository.GetSeatByRowNoSeatNoAsync(rowNo, seatNo);

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
                _logger.LogError("Saving changes to database while creating a seat failed");
            }

            return CreatedAtRoute("GetSeat", new { seatId = seatToAdd.Id }, seatToAdd);
        }

        [HttpPut("{seatId}")]
        public async Task<IActionResult> UpdateSeatById([FromRoute] int seatId, [FromBody] SeatToUpdateDto seatToUpdate)
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
                    _logger.LogError($"Upserting seat: {seatId} failed on save");
                }

                var seatToReturn = Mapper.Map<SeatDto>(seatEntity);

                return CreatedAtRoute("GetSeat", new { seatId = seatToReturn.Id }, seatToReturn);
            }

            Mapper.Map(seatToUpdate, seatFromDb);

            _seatRepository.UpdateSeat(seatFromDb);

            if (!await _seatRepository.SaveChangesAsync())
            {
                _logger.LogError($"Updating seat: {seatId} failed on save");
            }

            return NoContent();

        }

        [HttpPatch("{seatId}")]
        public async Task<IActionResult> PartiuallyUpdateSeatById([FromRoute] int seatId, JsonPatchDocument<SeatToUpdateDto> patchDoc)
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
                    _logger.LogError($"Upserting seat: {seatId} failed on save");
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
                _logger.LogError($"Partially updating seat: {seatId} failed on save");
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
                _logger.LogError($"Deleting seat: {seatId} failed on save");
            }

            return NoContent();
        }

    }
}
