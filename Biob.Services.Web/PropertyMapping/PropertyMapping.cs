using System.Collections.Generic;

namespace Biob.Services.Web.PropertyMapping
{
    class PropertyMapping<TSource, TDestination> : IPropertyMapping
    {
        public Dictionary<string, PropertyMappingValue> _mappingdictionary { get; private set; }

        public PropertyMapping(Dictionary<string, PropertyMappingValue> mappingDictionary)
        {
            _mappingdictionary = mappingDictionary;
        }
    }
}
