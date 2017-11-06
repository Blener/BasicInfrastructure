using System.Linq;
using System.Threading.Tasks;
using BasicInfrastructure.ParameterHelpers;
using BasicInfrastructure.Persistence;

namespace BasicInfrastructure.Service
{
    public class ReadOnlyService<T> : IReadOnlyService<T>
        where T : Entity
    {
        protected IRepository<T> Repository;

        public ReadOnlyService(IRepository<T> repository)
        {
            Repository = repository;
        }

        public virtual async Task<IQueryable<T>> GetAll(IRequestParameters<T> request = null)
        {
            return Repository.GetAll(request);
        }

        public virtual async Task<T> Get(int id)
        {
            return Repository.Get(id);
        }
    }
}