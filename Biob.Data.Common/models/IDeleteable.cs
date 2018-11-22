using System;

namespace Biob.Data.Common.models
{
    public interface IDeleteable
    {
        bool IsDeleted { get; set; }
        DateTimeOffset? DeletedOn { get; set; }
    }
}
