using System;
using System.Text.RegularExpressions;

namespace amplitude.tool.Utilities
{
    public static class CastExtension
    {
        public static object CastToValue(string value)
        {
            if (Regex.IsMatch(value, "[Tt]rue|[Ff]alse"))
                return Convert.ToBoolean(value);

            if (Regex.IsMatch(value, "^[0-9]*$"))
                return Convert.ToInt64(value);
            
            if (Regex.IsMatch(value, "^[+-]?([0-9]+([.][0-9]*)?|[.][0-9]+)$"))
                return Convert.ToDouble(value);

            return value;
        }
    }
}