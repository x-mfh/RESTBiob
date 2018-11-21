using System;
using System.Collections.Generic;
using System.Linq;

namespace Biob.Services.Web.PropertyMapping
{
    public class PropertyMappingService : IPropertyMappingService
    {
        private readonly IList<IPropertyMapping> propertyMappings = new List<IPropertyMapping>();
        public Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>()
        {
            var matchMapping = propertyMappings.OfType<PropertyMapping<TSource, TDestination>>();

            if (matchMapping.Count() == 1)
            {
                return matchMapping.First()._mappingdictionary;
            }

            throw new Exception($"Can't find property mapping instance for <{typeof(TSource)}>");
        }

        public void AddPropertyMapping<Tsource, TDestination>(Dictionary<string, PropertyMappingValue> propertyMappingToAdd)
        {
            propertyMappings.Add(new PropertyMapping<Tsource,TDestination>(propertyMappingToAdd));
        }
        public bool ValidMappingExistsFor<Tsource, TDestination>(string fields)
        {
            var propertyMapping = GetPropertyMapping<Tsource, TDestination>();

            if(string.IsNullOrWhiteSpace(fields))
            {
                return true;
            }

            string[] fieldsAftersplit = fields.Split(",");

            foreach (string field in fieldsAftersplit)
            {
                string trimmedField = field.Trim();

                int indexOfFirstSpace = trimmedField.IndexOf(" ");

                string propertyName = indexOfFirstSpace == -1 ? trimmedField : trimmedField.Remove(indexOfFirstSpace);

                if (!propertyMapping.ContainsKey(propertyName))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
