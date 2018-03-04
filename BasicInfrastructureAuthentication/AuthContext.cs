using System.Data.Entity;
using BasicInfrastructure.Service;

namespace BasicInfrastructureAuthentication
{
    public class AuthContext : AppContext
    {
        public AuthContext(string nameOrConnectionString = "DefaultConnection")
            : base(nameOrConnectionString)
        {
        }
        public DbSet<ChangePasswordToken> ChangePasswordTokens { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Password> Passwords { get; set; }
        public DbSet<AuthToken> Tokens { get; set; }
    }
}
