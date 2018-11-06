using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SqlCommandBuilder
{
    public static class TypeExtensions
    {
        public static IEnumerable<PropertyInfo> GetDeclaredProperties(this Type type)
        {
            return type.GetTypeInfo().DeclaredProperties;
        }

        public static PropertyInfo GetDeclaredProperty(this Type type, string propertyName)
        {
            return type.GetTypeInfo().GetDeclaredProperty(propertyName);
        }

        public static IEnumerable<FieldInfo> GetDeclaredFields(this Type type)
        {
            return type.GetTypeInfo().DeclaredFields;
        }

        public static MethodInfo GetDeclaredMethod(this Type type, string methodName)
        {
            return type.GetTypeInfo().GetDeclaredMethod(methodName);
        }

        public static IEnumerable<ConstructorInfo> GetDeclaredConstructors(this Type type)
        {
            return type.GetTypeInfo().DeclaredConstructors;
        }

        public static ConstructorInfo GetDefaultConstructor(this Type type)
        {
            return type.GetTypeInfo().DeclaredConstructors.SingleOrDefault(x => x.GetParameters().Length == 0);
        }

        public static TypeAttributes GetTypeAttributes(this Type type)
        {
            return type.GetTypeInfo().Attributes;
        }

        public static Type[] GetGenericTypeArguments(this Type type)
        {
            return type.GetTypeInfo().GenericTypeArguments;
        }

        public static bool IsTypeAssignableFrom(this Type type, Type otherType)
        {
            return type.GetTypeInfo().IsAssignableFrom(otherType.GetTypeInfo());
        }

        public static bool HasCustomAttribute(this Type type, Type attributeType, bool inherit)
        {
            return type.GetTypeInfo().GetCustomAttribute(attributeType, inherit) != null;
        }

        public static bool IsGeneric(this Type type)
        {
            return type.GetTypeInfo().IsGenericType;
        }

        public static bool IsValue(this Type type)
        {
            return type.GetTypeInfo().IsValueType;
        }
    }
}
