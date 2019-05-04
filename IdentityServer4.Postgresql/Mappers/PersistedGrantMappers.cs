using AutoMapper;
using IdentityServer4.Models;
using IdentityServer4.Postgresql.Entities;

namespace IdentityServer4.Postgresql.Mappers
{
    public static class PersistedGrantMappers
    {
        static PersistedGrantMappers()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<PersistedGrantMapperProfile>())
                 .CreateMapper();
        }
        public static IMapper Mapper { get; }

        public static PersistedGrant ToModel(this PostgresPersistedGrant token)
        {
            if (token == null) return null;

            return Mapper.Map<PostgresPersistedGrant, PersistedGrant>(token);
        }

        public static PostgresPersistedGrant ToEntity(this PersistedGrant token)
        {
            if (token == null) return null;

            return Mapper.Map<PersistedGrant, PostgresPersistedGrant>(token);
        }

        public static void UpdateEntity(this PersistedGrant token, PostgresPersistedGrant target)
        {
            Mapper.Map(token, target);
        }
    }
}
