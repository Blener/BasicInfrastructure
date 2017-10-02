using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace BasicInfrastructure.Extensions
{
    public class HandleExceptionsAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            //TODO Responses padrões quando lanãr exceção nos controllers
            //actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }
    }
}