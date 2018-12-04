using System;
using System.Collections.Generic;
using Biob.Services.Web.PropertyMapping;
using System.Linq.Dynamic.Core;
using System.Linq;

namespace Biob.Services.Data.Helpers
{
    static class IQueryableExtensions
    {
        public static IQueryable<T> Applysort<T>(this IQueryable<T> source,
                                                 string orderBy, Dictionary<string, PropertyMappingValue> mappingDictionary)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (mappingDictionary == null)
            {
                throw new ArgumentNullException("mappingDictionary");
            }

            if (string.IsNullOrWhiteSpace(orderBy))
            {
                return source;
            }

            string[] orderByAfterSplit = orderBy.Split(",");
            
            
            foreach (string orderByClause in orderByAfterSplit.Reverse())
            {
                string trimmeedOrderByClause = orderByClause.Trim();

                bool orderDescending = trimmeedOrderByClause.EndsWith(" desc");

                int indexOfFirstSpace = trimmeedOrderByClause.IndexOf(" ");

                var propertyName = indexOfFirstSpace == -1 ? trimmeedOrderByClause : trimmeedOrderByClause.Remove(indexOfFirstSpace);

                if (!mappingDictionary.ContainsKey(propertyName))
                {
                    throw new ArgumentException($"key mapping for {propertyName} is missing");
                }

                var propertyMappingValue = mappingDictionary[propertyName];

                if (propertyMappingValue == null)
                {
                    throw new ArgumentNullException("propertyMappingValue");
                }

                foreach (string destinationProperty in propertyMappingValue.DestinationProperties.Reverse())
                {
                    if (propertyMappingValue.Revert)
                    {
                        orderDescending = !orderDescending;
                    }

                    source = source.OrderBy(destinationProperty + (orderDescending ? " descending" : " ascending"));
                }
            }

            return source;
        }
    }
}
