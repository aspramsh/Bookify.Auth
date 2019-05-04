using AutoMapper;
using IdentityServer4.Models;
using IdentityServer4.Postgresql.Entities;
using System.Linq;

namespace IdentityServer4.Postgresql.Mappers
{
    public class ApiResourceMapperProfile : Profile
    {
        public ApiResourceMapperProfile()
        {
            CreateMap<PostgresApiResource, ApiResource>(MemberList.Destination)
                .ForMember(x => x.ApiSecrets, opt => opt.MapFrom(src => src.Secrets.Select(x => x)))
                .ForMember(x => x.Scopes, opt => opt.MapFrom(src => src.Scopes.Select(x => x)))
                .ForMember(x => x.UserClaims, opts => opts.MapFrom(src => src.UserClaims.Select(x => x.Type)));
            CreateMap<PostgresApiSecret, Secret>(MemberList.Destination);
            CreateMap<PostgresApiScope, Scope>(MemberList.Destination)
                .ForMember(x => x.UserClaims, opt => opt.MapFrom(src => src.UserClaims.Select(x => x.Type)));

            // model to entity
            CreateMap<ApiResource, PostgresApiResource>(MemberList.Source)
                .ForMember(x => x.Secrets, opts => opts.MapFrom(src => src.ApiSecrets.Select(x => x)))
                .ForMember(x => x.Scopes, opts => opts.MapFrom(src => src.Scopes.Select(x => x)))
                .ForMember(x => x.UserClaims, opts => opts.MapFrom(src => src.UserClaims.Select(x => new PostgresApiResourceClaim { Type = x })));
            CreateMap<Secret, PostgresApiSecret>(MemberList.Source);
            CreateMap<Scope, PostgresApiScope>(MemberList.Source)
                .ForMember(x => x.UserClaims, opts => opts.MapFrom(src => src.UserClaims.Select(x => new PostgresApiScopeClaim { Type = x })));
        }
    }
}
