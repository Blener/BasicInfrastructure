using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
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

        public virtual async Task<JsonResult<PagedListResult<T>>> Get(IRequestParameters<T> request)
        {
            return Json(await _service.GetAll(null).ToPagedListResultAsync(request.Page, request.PerPage));
        }

        public virtual async Task<JsonResult<T>> Get(int? id)
        {
            return Json(await _service.Get(id.Value));
        }

    }
}
