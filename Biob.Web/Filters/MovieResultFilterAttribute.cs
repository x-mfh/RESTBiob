using Biob.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;

namespace Biob.Web.Filters
{
    public class MovieResultFilterAttribute : ResultFilterAttribute
    {
        public async override Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var resultFromControllerMethod = (ObjectResult)context.Result;

            if (resultFromControllerMethod?.Value == null
                || resultFromControllerMethod.StatusCode < 200
                || resultFromControllerMethod.StatusCode >= 300)
            {
                await next();
                return;
            }
            if (resultFromControllerMethod.Value is IEnumerable<Movie>)
            {
                resultFromControllerMethod.Value = Mapper.Map<IEnumerable<MovieDto>>(resultFromControllerMethod.Value);
            }
            else
            {
                resultFromControllerMethod.Value = Mapper.Map<MovieDto>(resultFromControllerMethod.Value);
            }

            await next();
        }
    }
}
