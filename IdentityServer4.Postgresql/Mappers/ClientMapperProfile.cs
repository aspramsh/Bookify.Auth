using AutoMapper;
using IdentityServer4.Models;
using IdentityServer4.Postgresql.Entities;
using System.Linq;

namespace IdentityServer4.Postgresql.Mappers
{
    public class ClientMapperProfile : Profile
    {
        public ClientMapperProfile()
        {
            CreateMap<PostgresClient, Client>(MemberList.Destination)
                .ForMember(x => x.AllowedGrantTypes,
                    opt => opt.MapFrom(src => src.AllowedGrantTypes.Select(x => x.GrantType)))
                .ForMember(x => x.RedirectUris, opt => opt.MapFrom(src => src.RedirectUris.Select(x => x.RedirectUri)))
                .ForMember(x => x.PostLogoutRedirectUris,
                    opt => opt.MapFrom(src => src.PostLogoutRedirectUris.Select(x => x.PostLogoutRedirectUri)))
                .ForMember(x => x.AllowedScopes, opt => opt.MapFrom(src => src.AllowedScopes.Select(x => x.Scope)))
                .ForMember(x => x.ClientSecrets, opt => opt.MapFrom(src => src.ClientSecrets.Select(x => x)))
                .ForMember(x => x.Claims, opt => opt.MapFrom(src => src.Claims.Select(x => new System.Security.Claims.Claim(x.Type, x.Value))))
                .ForMember(x => x.IdentityProviderRestrictions,
                    opt => opt.MapFrom(src => src.IdentityProviderRestrictions.Select(x => x.Provider)))
                .ForMember(x => x.AllowedCorsOrigins,
                    opt => opt.MapFrom(src => src.AllowedCorsOrigins.Select(x => x.Origin)));

            CreateMap<PostgresClientSecret, Secret>(MemberList.Destination)
                .ForMember(dest => dest.Type, opt => opt.Condition(srs => srs != null));

            // model to entity
            CreateMap<Client, PostgresClient>(MemberList.Source)
                .ForMember(x => x.AllowedGrantTypes,
                    opt => opt.MapFrom(src => src.AllowedGrantTypes.Select(x => new PostgresClientGrantType { GrantType = x })))
                .ForMember(x => x.RedirectUris,
                    opt => opt.MapFrom(src => src.RedirectUris.Select(x => new PostgresClientRedirectUri { RedirectUri = x })))
                .ForMember(x => x.PostLogoutRedirectUris,
                    opt =>
                        opt.MapFrom(
                            src =>
                                src.PostLogoutRedirectUris.Select(
                                    x => new PostgresClientPostLogoutRedirectUri { PostLogoutRedirectUri = x })))
                .ForMember(x => x.AllowedScopes,
                    opt => opt.MapFrom(src => src.AllowedScopes.Select(x => new PostgresClientScope { Scope = x })))
                .ForMember(x => x.Claims,
                    opt => opt.MapFrom(src => src.Claims.Select(x => new PostgresClientClaim { Type = x.Type, Value = x.Value })))
                .ForMember(x => x.IdentityProviderRestrictions,
                    opt =>
                        opt.MapFrom(
                            src => src.IdentityProviderRestrictions.Select(x => new PostgresClientIdPRestriction { Provider = x })))
                .ForMember(x => x.AllowedCorsOrigins,
                    opt => opt.MapFrom(src => src.AllowedCorsOrigins.Select(x => new PostgresClientCorsOrigin { Origin = x })));
            CreateMap<Secret, PostgresClientSecret>(MemberList.Source);
        }
    }
}
