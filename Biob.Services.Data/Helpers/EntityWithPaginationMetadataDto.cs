using System.Collections.Generic;

namespace Biob.Services.Data.Helpers
{
    public class EntityWithPaginationMetadataDto<T>
    {
        public PaginationMetadata Metadata { get; set; }
        public IEnumerable<T> Records { get; set; }
        public EntityWithPaginationMetadataDto(PaginationMetadata metadata, IEnumerable<T> records)
        {
            Metadata = metadata;
            Records = records;
        }
    }
}
