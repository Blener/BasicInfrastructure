using System.Linq;
using System.Threading.Tasks;
using BasicInfrastructure.ParameterHelpers;
using BasicInfrastructure.Persistence;

namespace BasicInfrastructure.Extensions
{
    public static class PagedResultExtensions
    {
        public static PagedResult<T> ToPagedResult<T>(this IQueryable<T> list, RequestParameters<T> request) where T : Entity
        {
            return new PagedResult<T>(list, request);
        }

        public static async Task<PagedResult<T>> ToPagedResultAsync<T>(this Task<IQueryable<T>> list, RequestParameters<T> request) where T : Entity
        {
            var syncList = await list;
            return await Task.Run(() => ToPagedResult(syncList, request));
        }
    }
}