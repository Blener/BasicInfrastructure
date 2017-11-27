using System.Linq;
using System.Threading.Tasks;
using BasicInfrastructure.ParameterHelpers;

namespace BasicInfrastructure.Persistence
{
    public interface IReadOnlyRepository<T> 
        where T : Entity
    {
        IQueryable<T> Items { get; }
        IQueryable<T> GetAll(IRequestParameters<T> request);
        T Get(int id);
    }
}