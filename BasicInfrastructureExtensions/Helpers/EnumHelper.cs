using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using BasicInfrastructureExtensions.Extensions;

namespace BasicInfrastructureExtensions.Helpers
{
    public class EnumHelper
    {
        public static string GetDescription(object value)
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            if (name == null) return string.Empty;
            var field = type.GetField(name);
            if (field != null)
            {
                var attr = GetDescriptionAttribute(field);
                if (attr != null) return attr.Description;
            }
            return name.Replace("_", " ");
        }

        private static DescriptionAttribute GetDescriptionAttribute(FieldInfo field)
        {
            return
                Attribute.GetCustomAttribute(field, typeof (DescriptionAttribute)) as DescriptionAttribute ??
                Attribute.GetCustomAttribute(field, typeof(LocalizedDescriptionAttribute)) as DescriptionAttribute;
        }

        public static string GetDescription(Type enumType, string value)
        {
            return GetDescription(Enum.Parse(enumType, value));
        }
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static IEnumerable<string> EnumToList<TEnum>(bool retrieveDescription = true)
            where TEnum : struct
        {
            var names = Enum.GetNames(typeof(TEnum));
            if (!retrieveDescription)
                return names.AsEnumerable();

            var result = names.Select(x =>
            {
                TEnum e;
                Enum.TryParse(x, out e);
                return (e as Enum).GetDescription();
            });

            return result;
        }
    }
}