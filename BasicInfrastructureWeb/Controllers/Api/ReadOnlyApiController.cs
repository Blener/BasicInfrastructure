using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Http.Routing;
using BasicInfrastructure.Extensions;
using BasicInfrastructure.ParameterHelpers;
using BasicInfrastructure.Persistence;
using BasicInfrastructure.Service;

namespace BasicInfrastructureWeb.Controllers.Api
{
    public class ReadOnlyApiController<T> : ApiController
        where T : Entity
    {
        private readonly IReadOnlyService<T> _service;

        public ReadOnlyApiController(IReadOnlyService<T> service) => _service = service;

        public virtual async Task<PagedResult<T>> Get([FromUri] RequestParameters<T> request)
        {
            return await _service.GetAll(request).ToPagedResultAsync(request);
        }

        public virtual async Task<T> Get(int id)
        {
            return await _service.Get(id);
        }

    }
}
