using System;
using System.Collections.Generic;
using System.Dynamic;

namespace BasicInfrastructureExtensions.Extensions
{
    public static class ExpandoObjectExtensions
    {
        public static T TryGetValue<T>(this ExpandoObject src, string field, T defaultValue = default(T))
        {
            object result;
            var dic = src as IDictionary<string, object>;
            if (dic == null || !dic.ContainsKey(field) || !dic.TryGetValue(field, out result))
                return defaultValue;

            try
            {
                var typedResult = (T)result;
                return typedResult;
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
    }
}
