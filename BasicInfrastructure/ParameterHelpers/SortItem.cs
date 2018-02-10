using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicInfrastructure.ParameterHelpers
{
    public class SortItem<T>  : ISortItem<T>
    {
        public string SortField { get; set; }
        public bool? SortDirection { get; set; }
        public int? Priotity { get; set; }

        public virtual IQueryable<T> GetQuery(IQueryable<T> query)
        {
            var prop = typeof(T).GetProperty(SortField);
            if (SortDirection ?? true)
                query = query.OrderBy(x => prop.GetValue(x));
            else
                query = query.OrderByDescending(x => prop.GetValue(x));

            return query;
        }
    }
}
