using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterLispApp.Classes.Actions
{
    public static class MathOperations
    {
        public static object Add(object left, object right)
        {
            var result = 0.0;

            if (string.IsNullOrEmpty(left.ToString()) & string.IsNullOrEmpty(right.ToString()))
                return 0;

            double leftValue, rightValue;
            var leftConverted = (double.TryParse(left.ToString(), out leftValue));
            if (leftConverted)
                result += leftValue;

            var rightConverted = (double.TryParse(right.ToString(), out rightValue));
            if (rightConverted)
                result += rightValue;

            long resultAsLong;
            double resultAsDouble;
            IFormatProvider formatProvider = new CultureInfo("en-US");

            if (long.TryParse(result.ToString(CultureInfo.InvariantCulture), out resultAsLong))
                return resultAsLong;

            if (double.TryParse(result.ToString(CultureInfo.InvariantCulture), NumberStyles.AllowDecimalPoint,
                formatProvider, out resultAsDouble))
                return resultAsDouble;

            return result;
        }

        public static object Subst(object left, object right)
        {
            var result = 0.0;

            if (string.IsNullOrEmpty(left.ToString()) & string.IsNullOrEmpty(right.ToString()))
                return 0;

            double leftValue, rightValue;
            var leftConverted = (double.TryParse(left.ToString(), out leftValue));
            if (leftConverted)
                result = leftValue;

            var rightConverted = (double.TryParse(right.ToString(), out rightValue));
            if (rightConverted)
                result -= rightValue;

            long resultAsLong;
            double resultAsDouble;
            IFormatProvider formatProvider = new CultureInfo("en-US");
            if (long.TryParse(result.ToString(CultureInfo.InvariantCulture), out resultAsLong))
                return resultAsLong;

            if (double.TryParse(result.ToString(CultureInfo.InvariantCulture), NumberStyles.AllowDecimalPoint,
                formatProvider, out resultAsDouble))
                return resultAsDouble;

            return result;
        }

        public static object Multiply(object left, object right)
        {
            var result = 1.0;

            if (string.IsNullOrEmpty(left.ToString()) & string.IsNullOrEmpty(right.ToString()))
                return 0;

            double leftValue, rightValue;
            var leftConverted = (double.TryParse(left.ToString(), out leftValue));
            if (leftConverted)
                result *= leftValue;

            var rightConverted = (double.TryParse(right.ToString(), out rightValue));
            if (rightConverted)
                result *= rightValue;

            long resultAsLong;
            double resultAsDouble;
            IFormatProvider formatProvider = new CultureInfo("en-US");
            if (long.TryParse(result.ToString(CultureInfo.InvariantCulture), out resultAsLong))
                return resultAsLong;

            if (double.TryParse(result.ToString(CultureInfo.InvariantCulture), NumberStyles.AllowDecimalPoint,
                formatProvider, out resultAsDouble))
                return resultAsDouble;

            return result;
        }

        public static object Div(object left, object right)
        {
            var result = 0.0;

            if (string.IsNullOrEmpty(left.ToString()) & string.IsNullOrEmpty(right.ToString()))
                return 0;

            double leftValue, rightValue;
            var leftConverted = (double.TryParse(left.ToString(), out leftValue));
            if (leftConverted)
            {
                result = leftValue;

                if (leftValue == 0.0)
                    return result;
            }

            var rightConverted = (double.TryParse(right.ToString(), out rightValue));
            if (rightConverted)
            {
                if (rightValue == 0.0)
                {
                    throw new DivideByZeroException("Attempt to divide by zero.");
                }

                result /= rightValue;
            }

            return result;
        }
    }
}
