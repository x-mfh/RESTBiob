using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;

namespace Biob.Services.Data.Helpers
{
    public static class EntityExtensions
    {

        public static ExpandoObject ShapeData<T>(this T source, string fields)
        {
            if (source == null)
            {
                throw new ArgumentNullException("the source to shape cant be null");
            }

            List<PropertyInfo> propertyInfoList = new List<PropertyInfo>();


            var dataShapedObject = new ExpandoObject();

            if (string.IsNullOrWhiteSpace(fields))
            {
                PropertyInfo[] propertyInfos = typeof(T)
                        .GetProperties(BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    var propertyValue = propertyInfo.GetValue(source);

                    ((IDictionary<string, object>)dataShapedObject).Add(propertyInfo.Name, propertyValue);
                }

                return dataShapedObject;
            }

            string[] fieldsAfterSplit = fields.Split(',');

            foreach (var field in fieldsAfterSplit)
            {
                var propertyName = field.Trim();

                var propertyInfo = typeof(T)
                    .GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (propertyInfo == null)
                {
                    throw new Exception($"Property {propertyName} wasn't found on {typeof(T)}");
                }

                var propertyValue = propertyInfo.GetValue(source);

                ((IDictionary<string, object>)dataShapedObject).Add(propertyInfo.Name, propertyValue);
            }

            return dataShapedObject;
        }
    }
}