using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using BasicInfrastructure.Persistence;
using BasicInfrastructure.Service;
using BasicInfrastructureAuthentication.Exception;
using BasicInfrastructureExtensions.Extensions;

namespace BasicInfrastructureAuthentication
{
    public class AuthService : ReadOnlyService<User>, IAuthService
    {
        private readonly ITokenService _tokenService;
        private readonly IService<ChangePasswordToken> _changePasswordService;
        private readonly IService<Password> _passwordService;

        public AuthService(IRepository<User> repository, ITokenService tokenService,
            IService<ChangePasswordToken> changePasswordService, IService<Password> passwordService) : base(repository)
        {
            _tokenService = tokenService;
            _changePasswordService = changePasswordService;
            _passwordService = passwordService;
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
            if (token == null)
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

        public async Task<bool> ChangePasswordRequest(string login)
        {
            var user = await GetByLogin(login);
            if (user == null)
                throw new AuthenticationException();

            var oldToken = (await _changePasswordService.GetAll()).SingleOrDefault(x => x.User.Id == user.Id);
            if (oldToken != null) {
                if (!oldToken.IsValid())
                    await _changePasswordService.Delete(oldToken);
                else
                    //TODO Trocar por reenviar email
                    throw new ChangePasswordTokenAlreadyExistsException();
            }

            var pwdToken = new ChangePasswordToken
                {
                    User = user,
                    CreationTime = DateTime.Now,
                    Token = Guid.NewGuid()
                };
            
            pwdToken = await _changePasswordService.Add(pwdToken);
            //TODO Send email

            return true;
        }

        public async Task<IAuthToken> ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            if (changePasswordViewModel.Password != changePasswordViewModel.PasswordConfirmation)
                throw new AuthenticationException();

            var token = (await _changePasswordService.GetAll()).SingleOrDefault(
                x => x.Token.ToString() == changePasswordViewModel.Token.ToString());
            if (token == null || !token.User.Login.EqualsIgnoreCase(changePasswordViewModel.Login))
                throw new AuthenticationException();

            if (!token.IsValid())
            {
                await _changePasswordService.Delete(token);
                throw new ChangePasswordTokenExpiredException();
            }

            var user = token.User;
            user.Password.SetValue(changePasswordViewModel.Password);
            user.Password.CreationDate = DateTime.Now;

            await _passwordService.Update(user.Password);
            var newLoginToken = await CreateToken(user);

            await _changePasswordService.Delete(token);

            return newLoginToken;
        }
    }
}
