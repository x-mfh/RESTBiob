using System.Collections.Generic;

namespace Biob.Services.Data.Helpers
{
    public abstract class LinkedBaseDto
    {
        public List<LinkDto> Links { get; set; } = new List<LinkDto>();
    }
}
