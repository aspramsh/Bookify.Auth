using AutoMapper;
using IdentityServer4.Models;
using IdentityServer4.Postgresql.Entities;

namespace IdentityServer4.Postgresql.Mappers
{
    public static class IdentityResourceMappers
    {
        static IdentityResourceMappers()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<IdentityResourceMapperProfile>())
                .CreateMapper();
        }
        public static IMapper Mapper { get; }

        public static IdentityResource ToModel(this PostgresIdentityResource identityResource)
        {
            if (identityResource == null) return null;

            return Mapper.Map<PostgresIdentityResource, IdentityResource>(identityResource);
        }

        public static PostgresIdentityResource ToEntity(this IdentityResource identityResource)
        {
            if (identityResource == null) return null;

            return Mapper.Map<IdentityResource, PostgresIdentityResource>(identityResource);
        }
    }
}
