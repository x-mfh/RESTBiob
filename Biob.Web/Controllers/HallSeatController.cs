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
using Microsoft.Extensions.Logging;

namespace Biob.Web.Controllers
{
    [Route("api/v1/halls/{hallId}/seats")]
    [ApiController]
    public class HallSeatController : ControllerBase
    {
        private readonly IHallSeatRepository _hallSeatRepository;
        private readonly ISeatRepository _seatRepository;
        private readonly IHallRepository _hallRepository;
        private readonly ILogger<HallSeatController> _logger;

        public HallSeatController(IHallSeatRepository hallSeatRepository, ISeatRepository seatRepository, 
                                  IHallRepository hallRepository, ILogger<HallSeatController> logger)
        {
            _hallSeatRepository = hallSeatRepository;
            _seatRepository = seatRepository;
            _hallRepository = hallRepository;
            _logger = logger;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAllHallSeatsAsync()
        //{
        //    var entities = await _hallSeatRepository.GetAllHallSeatsAsync();
        //    var mappedEntities = Mapper.Map<IEnumerable<HallSeatDto>>(entities);

        //    return Ok(mappedEntities);
        //}

        //[HttpGet("{hallSeatId}", Name = "GetHallSeat")]
        //public async Task<IActionResult> GetOneHallSeat([FromRoute] int hallSeatId)
        //{
        //    var foundHallSeat = await _hallSeatRepository.GetHallSeatAsync(hallSeatId);

        //    if (foundHallSeat == null)
        //    {
        //        return NotFound();
        //    }

        //    var hallSeatToReturn = Mapper.Map<HallSeatDto>(foundHallSeat);
        //    return Ok(hallSeatToReturn);
        //}

        [HttpGet]
        public async Task<IActionResult> GetHallSeatsByHallId([FromRoute] int hallId)
        {
            var hallExists = _hallRepository.GetHallAsync(hallId);

            if (hallExists == null)
            {
                return BadRequest();
            }

            var foundHallSeat = await _hallSeatRepository.GetAllByHallId(hallId);

            if (foundHallSeat == null)
            {
                return NotFound();
            }

            var hallSeatToReturn = Mapper.Map<IEnumerable<HallSeatDto>>(foundHallSeat);
            return Ok(hallSeatToReturn);
        }

        [HttpGet("{seatId}")]
        public async Task<IActionResult> GetHallSeatByHallIdSeatId([FromRoute] int hallId, [FromRoute] int seatId)
        {
            var hallExists = _hallRepository.GetHallAsync(hallId);

            if (hallExists == null)
            {
                return BadRequest();
            }

            var foundHallSeat = await _hallSeatRepository.GetHallSeatByHallIdSeatIdAsync(hallId, seatId);

            if (foundHallSeat == null)
            {
                return NotFound();
            }

            var hallSeatToReturn = Mapper.Map<IEnumerable<HallSeatDto>>(foundHallSeat);
            return Ok(hallSeatToReturn);
        }

        [HttpPost]
        public async Task<IActionResult> CreateHallSeat([FromRoute] int hallId, [FromBody] HallSeatToCreateDto hallSeatToCreate)
        {
            if (hallSeatToCreate == null)
            {
                return BadRequest();
            }

            var hallExists = _hallRepository.GetHallAsync(hallId);

            if (hallExists == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return new ProccessingEntityObjectResultErrors(ModelState);
            }

            // check if seat combination already exists
            var seatToAdd = await _seatRepository.GetSeatByRowNoSeatNoAsync(hallSeatToCreate.RowNo, hallSeatToCreate.SeatNo);

            // upserting if seat doesn't exist
            if (seatToAdd == null)
            {
                // use mapping instead
                //seatToAdd.RowNo = hallSeatToCreate.RowNo;
                //seatToAdd.SeatNo = hallSeatToCreate.SeatNo;
                seatToAdd = Mapper.Map<Seat>(hallSeatToCreate);

                _seatRepository.AddSeat(seatToAdd);
            }

            // check if seat combination exist in hall
            var seatExistsInHall = await _hallSeatRepository.GetHallSeatByHallIdSeatIdAsync(hallId, seatToAdd.Id);

            // add new seat to hall
            if (seatExistsInHall == null)
            {
                // create hallseat object with seatToAdd id and hallId from route
                HallSeat hallSeatToAdd = new HallSeat { HallId = hallId, SeatId = seatToAdd.Id };

                //var hallSeatToAdd = Mapper.Map<HallSeat>();
                _hallSeatRepository.AddHallSeat(hallSeatToAdd);

                if (!await _hallSeatRepository.SaveChangesAsync())
                {
                    _logger.LogError("Saving changes to database while creating a hallseat failed");
                }

                return CreatedAtRoute("GetHallSeat", new { hallSeatId = hallSeatToAdd.Id }, hallSeatToAdd);
            }

            // return something if seat already exists in hall
            // maybe redirect to existing object
            return Conflict(); 

        }


        // TODO
        // check if hall exists, check if seat exists, upsert if not, take id from seat, update only seatId on hallseat

        [HttpPut("{seatId}")]
        public async Task<IActionResult> UpdateHallSeatById([FromRoute] int hallId, [FromRoute] int seatId, [FromRoute] int hallSeatId, [FromBody] HallSeatToUpdateDto hallSeatToUpdate)
        {
            var hallExists = _hallRepository.GetHallAsync(hallId);

            if (hallExists == null)
            {
                return BadRequest();
            }

            if (hallSeatToUpdate == null)
            {
                return BadRequest();
            }

            var hallSeatFromDb = await _hallSeatRepository.GetHallSeatAsync(hallSeatId);

            // upserting if hallseat does not already exist
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

        [HttpPatch("{seatId}")]
        public async Task<IActionResult> PartiuallyUpdateHallSeatById([FromRoute] int hallSeatId, JsonPatchDocument<HallSeatToUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var hallSeatFromDb = await _hallSeatRepository.GetHallSeatAsync(hallSeatId);

            //  upserting if hallseat does not already exist
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

        [HttpDelete("{seatId}")]
        public async Task<IActionResult> DeleteHallSeatByHallIdSeatId([FromRoute]int hallId, [FromRoute] int seatId)
        {
            var hallSeatToDelete = await _hallSeatRepository.GetHallSeatByHallIdSeatIdAsync(hallId, seatId);

            if (hallSeatToDelete == null)
            {
                return NotFound();
            }

            _hallSeatRepository.DeleteHallSeat(hallSeatToDelete);

            if (!await _hallSeatRepository.SaveChangesAsync())
            {
                _logger.LogError($"Deleting seat {seatId} from hall {hallId} failed on save");
            }

            return NoContent();
        }

    }
}
