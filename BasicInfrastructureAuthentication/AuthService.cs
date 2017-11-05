using System;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using BasicInfrastructure.Persistence;
using BasicInfrastructure.Service;
using BasicInfrastructureExtensions.Extensions;

namespace BasicInfrastructureAuthentication
{
    public class AuthService : ReadOnlyService<User>, IAuthService
    {
        private readonly ITokenService _tokenService;

        public AuthService(IRepository<User> repository, ITokenService tokenService) : base(repository)
        {
            _tokenService = tokenService;
        }

        private async Task<User> GetByLogin(string login)
        {
            return (await GetAll()).SingleOrDefault(
                x => x.Login.Equals(login, StringComparison.InvariantCultureIgnoreCase));
        }

        public async Task<IAuthToken> Authenticate(IAuthable user)
        {
            var loginUser = await GetByLogin(user.Login);
            if (loginUser?.Password == null)
                throw new AuthenticationException();

            if (loginUser.Password.Matches(user.Password))
                return await Authenticate(await CreateToken(loginUser));

            throw new AuthenticationException();
        }

        public async Task<IAuthToken> Authenticate(string token)
        {
            if (token.IsNullOrEmpty())
                throw new AuthenticationException();

            return await Authenticate(await _tokenService.GetByToken(token));
        }
        public async Task<IAuthToken> Authenticate(IAuthToken token)
        {
            if(token == null)
                throw new AuthenticationException();

            var dbToken = (await _tokenService.GetByToken(token.Token.ToString()));

            if (dbToken?.IsValid() ?? false)
                return await RenewToken(dbToken, token);

            throw new UnauthorizedAccessException();
        }
        public async Task<IAuthToken> Authenticate(IAuthToken token, string controller, string action)
        {
            var newToken = await Authenticate(token);
            return await _tokenService.Authorize(newToken, controller, action);

        }
        private async Task<IAuthToken> CreateToken(User user)
        {
            var token = new AuthToken
            {
                RenewInterval = 30,
                CreateTime = DateTime.Now,
                LastTouch = DateTime.Now,
                Token = Guid.NewGuid(),
                UserId = user.UserId
            };
            token = await _tokenService.Add(token);
            return await Authenticate(token);
        }
        private async Task<IAuthToken> RenewToken(AuthToken dbToken, IAuthToken sessionToken)
        {
            dbToken.LastTouch = DateTime.Now;
            dbToken.RenewInterval = sessionToken.RenewInterval < 10 ? dbToken.RenewInterval : sessionToken.RenewInterval;
            return await _tokenService.Update(dbToken);
        }
    }
}
