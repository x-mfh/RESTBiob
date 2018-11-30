using Biob.Services.Data.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Biob.Web.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class ApiRootController : ControllerBase
    {
        private readonly IUrlHelper _urlHelper;

        public ApiRootController(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }

        [HttpGet(Name = "GetApiRoot")]
        public IActionResult GetApiRoot([FromHeader(Name = "Accept")] string mediaType)
        {
            if (mediaType == "application/vnd.biob.json+hateoas")
            {
                var links = new List<LinkDto>()
                {
                    new LinkDto(_urlHelper.Link("GetApiRoot", new { }), "self", "GET"),
                    new LinkDto(_urlHelper.Link("GetMovies", new { }), "get_all_movies", "GET"),
                    new LinkDto(_urlHelper.Link("GetApiRoot", new { }), "create_movie", "POST"),
                };

                return Ok(links);
            }

            return NoContent();
        }
    }
}
