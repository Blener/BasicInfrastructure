using BasicInfrastructure.Persistence;
using BasicInfrastructure.Service;

namespace BasicInfrastructureWeb.Controllers
{
    public class BaseController<T> : System.Web.Mvc.Controller
        where T : Entity
    {
        protected readonly IService<T> Service;

        public BaseController(IService<T> service) => Service = service;

    }
}
