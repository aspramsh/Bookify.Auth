using Bookify.Auth.DataAccess.Extensions;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Auth.DataAccess.DbContexts
{
    public class SnakeCasePersistedGrantDbContext : PersistedGrantDbContext
    {
        public SnakeCasePersistedGrantDbContext(DbContextOptions<PersistedGrantDbContext> options, OperationalStoreOptions StoreOptions) 
            : base(options, StoreOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.NamesToSnakeCase();
        }
    }
}
