using System;
using System.Linq;
using System.Reflection;

namespace BasicInfrastructureExtensions.Extensions
{
    public static class PropertyInfoExtensions
    {
        public static bool AttributeExists<T>(this PropertyInfo propertyInfo) where T : class
        {
            var attribute = propertyInfo.GetCustomAttributes(typeof (T), false).FirstOrDefault() as T;
            return attribute != null;
        }

        public static bool AttributeExists<T>(this Type type) where T : class
        {
            var attribute = type.GetCustomAttributes(typeof (T), false).FirstOrDefault() as T;
            return attribute != null;
        }

        public static T GetAttribute<T>(this Type type) where T : class
        {
            return type.GetCustomAttributes(typeof (T), false).FirstOrDefault() as T;
        }

        public static T GetAttribute<T>(this PropertyInfo propertyInfo) where T : class
        {
            return propertyInfo.GetCustomAttributes(typeof (T), false).FirstOrDefault() as T;
        }
    }
}