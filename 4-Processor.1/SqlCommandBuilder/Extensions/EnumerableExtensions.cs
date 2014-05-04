using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SqlCommandBuilder
{
    public static class EnumerableExtensions
    {
        public static T ToObject<T>(this IEnumerable<IDictionary<string, object>> source, bool dynamicObject = false)
            where T : class
        {
            if (source == null)
                return default(T);
            if (typeof(IDictionary<string, object>).IsAssignableFrom(typeof(T)))
                return source as T;

            return (T)ToObject(source, typeof(T), dynamicObject);
        }

        public static object ToObject(this IEnumerable<IDictionary<string, object>> source, Type type, bool dynamicObject = false)
        {
            if (source == null)
                return null;
            if (typeof(IDictionary<string, object>).IsAssignableFrom(type))
                return source;

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
                    element = item.ToObject(elementType);
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

        public static IEnumerable<IDictionary<string, object>> ToEnumerable(this object source,
            BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
        {
            if (source == null)
                return new List<Dictionary<string, object>>();
            if (source is IEnumerable<IDictionary<string, object>>)
                return source as IEnumerable<IDictionary<string, object>>;

            throw new NotImplementedException();
        }
    }
}