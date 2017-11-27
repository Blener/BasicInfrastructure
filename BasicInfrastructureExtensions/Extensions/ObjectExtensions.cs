using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BasicInfrastructureExtensions.Extensions
{
    public static class ObjectExtensions
    {
        public static IDictionary<string, object> ToDictionary(this object @object)
        {
            var dictionary = new Dictionary<string, object>(StringComparer.CurrentCultureIgnoreCase);
            if (@object != null)
            {
                foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(@object))
                {
                    dictionary.Add(property.Name.Replace("_", "-"), property.GetValue(@object));
                }
            }
            return dictionary;
        }
        public static IEnumerable<KeyValuePair<string, object>> ToKeyValuePairs(this object @object)
        {
            if (@object != null)
                foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(@object))
                    yield return new KeyValuePair<string, object>(property.Name.Replace("_", "-"), property.GetValue(@object));
        }

        public static object GetValue(this object model, string property)
        {
            return model.GetType().GetProperty(property).GetValue(model, new object[0]);
        }
    }
}