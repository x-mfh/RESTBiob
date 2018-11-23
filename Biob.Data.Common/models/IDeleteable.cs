using System;

namespace Biob.Data.Common.Models
{
    public interface IDeleteable
    {
        bool IsDeleted { get; set; }
        DateTimeOffset? DeletedOn { get; set; }
    }
}
