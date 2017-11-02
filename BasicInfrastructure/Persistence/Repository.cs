using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace BasicInfrastructure.Persistence
{
    public class Repository<T, D> : BaseRepository<T, D>, IRepository<T>
        where T : Entity
        where D : DbContext
    {
        public Repository(D context)
            : base(context)
        {
        }

        public IQueryable<T> Items { get { lock (_locker) { return _context.Set<T>(); } } }

        public virtual async Task<IQueryable<T>> GetAll()
        {
            return Items.OrderBy(x => x.Id).AsQueryable();
        }
    }
}