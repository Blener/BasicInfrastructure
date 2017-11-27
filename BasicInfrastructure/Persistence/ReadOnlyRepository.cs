using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using BasicInfrastructure.ParameterHelpers;

namespace BasicInfrastructure.Persistence
{
    public class ReadOnlyRepository<T, D> : IReadOnlyRepository<T>
        where T : Entity
        where D : DbContext
    {
        protected static object _locker = new object();
        protected readonly D _context;

        public ReadOnlyRepository(D context)
        {
            _context = context;
        }

        public IQueryable<T> Items { get { lock (_locker) { return _context.Set<T>(); } } }

        public virtual IQueryable<T> GetAll(IRequestParameters<T> request = default)
        {
            return request?.GetQuery(Items) ?? Items;
        }
        public T Get(int id)
        {
            return Items.SingleOrDefault(x => x.Id == id);
        }
    }
}