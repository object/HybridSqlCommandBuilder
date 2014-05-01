using System;
using System.Globalization;

namespace SqlCommandBuilder
{
    internal static class ValueFormatterExtensions
    {
        public static string FormatValue(this decimal number)
        {
            var value = number.ToString("F", CultureInfo.InvariantCulture);
            return string.Format(@"{0}M", value);
        }

        public static string FormatValue(this long number)
        {
            var value = number.ToString(CultureInfo.InvariantCulture);
            return string.Format(@"{0}L", value);
        }

        public static string FormatValue(this ulong number)
        {
            var value = number.ToString(CultureInfo.InvariantCulture);
            return string.Format(@"{0}L", value);
        }

        public static string FormatValue(this Guid guid)
        {
            var value = guid.ToString();
            return string.Format(@"guid'{0}'", value);
        }

        public static string FormatValue(this DateTime dateTime)
        {
            var value = dateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffffffZ");
            return string.Format(@"datetime'{0}'", value);
        }

        public static string FormatValue(this DateTimeOffset dateTimeOffset)
        {
            var value = dateTimeOffset.ToString("yyyy-MM-ddTHH:mm:ss.fffffffZ");
            return string.Format(@"datetimeoffset'{0}'", value);
        }

        public static string FormatValue(this TimeSpan timeSpan)
        {
            var value = timeSpan.ToString();
            return string.Format(@"time'{0}'", value);
        }
    }
}