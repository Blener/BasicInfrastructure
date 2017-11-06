using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using BasicInfrastructure.Persistence;
using BasicInfrastructure.Service;
using BasicInfrastructureExtensions.Extensions;

namespace BasicInfrastructureAuthentication
{
    public class TokenService: BaseService<AuthToken>, ITokenService
    {
        public TokenService(IRepository<AuthToken> repository) : base(repository)
        {
        }

        public override async Task<AuthToken> Add(AuthToken entity)
        {
            await RemoveCurrentTokens(entity);
            return await base.Add(entity);
        }

        private async Task RemoveCurrentTokens(IAuthToken entity)
        {
            var currentTokens = (await GetAll()).Where(x => x.RenewInterval > 0 && x.UserId == entity.UserId);
            await currentTokens.ForEachAsync(async x => await Delete(x));
        }

        public async Task<AuthToken> Authorize(IAuthToken token, string controller, string action)
        {
            //TODO ProfileMapping authorization
            throw new NotImplementedException();
        }

        public async Task<AuthToken> GetByToken(string token)
        {
            return (await GetAll()).SingleOrDefault(x => x.Token.ToString() == token);
        }
    }
}
