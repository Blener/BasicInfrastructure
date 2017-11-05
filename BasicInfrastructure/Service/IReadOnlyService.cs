using System.Linq;
using System.Threading.Tasks;
using BasicInfrastructure.ParameterHelpers;
using BasicInfrastructure.Persistence;

namespace BasicInfrastructure.Service
{
    public interface IReadOnlyService<T> where T : Entity
    {
        Task<IQueryable<T>> GetAll(IRequestParameters<T> request = null);
        Task<T> Get(int id);
    }
}