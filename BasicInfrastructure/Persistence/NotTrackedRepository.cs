using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using BasicInfrastructure.ParameterHelpers;

namespace BasicInfrastructure.Persistence
{
    public class NotTrackedRepository<T, D> : BaseRepository<T, D>, IRepository<T>
        where T : Entity
        where D : DbContext
    {
        public NotTrackedRepository(D context)
            : base(context)
        {
        }

        public IQueryable<T> Items { get { lock (_locker) { return _context.Set<T>().AsNoTracking(); } } }

        public virtual IQueryable<T> GetAll(IRequestParameters<T> request)
        {
            return request?.GetQuery(Items) ?? Items;
        }
    }
}