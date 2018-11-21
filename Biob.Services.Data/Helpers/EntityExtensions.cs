using Biob.Services.Data.DtoModels;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;

namespace Biob.Services.Data.Helpers
{
    public static class EntityExtensions
    {
        public static ExpandoObject Shapedata(this MovieDto movie, string fields)
        {
            return Shapedata(movie, fields);
        }

        private static ExpandoObject ShapeData<T>(this T source, string fields)
        {
            if (source == null)
            {
                throw new ArgumentNullException("the source to shape cant be null");
            }

            List<PropertyInfo> propertyInfoList = new List<PropertyInfo>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                propertyInfoList.AddRange(propertyInfos);
            }
            else
            {
                string[] fieldsAftersplit = fields.Split(",");

                foreach (string field in fieldsAftersplit)
                {
                    string propertyName = field.Trim();

                    var propertyInfo = typeof(T).GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

                    if (propertyInfo == null)
                    {
                        throw new Exception($"property {propertyName} was not found on {typeof(T)}");
                    }

                    propertyInfoList.Add(propertyInfo);
                }
            }

            ExpandoObject dataShapeObject = new ExpandoObject();

            foreach (var propertyInfo in propertyInfoList)
            {
                var propertyValue = propertyInfo.GetValue(source);
                ((IDictionary<string, object>)dataShapeObject).Add(propertyInfo.Name, propertyValue);
            }

            return dataShapeObject;

        }
    }
}
