﻿using Microsoft.AspNetCore.Mvc;
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
    [Route("/api/v1/hallSeats")]
    [ApiController]
    public class HallSeatController : ControllerBase
    {
        private readonly IHallSeatRepository _hallSeatRepository;

        public HallSeatController(IHallSeatRepository hallSeatRepository)
        {
            _hallSeatRepository = hallSeatRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHallSeatsAsync()
        {
            var entities = await _hallSeatRepository.GetAllHallSeatsAsync();
            var mappedEntities = Mapper.Map<IEnumerable<HallSeatDto>>(entities);

            return Ok(mappedEntities);
        }

        [HttpGet("{hallSeatId}", Name = "GetHallSeat")]
        public async Task<IActionResult> GetOneHallSeat([FromRoute] int hallSeatId)
        {
            var foundHallSeat = await _hallSeatRepository.GetHallSeatAsync(hallSeatId);

            if (foundHallSeat == null)
            {
                return NotFound();
            }

            var hallSeatToReturn = Mapper.Map<HallSeatDto>(foundHallSeat);
            return Ok(hallSeatToReturn);
        }

        [HttpGet("hall/{hallId}")]
        public async Task<IActionResult> GetHallSeatByHallId([FromRoute] int hallId)
        {
            var foundHallSeat = await _hallSeatRepository.GetAllByHallId(hallId);

            if (foundHallSeat == null)
            {
                return NotFound();
            }

            var hallSeatToReturn = Mapper.Map<IEnumerable<HallSeatDto>>(foundHallSeat);
            return Ok(hallSeatToReturn);
        }

        [HttpGet("seat/{seatId}")]
        public async Task<IActionResult> GetHallSeatBySeatId([FromRoute] int seatId)
        {
            var foundHallSeat = await _hallSeatRepository.GetAllBySeatId(seatId);

            if (foundHallSeat == null)
            {
                return NotFound();
            }

            var hallSeatToReturn = Mapper.Map<IEnumerable<HallSeatDto>>(foundHallSeat);
            return Ok(hallSeatToReturn);
        }

        [HttpPost]
        public async Task<IActionResult> CreateHallSeat([FromBody] HallSeatToCreateDto hallSeatToCreate)
        {
            if (hallSeatToCreate == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return new ProccessingEntityObjectResultErrors(ModelState);
            }

            var hallSeatToAdd = Mapper.Map<HallSeat>(hallSeatToCreate);
            _hallSeatRepository.AddHallSeat(hallSeatToAdd);

            if (!await _hallSeatRepository.SaveChangesAsync())
            {
                // TODO: consider adding logging
                // instead of using expensive exceptions
                throw new Exception("Failed to create new hallSeat");
            }

            return CreatedAtRoute("GetHallSeat", new { hallSeatId = hallSeatToAdd.Id }, hallSeatToAdd);
        }

        [HttpPut("{hallSeatId}")]
        public async Task<IActionResult> UpdateHallSeat([FromRoute] int hallSeatId, [FromBody] HallSeatToUpdateDto hallSeatToUpdate)
        {
            if (hallSeatToUpdate == null)
            {
                return BadRequest();
            }

            var hallSeatFromDb = await _hallSeatRepository.GetHallSeatAsync(hallSeatId);

            // upserting if movie does not already exist
            if (hallSeatFromDb == null)
            {
                var hallSeatEntity = Mapper.Map<HallSeat>(hallSeatToUpdate);
                hallSeatEntity.Id = hallSeatId;
                _hallSeatRepository.AddHallSeat(hallSeatEntity);

                if (!await _hallSeatRepository.SaveChangesAsync())
                {
                    //  TODO: consider adding logging
                    //  instead of using expensive exceptions
                    throw new Exception("Failed to upsert a new hallSeat");
                }

                var hallSeatToReturn = Mapper.Map<HallSeatDto>(hallSeatEntity);

                return CreatedAtRoute("GetHallSeat", new { hallSeatId = hallSeatToReturn.Id }, hallSeatToReturn);
            }

            Mapper.Map(hallSeatToUpdate, hallSeatFromDb);

            _hallSeatRepository.UpdateHallSeat(hallSeatFromDb);

            if (!await _hallSeatRepository.SaveChangesAsync())
            {
                //  TODO: consider adding logging
                //  instead of using expensive exceptions
                throw new Exception("Failed to upsert a new hallSeat");
            }

            return NoContent();

        }

        [HttpPatch("{hallSeatId}")]
        public async Task<IActionResult> PartiuallyUpdateHallSeat([FromRoute] int hallSeatId, JsonPatchDocument<HallSeatToUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var hallSeatFromDb = await _hallSeatRepository.GetHallSeatAsync(hallSeatId);

            //  upserting if movie does not already exist
            //  TODO: research if upserting is neccesary in patching
            if (hallSeatFromDb == null)
            {
                var hallSeatToCreate = new HallSeatToUpdateDto();
                patchDoc.ApplyTo(hallSeatToCreate, ModelState);

                if (!ModelState.IsValid)
                {
                    new ProccessingEntityObjectResultErrors(ModelState);
                }

                var hallSeatToAddToDb = Mapper.Map<HallSeat>(hallSeatToCreate);
                hallSeatToAddToDb.Id = hallSeatId;
                _hallSeatRepository.AddHallSeat(hallSeatToAddToDb);

                if (!await _hallSeatRepository.SaveChangesAsync())
                {
                    throw new Exception("Upserting hallSeat failed");
                }

                var hallSeatToReturn = Mapper.Map<HallSeatDto>(hallSeatToAddToDb);

                return CreatedAtRoute("GetHallSeat", new { hallSeatId = hallSeatToReturn.Id }, hallSeatToReturn);
            }

            var hallSeatToPatch = Mapper.Map<HallSeatToUpdateDto>(hallSeatFromDb);

            patchDoc.ApplyTo(hallSeatToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                new ProccessingEntityObjectResultErrors(ModelState);
            }

            Mapper.Map(hallSeatToPatch, hallSeatFromDb);
            _hallSeatRepository.UpdateHallSeat(hallSeatFromDb);

            if (!await _hallSeatRepository.SaveChangesAsync())
            {
                throw new Exception("partially updating hallSeat failed");
            }

            return NoContent();
        }

        [HttpDelete("{hallSeatId}")]
        public async Task<IActionResult> DeleteHallSeatById([FromRoute]int hallSeatId)
        {
            var hallSeatToDelete = await _hallSeatRepository.GetHallSeatAsync(hallSeatId);

            if (hallSeatToDelete == null)
            {
                return NotFound();
            }

            _hallSeatRepository.DeleteHallSeat(hallSeatToDelete);

            if (!await _hallSeatRepository.SaveChangesAsync())
            {
                throw new Exception("failed to delete hallSeat");
            }

            return NoContent();
        }

    }
}