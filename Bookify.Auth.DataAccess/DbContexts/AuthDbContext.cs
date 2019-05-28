using Bookify.Auth.DataAccess.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Auth.DataAccess.DbContexts
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.NamesToSnakeCase();
        }
    }
}
