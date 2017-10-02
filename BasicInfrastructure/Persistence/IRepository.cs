using System.Linq;
using System.Threading.Tasks;

namespace BasicInfrastructure.Persistence
{
    public interface IRepository<T> : IBaseRepository<T>
        where T : Entity
    {
        Task<IQueryable<T>> GetAll();
    }
}