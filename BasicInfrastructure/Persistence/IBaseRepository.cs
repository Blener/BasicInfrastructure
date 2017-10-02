using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicInfrastructure.Persistence
{
    public interface IBaseRepository<T>
        where T : Entity
    {
        Task<T> Get(int id);
        Task<T> Add(T entity);
        Task<ICollection<T>> Add(ICollection<T> entity);
        Task<T> Update(T entity);
        Task<bool> Delete(T entity);
        Task<bool> Delete(int id);
        Task<int> Save();
    }
}