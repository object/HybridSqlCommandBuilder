using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SqlCommandBuilder
{
    class ValueFormatter
    {
        public string Format(IDictionary<string, object> keyValues, string separator = ",")
        {
            return string.Join(separator, keyValues.Select(x => string.Format("{0}={1}", x.Key, FormatContentValue(x.Value))));
        }

        public string Format(IEnumerable<object> keyValues, string separator = ",")
        {
            return string.Join(separator, keyValues.Select(FormatContentValue));
        }

        public string FormatContentValue(object value)
        {
            return FormatValue(value);
        }

        public string FormatQueryStringValue(object value)
        {
            return FormatValue(value);
        }

        public string FormatExpressionValue(object value)
        {
            return FormatValue(value);
        }

        private string FormatValue(object value)
        {
            return value == null ? "null"
                : value is CommandExpression ? (value as CommandExpression).Format()
                : value is string ? string.Format("'{0}'", value)
                : value is DateTime ? ((DateTime)value).FormatValue()
                : value is DateTimeOffset ? ((DateTimeOffset)value).FormatValue()
                : value is TimeSpan ? ((TimeSpan)value).FormatValue()
                : value is Guid ? ((Guid)value).FormatValue()
                : value is bool ? value.ToString().ToLower()
                : value is long ? ((long)value).FormatValue()
                : value is ulong ? ((ulong)value).FormatValue()
                : value is float ? ((float)value).ToString(CultureInfo.InvariantCulture)
                : value is double ? ((double)value).ToString(CultureInfo.InvariantCulture)
                : value is decimal ? ((decimal)value).FormatValue()
                : value.ToString();
        }
    }
}