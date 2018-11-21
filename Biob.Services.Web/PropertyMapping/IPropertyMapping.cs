using System.Collections.Generic;

namespace Biob.Services.Web.PropertyMapping
{
    public interface IPropertyMapping
    {
        Dictionary<string, PropertyMappingValue> _mappingdictionary { get; }
    }
}
