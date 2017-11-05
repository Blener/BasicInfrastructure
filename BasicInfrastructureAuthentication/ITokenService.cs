using System.Threading.Tasks;
using BasicInfrastructure.Service;

namespace BasicInfrastructureAuthentication
{
    public interface ITokenService : IService<AuthToken>
    {
        Task<AuthToken> Authorize(IAuthToken token, string controller, string action);
        Task<AuthToken> GetByToken(string token);
    }
}
