using System;

namespace Biob.Data.Common.models
{
    public class DeleteableModelBase<TKey> : ModelBase<TKey>, IDeleteable
    {
        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedOn { get; set; }
    }
}
