using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicInfrastructure.Persistence
{
    public interface IBaseRepository<T>
        where T : Entity
    {
        T Get(int id);
        T Add(T entity);
        ICollection<T> Add(ICollection<T> entity);
        T Update(T entity);
        bool Delete(T entity);
        bool Delete(int id);
        int Save();
    }
}