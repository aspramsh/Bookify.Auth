using AutoMapper;
using IdentityServer4.Models;
using IdentityServer4.Postgresql.Entities;

namespace IdentityServer4.Postgresql.Mappers
{

    public static class ApiResourceMappers
    {
        static ApiResourceMappers()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<ApiResourceMapperProfile>())
                .CreateMapper();
        }
        public static IMapper Mapper { get; }

        public static ApiResource ToModel(this PostgresApiResource resource)
        {
            return resource == null ? null : Mapper.Map<ApiResource>(resource);
        }

        public static PostgresApiResource ToEntity(this ApiResource resource)
        {
            return resource == null ? null : Mapper.Map<PostgresApiResource>(resource);
        }
    }
}
