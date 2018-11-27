using System.Collections.Generic;

namespace Biob.Services.Data.Helpers
{
    public class LinkedCollectionWrapperDto<T> : LinkedBaseDto
    {
        public IEnumerable<T> Resources { get; set; }

        public LinkedCollectionWrapperDto(IEnumerable<T> resources)
        {
            Resources = resources;
        }
    }
}
