using Microsoft.AspNetCore.Components;
using SiGeP.API.Common.Model;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using System.Text;

namespace SiGeP.API.Common
{
    public static class WebApiClientExt2
    {
        public static async Task<bool> ValidateAccessToken(this WebApiClient webApiClient, IHttpContextAccessor _contextAccessor, NavigationManager _navigationManager,
            string prefix = "")
        {

            var loginUrl = AppConfiguration.GetConfiguration("LoginUrl");
            var user = _contextAccessor.HttpContext.User;
            if (!user.Identity.IsAuthenticated)
            {
                _navigationManager.NavigateTo(loginUrl, true);
                return false;
            }
            var warningMinutes = Convert.ToDouble(AppConfiguration.GetConfiguration("WarningMinutes"));

            var refreshTokenEndpoint = AppConfiguration.GetConfiguration("RefreshTokenEndpoint");

            var authTokenClaimName = string.IsNullOrWhiteSpace(prefix) ? "authToken" : $"{prefix}-authToken";
            var refreshTokenClaimName = string.IsNullOrWhiteSpace(prefix) ? "refreshToken" : $"{prefix}-refreshToken";
            var expClaimName = string.IsNullOrWhiteSpace(prefix) ? "exp" : $"{prefix}-exp";

            var accessTokenExpirationClaim = user.Claims.FirstOrDefault(x => x.Type == expClaimName);
            var authTokenClaim = user.Claims.FirstOrDefault(x => x.Type == authTokenClaimName);
            var refreshTokenClaim = user.Claims.FirstOrDefault(x => x.Type == refreshTokenClaimName);
            DateTimeOffset offset = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(accessTokenExpirationClaim.Value));
            DateTime expirationDate = offset.LocalDateTime;



            if (DateTime.Now < expirationDate.AddMinutes(warningMinutes))
            {
                webApiClient.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authTokenClaim.Value);
                return true;
            }
            else
            {
                var dtoAsJson = JsonSerializer.Serialize(new { AccessToken = authTokenClaim.Value, RefreshToken = refreshTokenClaim.Value });

                var response = await webApiClient.httpClient.PostAsync(refreshTokenEndpoint, new StringContent(dtoAsJson, Encoding.UTF8, "application/json"));
                if (!response.IsSuccessStatusCode)
                {
                    _navigationManager.NavigateTo(loginUrl, true);
                    return false;
                }
                else
                {
                    var tokens = await response.Content.ReadFromJsonAsync<JwtAuthResult>();
                    var identity = user.Identity as ClaimsIdentity;

                    var claimsCopy = user.Claims.ToList();
                    foreach (var claim in claimsCopy)
                    {
                        if (claim.Type.Contains(prefix))
                            identity.RemoveClaim(claim);
                    }


                    webApiClient.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens.AccessToken);

                    identity.AddClaims(ServiceExtensions.ParseClaimsFromJwt(tokens.AccessToken, prefix));

                    identity.AddClaim(new Claim(refreshTokenClaimName, tokens.RefreshToken.TokenString));

                    return true;
                }


            }
        }
    }
}
