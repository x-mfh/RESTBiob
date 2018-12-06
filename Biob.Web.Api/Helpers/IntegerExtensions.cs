using System;

namespace Biob.Web.Api.Helpers
{
    public static class IntegerExtensions
    {
        public static string CalculateFromSeconds(this int seconds)
        {
            TimeSpan ts = TimeSpan.FromSeconds(seconds);

            return ts.ToString("hh\\:mm");
        }
    }
}
