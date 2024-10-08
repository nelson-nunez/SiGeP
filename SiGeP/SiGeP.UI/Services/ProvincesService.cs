﻿using SiGeP.Model.DTO;
using Microsoft.AspNetCore.Components;
using SiGeP.API.Common;


namespace SiGeP.UI.Services
{
    public class ProvinceService
    {
        private readonly WebApiClient baseApiClient;
        private readonly IHttpContextAccessor contextAccessor;
        private readonly NavigationManager navigator;

        public ProvinceService(WebApiClient baseApiClient, IHttpContextAccessor contextAccessor, NavigationManager navigator)
        {
            this.baseApiClient = baseApiClient;
            this.contextAccessor = contextAccessor;
            this.navigator = navigator;
        }

        public async Task<List<ProvinceDTO>> GetProvincesAsync()
        {
            await baseApiClient.ValidateAccessToken(contextAccessor, navigator);
            var result = await baseApiClient.GetAsync<List<ProvinceDTO>>("Provinces");
            return result;
        }
    }
}
