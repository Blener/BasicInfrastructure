using System.Linq;
using System.Threading.Tasks;
using BasicInfrastructure.ParameterHelpers;

namespace BasicInfrastructure.Persistence
{
    public interface IRepository<T> : IBaseRepository<T>
        where T : Entity
    {
        IQueryable<T> GetAll(IRequestParameters<T> request);
    }
}