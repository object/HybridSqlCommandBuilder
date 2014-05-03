using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SqlCommandBuilder
{
    public static class EnumerableExtensions
    {
        internal static Func<IEnumerable<ResultRow>, ResultCollection> CreateDynamicResultCollection { get; set; }

        public static T ToObject<T>(this IEnumerable<ResultRow> source, bool dynamicObject = false)
            where T : class
        {
            if (source == null)
                return default(T);
            if (typeof(IDictionary<string, object>).IsAssignableFrom(typeof(T)))
                return source as T;
            if (typeof(T) == typeof(ResultCollection))
                return CreateResultCollection(source, dynamicObject) as T;

            return (T)ToObject(source, typeof(T), dynamicObject);
        }

        public static object ToObject(this IEnumerable<ResultRow> source, Type type, bool dynamicObject = false)
        {
            if (source == null)
                return null;
            if (typeof(IDictionary<string, object>).IsAssignableFrom(type))
                return source;
            if (type == typeof(ResultCollection))
                return CreateResultCollection(source, dynamicObject);

            var elementType = type.IsArray
                ? type.GetElementType()
                : type.IsGenericType && type.GetGenericArguments().Length == 1
                    ? type.GetGenericArguments()[0]
                    : null;
            var arrayValue = Array.CreateInstance(elementType, source.Count());

            var index = 0;
            foreach (var item in source)
            {
                object element = null;
                if (item != null)
                {
                    element = item.AsDictionary().ToObject(elementType, CommandProcessor.EnableDynamics);
                }
                arrayValue.SetValue(element, index++);
            }

            if (type.IsArray || type.IsInstanceOfType(arrayValue))
            {
                return arrayValue;
            }
            else
            {
                var typedef = typeof(IEnumerable<>);
                var enumerableType = typedef.MakeGenericType(elementType);
                var ctor = type.GetConstructor(new[] { enumerableType });
                return ctor != null ? ctor.Invoke(new object[] { arrayValue }) : null;
            }
        }

        public static IEnumerable<ResultRow> ToEnumerable(this object source,
            BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
        {
            if (source == null)
                return new List<ResultRow>();
            if (source is IEnumerable<ResultRow>)
                return source as IEnumerable<ResultRow>;
            if (source is ResultCollection)
                return (List<ResultRow>)(source as ResultCollection);

            throw new NotImplementedException();
        }

        private static ResultCollection CreateResultCollection(IEnumerable<ResultRow> source, bool dynamicObject = false)
        {
            return dynamicObject && CreateDynamicResultCollection != null ?
                CreateDynamicResultCollection(source) :
                new ResultCollection(source);
        }
    }
}