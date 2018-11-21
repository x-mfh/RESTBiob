using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Biob.Web.Filters
{
    public class MovieParameterValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!Guid.TryParse(context.RouteData.Values["movieId"].ToString(), out Guid movieId))
            {
                context.Result = new BadRequestResult();
            }

            if (movieId == Guid.Empty)
            {
                context.Result = new BadRequestResult();
            }
        }
        
    }
}
