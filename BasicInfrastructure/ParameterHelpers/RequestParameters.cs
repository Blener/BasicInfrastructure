using System;
using System.Collections.Generic;
using System.Linq;
using BasicInfrastructureExtensions.Extensions;

namespace BasicInfrastructure.ParameterHelpers
{
    public class RequestParameters<T> : IRequestParameters<T>
    {
        public int? PageId { get; set; }
        public int? PerPage { get; set; }
        public List<SortItem<T>> SortItems { get; set; }
        public List<Filter<T>> Filters { get; set; }

        public int? ItemCount { get; set; }
        public int? PageCount { get; set; }

        public RequestParameters()
        {
            SortItems = new List<SortItem<T>>();
            Filters = new List<Filter<T>>();
        }

        public virtual IQueryable<T> GetQuery(IQueryable<T> query, bool countItems = false)
        {
            if (query == null)
                return null;

            query = GetFiltersQuery(query);
            query = GetSortQuery(query);
            query = GetPaginationQuery(query, countItems);

            return query;
        }

        protected virtual IQueryable<T> GetFiltersQuery(IQueryable<T> query)
        {
            if (Filters == null || Filters.All(x => x == null))
                return query;

            return Filters.Aggregate(query, (x, item) => item.GetQuery(x));
        }

        protected virtual IQueryable<T> GetSortQuery(IQueryable<T> query)
        {
            if (SortItems == null || !SortItems.Any())
                return query;

            return SortItems
                .OrderBy(x => x.Priotity ?? 0)
                .Aggregate(query, (x, item) => item.GetQuery(x));

        }

        protected virtual IQueryable<T> GetPaginationQuery(IQueryable<T> query, bool countItems = false)
        {
            PageId = PageId == null || PageId < 0 ? 0 : PageId;
            PerPage = PerPage == null || PerPage <= 0 ? 10 : PerPage;

            ItemCount = null;
            PageCount = null;
            if (countItems)
            {
                ItemCount = query.Count();
                PageCount = (int)Math.Ceiling(((decimal)ItemCount) / PerPage.Value);
            }

            return query.Skip(PageId.Value * PerPage.Value).Take(PerPage.Value);
        }
    }
}
