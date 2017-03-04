using System;
using System.Globalization;

namespace Starter.Examples.InterLisp.Classes
{
    public static class Converter
    {
        public static string Convert(object value, bool toUpper = false)
        {
            if (value == null)
                return "NIL";

            if (value is bool)
                if ((bool)value)
                    return "T";
                else
                    return "NIL";

            var s = value as string;
            if (s != null)
            {
                if (toUpper)
                    s = s.ToUpper();
                return '"' + s + '"';
            }
            
            value = CastValue(value);

            return System.Convert.ToString(value, CultureInfo.InvariantCulture);
        }

        private static object CastValue(object argument)
        {
            if (!(argument is double)) return argument;

            try
            {
                var argumentAsDouble = (double) argument; 
                var integralPart = (long) Math.Truncate(argumentAsDouble);

                if ((Math.Abs(argumentAsDouble) - Math.Abs((integralPart)) > 0))
                {
                    return argument;
                }

                var resultAsLong = System.Convert.ToInt64(argument);
                return resultAsLong;

            }
            catch
            { }

            return argument;
        }

    }
}
