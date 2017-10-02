using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BasicInfrastructureExtensions.Extensions
{
    #region

    

    #endregion

    public static class ClassExtensions
    {
        public static T Extend<T>(this T target, T source, bool overrideValues = false) where T : class
        {
            if (source == null)
                return target;
            var properties = GetTypeProperties<T>(target);

            foreach (var propertyInfo in properties)
            {
                if (TypeHasValue(target, propertyInfo))
                {
                    if (overrideValues && TypeHasValue(source, propertyInfo))
                    {
                        CopyValue<T>(target, source, propertyInfo);
                        continue;
                    }
                    else
                        continue;
                }
                else
                    CopyValue<T>(target, source, propertyInfo);
            }

            return target;
        }

        private static void CopyValue<T>(T target, T source, PropertyInfo propertyInfo) where T : class
        {
            dynamic sourceValue;
            sourceValue = propertyInfo.GetValue(source, null);
            propertyInfo.SetValue(target, sourceValue, null);
        }

        private static bool TypeHasValue<T>(T type, PropertyInfo propertyInfo) where T : class
        {
            try
            {
                var typeValue = propertyInfo.GetValue(type, null);
                var defaultValue = propertyInfo.PropertyType.GetDefault();

                if (typeValue != null && typeValue.GetType().IsArray)
                {
                    var arrayTypeValue = (Array)typeValue;
                    var arrayDefaultValue = (Array)defaultValue;

                    return arrayTypeValue.Length != arrayDefaultValue.Length;
                }

                return typeValue != null && !typeValue.Equals(defaultValue);
            }
            catch
            {
                return false;
            }
        }

        private static IEnumerable<PropertyInfo> GetTypeProperties<T>(T type) where T : class
        {
            var properties = type.GetType().GetProperties().Where(pi => pi.CanRead && pi.CanWrite);
            return properties;
        }

        public static object GetDefault(this Type type)
        {
            if (type.IsArray)
                return Array.CreateInstance(type, 0);

            // If no Type was supplied, if the Type was a reference type, or if the Type was a System.Void, return null
            if (type == null || !type.IsValueType || type == typeof(void))
                return null;

            // If the supplied Type has generic parameters, its default value cannot be determined
            if (type.ContainsGenericParameters)
            {
                throw new ArgumentException(
                    "{" + MethodBase.GetCurrentMethod() + "} Error:\n\nThe supplied value type <" + type +
                    "> contains generic parameters, so the default value cannot be retrieved");
            }

            // If the Type is a primitive type, or if it is another publicly-visible value type (i.e. struct), return a 
            //  default instance of the value type
            if (type.IsPrimitive || !type.IsNotPublic)
            {
                try
                {
                    return Activator.CreateInstance(type);
                }
                catch (Exception e)
                {
                    throw new ArgumentException(
                        "{" + MethodBase.GetCurrentMethod() +
                        "} Error:\n\nThe Activator.CreateInstance method could not " +
                        "create a default instance of the supplied value type <" + type +
                        "> (Inner Exception message: \"" + e.Message + "\")", e);
                }
            }

            // Fail with exception
            throw new ArgumentException("{" + MethodBase.GetCurrentMethod() + "} Error:\n\nThe supplied value type <" +
                                        type +
                                        "> is not a publicly-visible type, so the default value cannot be retrieved");
        }
    }
}