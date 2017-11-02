using System.Linq;
using System.Threading.Tasks;
using BasicInfrastructure.Persistence;

namespace BasicInfrastructure.Service
{
    public interface IService<T> : IReadOnlyService<T> where T : Entity
    {
        Task<T> Add(T entity);
        Task<T> Update(T entity, int? id = null);
        Task<bool> Delete(T entity);
        Task<bool> Delete(int id);
    }
}