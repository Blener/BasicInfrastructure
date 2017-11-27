using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using BasicInfrastructure.ParameterHelpers;

namespace BasicInfrastructure.Persistence
{
    public class NotTrackedRepository<T, D> : ReadOnlyRepository<T, D>
        where T : Entity
        where D : DbContext
    {
        public NotTrackedRepository(D context)
            : base(context)
        {
        }

        public new IQueryable<T> Items { get { lock (_locker) { return _context.Set<T>().AsNoTracking(); } } }
    }
}