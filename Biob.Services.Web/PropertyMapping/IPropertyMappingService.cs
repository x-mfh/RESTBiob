using System.Collections.Generic;

namespace Biob.Services.Web.PropertyMapping
{
    public interface IPropertyMappingService
    {
        bool ValidMappingExistsFor<Tsource, TDestination>(string fields);
        Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>();
        void AddPropertyMapping<Tsource, TDestination>(Dictionary<string, PropertyMappingValue> propertyMappingToAdd);


    }
}
