using System;
using BasicInfrastructureExtensions.Helpers;

namespace BasicInfrastructureExtensions.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            return EnumHelper.GetDescription(value);
        }
    }
}