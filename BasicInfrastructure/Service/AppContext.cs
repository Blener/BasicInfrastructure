using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Infrastructure.Pluralization;

namespace BasicInfrastructure.Service
{
    public class AppContext : DbContext
    {
        public AppContext(string nameOrConnectionString = "DefaultConnection") : base(nameOrConnectionString)
        {
            //Database.Connection.ConnectionString = connectionString;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            const string prefix = "";
            modelBuilder.Types().Configure(x => x.ToTable(GetTableName(x.ClrType, prefix)));
        }

        private static string GetTableName(Type type, string prefix)
        {
            var pluralizationService = DbConfiguration.DependencyResolver.GetService<IPluralizationService>();
            var result = pluralizationService.Pluralize(type.Name);
            return prefix + result;
        }

    }
}