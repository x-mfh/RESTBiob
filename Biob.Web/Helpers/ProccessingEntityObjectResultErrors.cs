using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace Biob.Web.Api.Helpers
{
    public class ProccessingEntityObjectResultErrors : ObjectResult
    {
        public ProccessingEntityObjectResultErrors(ModelStateDictionary value) : base (new SerializableError(value))
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            StatusCode = 422;
        }
    }
}
