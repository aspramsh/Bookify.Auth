using Bookify.Auth.Business.Models.Request;
using Bookify.Auth.Business.Models.Response;
using Bookify.Auth.Infrastructure.Enums;
using Bookify.Auth.Infrastructure.Helpers.Http;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Bookify.Auth.Business.ProfileValidator
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IHttpClientHelper _httpClientHelper;

        public const string UserId = "NameIdentifier";
        public const string AuthorizationMethod = "password";

        public ResourceOwnerPasswordValidator(IHttpClientHelper httpClientHelper)
        {
            _httpClientHelper = httpClientHelper;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            try
            {
                RequestLoginModel request = new RequestLoginModel
                {
                    Username = context.UserName,
                    Password = context.Password
                };

                ResponseAuthorizationModel response =
                    await _httpClientHelper.PostAsync<RequestLoginModel, ResponseAuthorizationModel>(
                        $"api/v1.0/Users/authorization",
                        request);
                if (response != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(UserId, response.UserId.ToString()),
                        new Claim(JwtClaimTypes.Email, response.Email),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean)
                    };

                    foreach (var claim in response.Claims)
                    {
                        claims.Add(new Claim(claim.Type, claim.Name));
                    }

                    context.Result = new GrantValidationResult(response.UserId.ToString(), AuthorizationMethod, claims);

                    await Task.FromResult(context.Result);
                }
                else
                {
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, ErrorCode.NotFound.ToString());
                    await Task.FromResult(context.Result);
                }
            }
            catch (HttpResponseException e)
            {
                switch (e.StatusCode)
                {
                    case HttpStatusCode.BadRequest:
                        context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, ErrorCode.Lock.ToString());
                        break;
                    default:
                        context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, e.StatusCode.ToString());
                        break;
                }
            }
            finally
            {
                await Task.FromResult(context.Result);
            }
        }
    }
}
