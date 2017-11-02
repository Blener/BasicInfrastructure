using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasicInfrastructure.ParameterHelpers;
using BasicInfrastructure.Persistence;
using PagedList;

namespace BasicInfrastructure.Extensions
{
    public static class PagedListResultExtensions 
    {
        public static PagedListResult<T> ToPagedListResult<T>(this IQueryable<T> list, RequestParameters<T> request) where T : Entity
        {
            return PagedListResult<T>.Create(list, request);
        }

        public static async Task<PagedListResult<T>> ToPagedListResultAsync<T>(this Task<IQueryable<T>> list, RequestParameters<T> request) where T : Entity
        {
            return await PagedListResult<T>.CreateAsync(list, request);
        }
    }

    public class PagedListResult<T> where T: Entity
    {
        public IQueryable<T> Items { get; set; }
        public IRequestParameters<T> Request { get; set; }

        public PagedListResult(IEnumerable<T> list, IRequestParameters<T> request) :
            this(list?.AsQueryable(), request)
        {
        }
        public PagedListResult(IQueryable<T> list, IRequestParameters<T> request)
        {
            Items = list;
            Request = request;
        }

        public static PagedListResult<T> Create(IQueryable<T> items, RequestParameters<T> request)
        {
            return new PagedListResult<T>(items, request);
        }

        public static async Task<PagedListResult<T>> CreateAsync(Task<IQueryable<T>> itens, RequestParameters<T> request)
        {
            var i = await itens;
            return Create(i, request);
        }
    }
}