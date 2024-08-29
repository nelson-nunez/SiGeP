using SiGeP.API.Common.Model;
using SiGeP.API.Common;

namespace SiGeP.UI.Services
{
    public class AuthenticationService
    {
        private readonly WebApiClient baseApiClient;
        private IHttpContextAccessor contextAccessor;

        public AuthenticationService(WebApiClient baseApiClient, IHttpContextAccessor contextAccessor)
        {
            this.baseApiClient = baseApiClient;
            this.contextAccessor = contextAccessor;
        }

        public async Task<JwtAuthResult> Authenticate(string username, string password)
        {
            var authenticationResult = await baseApiClient.PostAsync<JwtAuthResult>($"Authentication/Authenticate", new SigninRequest
            {
                UserName = username,
                Password = password,
                AppClientId = "1",
                ClientIPAddress= "1"
            });
            return authenticationResult;
        }
    }
}

