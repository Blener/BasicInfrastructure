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

        public virtual async Task<JsonResult<PagedResult<T>>> Get([FromUri] RequestParameters<T> request)
        {
            return Json(await _service.GetAll(request).ToPagedResultAsync(request));
        }

        public virtual async Task<JsonResult<T>> Get(int? id)
        {
            return Json(await _service.Get(id.Value));
        }

    }
}
