using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BasicInfrastructureExtensions.Helpers
{
    public static class ReflectionHelper
    {
        public static IEnumerable<T> GetEnumerableOfType<T>(params object[] constructorArgs) where T : class
        {
            return
                Assembly.GetAssembly(typeof (T))
                        .GetTypes()
                        .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof (T)))
                        .Select(type => (T) Activator.CreateInstance(type, constructorArgs))
                        .ToList();
        }
    }
}