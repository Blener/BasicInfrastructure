using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace BasicInfrastructureWeb.Filters
{
    public class AuthorizeAttribute : AuthorizationFilterAttribute
    {
        public override async Task OnAuthorizationAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            //Model for implementing authorization logic
        }

        public override void OnAuthorization(HttpActionContext filterContext)
        {
            OnAuthorizationAsync(filterContext, new CancellationToken()).RunSynchronously();
        }
    }
}
