﻿using System.Linq;
using System.Threading.Tasks;
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

        public virtual async Task<IQueryable<T>> GetAll()
        {
            return await Repository.GetAll();
        }

        public virtual async Task<T> Get(int id)
        {
            return await Repository.Get(id);
        }
    }
}