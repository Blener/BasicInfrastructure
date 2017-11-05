using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using BasicInfrastructureAuthentication;

namespace BasicInfrastructureWeb.Controllers.Api
{
    public class AuthController: ApiController
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }
        //Login
        public virtual async Task<IAuthToken> Post(LoginCredentials user)
        {
            return await _service.Authenticate(user);
        }
        //Renew Token
        public virtual async Task<IAuthToken> Put(AuthToken token)
        {
            return await _service.Authenticate(token);
        }
    }
}
