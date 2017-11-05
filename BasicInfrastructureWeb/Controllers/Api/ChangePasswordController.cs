using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using BasicInfrastructureAuthentication;

namespace BasicInfrastructureWeb.Controllers.Api
{
    public class ChangePasswordController : ApiController
    {
        private readonly IAuthService _service;

        public ChangePasswordController(IAuthService service)
        {
            _service = service;
        }
        //Request
        public virtual async Task<bool> Get(string login)
        {
            return await _service.ChangePasswordRequest(login);
        }
        //Change
        public virtual async Task<IAuthToken> Put(ChangePasswordViewModel changePasswordViewModel)
        {
            return await _service.ChangePassword(changePasswordViewModel);
        }
    }
}
