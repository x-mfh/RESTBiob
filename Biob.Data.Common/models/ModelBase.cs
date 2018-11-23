using System;
using System.ComponentModel.DataAnnotations;

namespace Biob.Data.Common.Models
{
    public abstract class ModelBase<TKey> : IAudit
    {
        [Key]
        public TKey Id { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
    }
}
