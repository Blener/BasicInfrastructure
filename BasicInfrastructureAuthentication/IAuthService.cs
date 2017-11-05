using System;
using System.Threading.Tasks;
using BasicInfrastructure.Service;

namespace BasicInfrastructureAuthentication
{
    public interface IAuthService : IReadOnlyService<User>
    {
        Task<IAuthToken> Authenticate(string token);
        Task<IAuthToken> Authenticate(IAuthable user);
        Task<IAuthToken> Authenticate(IAuthToken token);
        Task<IAuthToken> Authenticate(IAuthToken token, string controller, string action);
        Task<bool> ChangePasswordRequest(string login);
        Task<IAuthToken> ChangePassword(ChangePasswordViewModel changePasswordViewModel);
    }
}