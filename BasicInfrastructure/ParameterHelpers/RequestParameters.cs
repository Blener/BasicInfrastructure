using System.Collections.Generic;
using System.Linq;
using BasicInfrastructure.Persistence;

namespace BasicInfrastructure.ParameterHelpers
{
    public abstract class RequestParameters<T> : IRequestParameters<T> where T : Entity
    {
        public int Page { get; set; }
        public int PerPage { get; set; }
        public string SortField { get; set; }
        public bool? SortDirection { get; set; }
        public IList<IFilter<T>> Filters { get; set; }
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
            return Filters.Aggregate(query, (x, item) => item.GetQuery(x));
        }

        protected virtual IQueryable<T> GetSortQuery(IQueryable<T> query)
        {
            var prop = typeof(T).GetProperty(SortField);
            if (SortDirection ?? true)
                query = query.OrderBy(x => prop.Name);
            else
                query = query.OrderByDescending(x => prop.Name);

            return query;
        }

        protected virtual IQueryable<T> GetPaginationQuery(IQueryable<T> query)
        {
            return query.Skip(Page * PerPage).Take(PerPage);
        }
    }
}
