using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PagedList;

namespace BasicInfrastructure.Extensions
{
    public static class PagedListResultExtensions
    {
        public static PagedListResult<T> ToPagedListResult<T>(this IQueryable<T> list, int page, int perPage)
        {
            return PagedListResult<T>.Create(list, page, perPage);
        }

        public static async Task<PagedListResult<T>> ToPagedListResultAsync<T>(this Task<IQueryable<T>> list, int page, int perPage)
        {
            return await PagedListResult<T>.CreateAsync(list, page, perPage);
        }
    }

    public class PagedListResult<T>
    {
        public int Page { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int ItemCount { get; set; }
        public IPagedList<T> Items { get; set; }

        public PagedListResult(IEnumerable<T> list, int page, int pageSize) :
            this(list?.AsQueryable(), page, pageSize)
        {
        }
        public PagedListResult(IQueryable<T> list, int page, int pageSize)
        {
            Items = list.ToPagedList(page, pageSize);

            Page = Items.PageNumber;
            PageSize = Items.PageSize < 1 ? 1 : Items.PageSize;
            PageCount = Items.PageCount < 1 ? 1 : Items.PageCount;
            ItemCount = Items.TotalItemCount;
        }

        public static PagedListResult<T> Create(IQueryable<T> items, int page, int pageSize)
        {
            return new PagedListResult<T>(items, page, pageSize);
        }

        public static async Task<PagedListResult<T>> CreateAsync(Task<IQueryable<T>> itens, int pagina, int porPagina)
        {
            var i = await itens;
            return Create(i, pagina, porPagina);
        }
    }
}