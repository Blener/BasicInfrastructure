using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace BasicInfrastructureWeb.Filters
{
    public class HandleExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is AuthenticationException || 
                context.Exception is UnauthorizedAccessException)
                context.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            if (context.Exception is NotImplementedException)
                context.Response = new HttpResponseMessage(HttpStatusCode.NotImplemented);

        }
    }
}
