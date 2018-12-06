using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biob.Services.Data.Repositories;
using AutoMapper;
using Biob.Web.Api.Helpers;
using Biob.Data.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;
using Biob.Services.Data.Helpers;
using System.Dynamic;
using Biob.Services.Data.DtoModels.HallDtos;
using Biob.Web.Api.Filters;

namespace Biob.Web.Api.Controllers
{
    [Route("/api/v1/halls")]
    [ApiController]
    public class HallController : ControllerBase
    {
        private readonly IHallRepository _hallRepository;
        private readonly IUrlHelper _urlHelper;
        private readonly ILogger<HallController> _logger;

        public HallController(IHallRepository hallRepository, IUrlHelper urlHelper, ILogger<HallController> logger)
        {
            _hallRepository = hallRepository;
            _urlHelper = urlHelper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHallsAsync([FromHeader(Name = "Accept")] string mediaType)
        {
            var halls = await _hallRepository.GetAllHallsAsync();

            var mappedHalls = Mapper.Map<IEnumerable<HallDto>>(halls);

            if (mediaType == "application/vnd.biob.json+hateoas")
            {
                return Ok(CreateLinksForHalls(mappedHalls));
            }

            return Ok(mappedHalls);
        }

        [HttpGet("{hallId}", Name = "GetHall")]
        [GuidCheckActionFilter(new string[] { "hallId" })]
        public async Task<IActionResult> GetOneHallAsync([FromRoute] Guid hallId, [FromHeader(Name = "Accept")] string mediaType)
        {
            var foundHall = await _hallRepository.GetHallAsync(hallId);

            if (foundHall == null)
            {
                return NotFound();
            }

            var hallToReturn = Mapper.Map<HallDto>(foundHall);

            if (mediaType == "application/vnd.biob.json+hateoas")
            {
                var links = CreateLinksForHall(hallId);

                var linkedGenre = hallToReturn.ShapeData(null) as IDictionary<string, object>;

                linkedGenre.Add("links", links);

                return Ok(linkedGenre);
            }

            return Ok(hallToReturn);
        }

        [HttpPost]
        public async Task<IActionResult> CreateHallAsync([FromBody] HallToCreateDto hallToCreate)
        {

            var hallToAdd = Mapper.Map<Hall>(hallToCreate);
            _hallRepository.AddHall(hallToAdd);

            if (!await _hallRepository.SaveChangesAsync())
            {
                _logger.LogError("Saving changes to database while creating a hall failed");
            }

            return CreatedAtRoute("GetHall", new { hallId = hallToAdd.Id }, hallToAdd);
        }

        [HttpPut("{hallId}", Name = "UpdateHall")]
        [GuidCheckActionFilter(new string[] { "hallId" })]
        public async Task<IActionResult> UpdateHallByIdAsync([FromRoute] Guid hallId, [FromBody] HallToUpdateDto hallToUpdate)
        {
            var hallFromDb = await _hallRepository.GetHallAsync(hallId);

            // upserting if movie does not already exist
            if (hallFromDb == null)
            {
                var hallEntity = Mapper.Map<Hall>(hallToUpdate);
                hallEntity.Id = hallId;
                _hallRepository.AddHall(hallEntity);

                if (!await _hallRepository.SaveChangesAsync())
                {
                    _logger.LogError($"Upserting hall: {hallId} failed on save");
                }

                var hallToReturn = Mapper.Map<HallDto>(hallEntity);

                return CreatedAtRoute("GetHall", new { hallId = hallToReturn.Id }, hallToReturn);
            }

            Mapper.Map(hallToUpdate, hallFromDb);

            _hallRepository.UpdateHall(hallFromDb);

            if (!await _hallRepository.SaveChangesAsync())
            {
                _logger.LogError($"Updating hall: {hallId} failed on save");
            }

            return NoContent();

        }

        [HttpOptions]
        public IActionResult GetHallsOptions()
        {
            Response.Headers.Add("Allow", "GET,POST,OPTIONS");
            return Ok();
        }

        [HttpOptions("{hallId}")]
        public IActionResult GetHallOptions()
        {
            Response.Headers.Add("Allow", "GET,PATCH,PUT,OPTIONS");
            return Ok();
        }

        [HttpPatch("{hallId}", Name = "PartiallyUpdateHall")]
        [GuidCheckActionFilter(new string[] { "hallId" })]
        public async Task<IActionResult> PartiuallyUpdateHallByIdAsync([FromRoute] Guid hallId, JsonPatchDocument<HallToUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var hallFromDb = await _hallRepository.GetHallAsync(hallId);

            //  upserting if movie does not already exist
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
                    _logger.LogError($"Upserting hall: {hallId} failed on save");
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
                _logger.LogError($"Partially updating hall: {hallId} failed on save");
            }

            return NoContent();
        }

        [HttpDelete("{hallId}", Name = "DeleteHall")]
        [GuidCheckActionFilter(new string[] { "hallId" })]
        public async Task<IActionResult> DeleteHallById([FromRoute]Guid hallId)
        {
            var hallToDelete = await _hallRepository.GetHallAsync(hallId);

            if (hallToDelete == null)
            {
                return NotFound();
            }

            _hallRepository.DeleteHall(hallToDelete);

            if (!await _hallRepository.SaveChangesAsync())
            {
                _logger.LogError($"Deleting hall: {hallId} failed on save");
            }

            return NoContent();
        }

        private IEnumerable<LinkDto> CreateLinksForHall(Guid id)
        {
            var links = new List<LinkDto>
            {
                new LinkDto(_urlHelper.Link("GetHall", new { hallId = id }), "self", "GET"),
                new LinkDto(_urlHelper.Link("UpdateHall", new { hallId = id }), "update_hall", "PUT"),
                new LinkDto(_urlHelper.Link("PartiallyUpdateHall", new { hallId = id }), "partially_update_hall", "PATCH"),
                new LinkDto(_urlHelper.Link("DeleteHall", new { hallId = id }), "delete_hall", "DELETE")
            };

            return links;
        }

        private ExpandoObject CreateLinksForHalls(IEnumerable<HallDto> hallList)
        {
            var shapedHalls = hallList.ShapeData(null);
            var hallWithLinks = shapedHalls.Select(hall =>
            {
                var hallDictionary = hall as IDictionary<string, object>;
                var hallLinks = CreateLinksForHall((Guid)hallDictionary["Id"]);

                hallDictionary.Add("links", hallLinks);

                return hallDictionary;
            });
            var linkedCollection = new ExpandoObject();
            ((IDictionary<string, object>)linkedCollection).Add("halls", hallWithLinks);

            return linkedCollection;
        }

    }
}
