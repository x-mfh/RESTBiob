using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Biob.Web.Api.Filters
{
    public class GuidCheckActionFilter : ActionFilterAttribute
    {
        private readonly string[] _idsToCheck; 
        public GuidCheckActionFilter(string [] idsToCheck)
        {
            _idsToCheck = idsToCheck;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            foreach (var id in _idsToCheck)
            {
                context.ActionArguments.TryGetValue(id, out object  idToCheck);
                if (idToCheck != null && (Guid)idToCheck == Guid.Empty)
                {
                    context.Result = new BadRequestResult();
                }
            }
        }
    }
}
