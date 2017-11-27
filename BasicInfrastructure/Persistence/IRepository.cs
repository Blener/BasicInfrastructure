using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicInfrastructure.Persistence
{
    public interface IRepository<T> : IReadOnlyRepository<T>
        where T : Entity
    {
        T Add(T entity);
        ICollection<T> Add(ICollection<T> entity);
        T Update(T entity);
        bool Delete(T entity);
        bool Delete(int id);
        int Save();
    }
}