using System;

namespace Biob.Data.Common.Models
{
    public interface IAudit
    {
        DateTimeOffset CreatedOn { get; set; }
        DateTimeOffset? ModifiedOn { get; set; }
    }
}
