using System.Reflection;

namespace Biob.Services.Web.PropertyMapping
{
    public class TypeHelperService : ITypeHelperService
    {
        public bool TypeHasProperties<T>(string fields)
        {
            //  TODO: check if i have misremembered this
            if (string.IsNullOrWhiteSpace(fields))
            {
                return true;
            }

            string[] fieldsAftersplit = fields.Split(",");

            foreach (string field in fieldsAftersplit)
            {
                string propertyName = field.Trim();

                var propertyInfo = typeof(T).GetProperty(propertyName,
                                                        BindingFlags.IgnoreCase
                                                        | BindingFlags.Public
                                                        | BindingFlags.Instance);


                if (propertyInfo == null)
                {
                    return false;
                }
            }

            return true;

        }
    }
}
