using System.Linq;
using System.Threading.Tasks;

namespace BasicInfrastructure.Service
{
    public interface IReadOnlyService<T> 
    {
        Task<IQueryable<T>> GetAll();
        Task<T> Get(int id);
    }
}