using System.Linq;
using System.Threading.Tasks;

namespace BasicInfrastructure.Service
{
    public interface IService<T> : IReadOnlyService<T>
    {
        Task<T> Add(T entity);
        Task<T> Update(T entity, int? id = null);
        Task<bool> Delete(T entity);
        Task<bool> Delete(int id);
    }
}