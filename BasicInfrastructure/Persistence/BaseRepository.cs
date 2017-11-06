using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace BasicInfrastructure.Persistence
{
    public class BaseRepository<T, D> : IBaseRepository<T>
        where T : Entity
        where D : DbContext
    {
        protected static object _locker = new object();
        protected readonly D _context;

        public BaseRepository(D context)
        {
            this._context = context;
        }

        public T Get(int id)
        {
            return _context.Set<T>().SingleOrDefault(x => x.Id == id);
        }

        public T Add(T entity)
        {
            var item = _context.Set<T>().Add(entity);
            Save();
            return item;
        }

        public ICollection<T> Add(ICollection<T> entity)
        {
            var list = new Collection<T>();
            foreach (var item in entity)
            {
                list.Add(Add(item));
            }
            Save();
            return list;
        }

        public T Update(T entity)
        {
            var item = _context.Entry(entity);
            var returnItem = item.Entity;

            if (item.State == EntityState.Detached)
            {
                var set = _context.Set<T>();
                var attachedEntity = set.Local.SingleOrDefault(x => x.Id == entity.Id);

                if (attachedEntity != null)
                {
                    var attachedEntry = _context.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(entity);
                    returnItem = attachedEntity;
                }
                else
                {
                    item.State = EntityState.Modified;
                    returnItem = item.Entity;
                }
            }
            Save();
            return returnItem;
        }

        public bool Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            Save();
            return true;
        }

        public bool Delete(int id)
        {
            var item = _context.Set<T>().SingleOrDefault(x => x.Id == id);
            if (item == null)
                return false;

            return Delete(item);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            GC.ReRegisterForFinalize(this);
        }
    }
}