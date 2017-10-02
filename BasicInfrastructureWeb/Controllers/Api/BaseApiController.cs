using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using BasicInfrastructure.Extensions;
using BasicInfrastructure.Persistence;
using BasicInfrastructure.Service;

namespace BasicInfrastructureWeb.Controllers.Api
{
    public class BaseApiController<T> : ReadOnlyApiController<T>
        where T : Entity
    {
        private readonly IService<T> _service;
        public BaseApiController(IService<T> service) : base(service) => _service = service;

        public virtual async Task<JsonResult<T>> Post([FromBody] T value)
        {
            return Json(await _service.Add(value));
        }

        public virtual async Task<JsonResult<T>> Put(int id, [FromBody] T value)
        {
            return Json(await _service.Update(value, id));
        }

        public virtual async Task<JsonResult<bool>> Delete(int id)
        {
            return Json(await _service.Delete(id));
        }

    }
}
