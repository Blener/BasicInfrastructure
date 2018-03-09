using System.Collections.Generic;
using System.Linq;
using BasicInfrastructure.Persistence;

namespace BasicInfrastructure.ParameterHelpers
{

    public class PagedResult<T> where T: Entity
    {
        public dynamic Items { get; set; }
        public IRequestParameters<T> Request { get; set; }

        public PagedResult(IQueryable<T> list, IRequestParameters<T> request)
        {
            Items = list.ToList();
            Request = request;
        }
    }
}