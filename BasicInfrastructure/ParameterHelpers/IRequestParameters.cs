using System.Collections.Generic;
using System.Linq;
using BasicInfrastructure.Persistence;

namespace BasicInfrastructure.ParameterHelpers
{
    public interface IRequestParameters<T> where T : Entity
    {
        int PageId { get; set; }
        int PerPage { get; set; }
        string SortField { get; set; }
        bool? SortDirection { get; set; }
        int ItemCount { get; set; }
        int PageCount { get; set; }

        List<Filter<T>> Filters { get; set; }

        IQueryable<T> GetQuery(IQueryable<T> query);
    }
}
