using System;
using System.Collections.Generic;
using System.Linq;
using BasicInfrastructure.Persistence;

namespace BasicInfrastructure.ParameterHelpers
{
    public class RequestParameters<T> : IRequestParameters<T> where T : Entity
    {
        public int PageId { get; set; }
        public int PerPage { get; set; }
        public string SortField { get; set; }
        public bool? SortDirection { get; set; }
        public List<Filter<T>> Filters { get; set; }

        public int ItemCount { get; set; }
        public int PageCount { get; set; }

        public virtual IQueryable<T> GetQuery(IQueryable<T> query)
        {
            if (query == null)
                return null;

            query = GetFiltersQuery(query);
            query = GetSortQuery(query);
            query = GetPaginationQuery(query);

            return query;
        }

        protected virtual IQueryable<T> GetFiltersQuery(IQueryable<T> query)
        {
            if (Filters == null || !Filters.Any())
                return query;

            return Filters.Aggregate(query, (x, item) => item.GetQuery(x));
        }

        protected virtual IQueryable<T> GetSortQuery(IQueryable<T> query)
        {
            if (SortField == null)
                return query;

            var prop = typeof(T).GetProperty(SortField);
            if (SortDirection ?? true)
                query = query.OrderBy(x => prop.GetValue(x));
            else
                query = query.OrderByDescending(x => prop.GetValue(x));

            return query;
        }

        protected virtual IQueryable<T> GetPaginationQuery(IQueryable<T> query)
        {
            PageId = PageId < 0 ? 0 : PageId;
            PerPage = PerPage <= 0 ? 10 : PerPage;

            ItemCount = query.Count();
            PageCount = (int)Math.Ceiling(((decimal)ItemCount) / PerPage);
            return query.Skip(PageId * PerPage).Take(PerPage);
        }
    }
}
