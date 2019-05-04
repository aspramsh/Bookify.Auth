using AutoMapper;
using IdentityServer4.Models;
using IdentityServer4.Postgresql.Entities;
using System.Linq;

namespace IdentityServer4.Postgresql.Mappers
{
    public class IdentityResourceMapperProfile : Profile
    {
        public IdentityResourceMapperProfile()
        {
            // entity to model
            CreateMap<PostgresIdentityResource, IdentityResource>(MemberList.Destination)
                    .ForMember(x => x.UserClaims, opt => opt.MapFrom(src => src.UserClaims.Select(x => x.Type)));

            // model to entity
            CreateMap<IdentityResource, PostgresIdentityResource>(MemberList.Source)
                .ForMember(x => x.UserClaims, opts => opts.MapFrom(src => src.UserClaims.Select(x => new PostgresIdentityClaim { Type = x })));
        }
    }
}
