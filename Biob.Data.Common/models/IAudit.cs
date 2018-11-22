using System;

namespace Biob.Data.Common.models
{
    public interface IAudit
    {
        DateTimeOffset CreatedOn { get; set; }
        DateTimeOffset? ModifiedOn { get; set; }
    }
}
