using AutoMapper;
using IdentityServer4.Models;
using IdentityServer4.Postgresql.Entities;

namespace IdentityServer4.Postgresql.Mappers
{
    public static class ClientMapper
    {
        static ClientMapper()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<ClientMapperProfile>())
               .CreateMapper();
        }
        public static IMapper Mapper { get; }
        public static Client ToModel(this PostgresClient client)
        {
            return Mapper.Map<PostgresClient, Client>(client);
        }

        public static PostgresClient ToEntity(this Client client)
        {
            return Mapper.Map<Client, PostgresClient>(client);
        }
    }
}
