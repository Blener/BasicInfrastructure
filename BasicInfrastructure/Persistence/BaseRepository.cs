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

        public async Task<T> Get(int id)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T> Add(T entity)
        {
            var item = _context.Set<T>().Add(entity);
            await Save();
            return item;
        }

        public async Task<ICollection<T>> Add(ICollection<T> entity)
        {
            var list = new Collection<T>();
            foreach (var item in entity)
            {
                list.Add(await Add(item));
            }
            await Save();
            return list;
        }

        public async Task<T> Update(T entity)
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
            await Save();
            return returnItem;
        }

        public async Task<bool> Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            await Save();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var item = await _context.Set<T>().SingleOrDefaultAsync(x => x.Id == id);
            if (item == null)
                return false;

            return await Delete(item);
        }

        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            GC.ReRegisterForFinalize(this);
        }
    }
}