using System.Linq;
using BasicInfrastructure.Persistence;

namespace BasicInfrastructure.ParameterHelpers
{

    public interface IFilter<T> where T : Entity
    {
        string Field { get; set; }
        string Operation { get; set; }
        string Value { get; set; }

        IQueryable<T> GetQuery(IQueryable<T> query);
    }
}
