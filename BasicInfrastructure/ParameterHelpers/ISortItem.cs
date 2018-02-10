using System.Linq;
using BasicInfrastructure.Persistence;

namespace BasicInfrastructure.ParameterHelpers
{

    public interface ISortItem<T>
    {
        string SortField { get; set; }
        bool? SortDirection { get; set; }
        int? Priotity { get; set; }

        IQueryable<T> GetQuery(IQueryable<T> query);
    }
}
