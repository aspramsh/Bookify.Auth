namespace IdentityServer4.Postgresql.Entities
{
    public class PostgresIdentityClaim : PostgresUserClaim
    {
        public PostgresIdentityResource IdentityResource { get; set; }
    }
}
