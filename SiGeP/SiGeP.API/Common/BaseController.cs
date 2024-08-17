﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiGeP.API.Common.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SiGeP.API.Common
{
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    [ApiController]
    [Authorize]
    public class BaseController : ControllerBase
    {
        protected IJwtAuthManager jwtAuthManager;


        public BaseController(IJwtAuthManager jwtAuthManager)
        {
            this.jwtAuthManager = jwtAuthManager;
        }

        protected async Task<JwtSecurityToken> GetJwtTokenAsync()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var (principal, jwtToken) = jwtAuthManager.DecodeJwtToken(accessToken);

            return jwtToken;
        }

        protected async Task<UserContext> GetUserContext(IHttpContextAccessor contextAccessor)
        {
            var claims = contextAccessor.HttpContext.User.Claims;

            var context = new UserContext
            {
                UserId = GetClaimValue(claims, "UserId"),
                Name = GetClaimValue(claims, ClaimTypes.Name),
                EmailAddress = GetClaimValue(claims, ClaimTypes.Email),
                AuthenticationTypeId = Convert.ToInt32(GetClaimValue(claims, "AuthenticationTypeId")),
                AppClientId = GetClaimValue(claims, "AppClientId"),
                ClientIPAddress = GetClaimValue(claims, "ClientIPAddress"),
                AccessTokenExpiration = Convert.ToInt32(GetClaimValue(claims, "AccessTokenExpiration")),
                Expiration = Convert.ToInt64(GetClaimValue(claims, "exp")),
                Audience = GetClaimValue(claims, "aud"),
                Issuer = GetClaimValue(claims, "iss"),
            };

            return context;
        }

        protected string GetClaimValue(IEnumerable<Claim> claims, string claimType)
        {
            var claim = claims.FirstOrDefault(x => x.Type == claimType);

            if (claim != null) return claim.Value;
            else return "";
        }
    }
}

