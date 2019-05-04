using AutoMapper;
using IdentityServer4.Models;
using IdentityServer4.Postgresql.Entities;

namespace IdentityServer4.Postgresql.Mappers
{
    public class PersistedGrantMapperProfile : Profile
    {
        public PersistedGrantMapperProfile()
        {
            CreateMap<PostgresPersistedGrant, PersistedGrant>(MemberList.Destination);

            // model to entity
            CreateMap<PersistedGrant, PostgresPersistedGrant>(MemberList.Source);
        }
       
       
    }
}
