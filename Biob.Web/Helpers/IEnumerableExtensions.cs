using System.Collections.Generic;
using System.Text;

namespace Biob.Web.Helpers
{
    public static class IEnumerableExtensions
    {
        public static string ConvertIEnumerableToString<T>(this IEnumerable<T> listOfString)
        {
            
            var sb = new StringBuilder();
            foreach (var name in listOfString)
            {
                //  appends name to end of string
                sb.Append($"{name}, ");
            }
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 2, 2);
            }
            return sb.ToString();
        }
    }
}
