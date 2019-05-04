using System;
using System.Collections.Generic;

namespace IdentityServer4.Postgresql.Entities
{
    public class PostgresApiResource : EntityKey
    {
        public PostgresApiResource()
        {
            Id = Guid.NewGuid().ToString();
        }
        
        public bool Enabled { get; set; } = true;
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public List<PostgresApiSecret> Secrets { get; set; }
        public List<PostgresApiScope> Scopes { get; set; }
        public List<PostgresApiResourceClaim> UserClaims { get; set; }
    }
}
