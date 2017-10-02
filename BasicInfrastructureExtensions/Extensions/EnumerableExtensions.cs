using System;
using System.Collections.Generic;
using System.Linq;

namespace BasicInfrastructureExtensions.Extensions
{

    public static class EnumerableExtensions
    {
        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> list, string sortExpression)
        {
            sortExpression += "";
            var parts = sortExpression.Split(' ');
            var descending = false;

            if (parts.Length > 0 && parts[0] != "")
            {
                var property = parts[0];

                if (parts.Length > 1)
                    descending = parts[1].ToLower().Contains("esc");

                var prop = typeof (T).GetProperty(property);

                if (prop == null)
                    throw new Exception("No property '" + property + "' in + " + typeof (T).Name + "'");

                return @descending
                           ? list.OrderByDescending(x => prop.GetValue(x, null))
                           : list.OrderBy(x => prop.GetValue(x, null));
            }

            return list;
        }

        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            if (list == null) return;

            foreach (var item in list)
            {
                action.Invoke(item);
            }
        }

        public static IEnumerable<T> Distinct<T, TKey>(this IEnumerable<T> @this, Func<T, TKey> keySelector)
        {
            return @this.GroupBy(keySelector).Select(grps => grps).Select(e => e.First());
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> items)
        {
            return items == null || !items.Any();
        }
    }
}