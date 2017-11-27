using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace BasicInfrastructure.Persistence
{
    public class Repository<T, D> : ReadOnlyRepository<T, D>, IRepository<T>
        where T : Entity
        where D : DbContext
    {

        public Repository(D context) : base(context)
        {
        }


        public T Add(T entity)
        {
            lock (_locker)
            {
                var item = _context.Set<T>().Add(entity);
                Save();
                return item;
            }
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
            lock (_locker)
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
        }

        public bool Delete(T entity)
        {
            lock (_locker)
            {
                _context.Set<T>().Remove(entity);
                Save();
                return true;
            }
        }

        public bool Delete(int id)
        {
            lock (_locker)
            {
                var item = _context.Set<T>().SingleOrDefault(x => x.Id == id);
                if (item == null)
                    return false;

                return Delete(item);
            }
        }

        public int Save()
        {
            lock (_locker)
            {
                return _context.SaveChanges();
            }
        }

        public void Dispose() =>
            GC.ReRegisterForFinalize(this);

    }
}