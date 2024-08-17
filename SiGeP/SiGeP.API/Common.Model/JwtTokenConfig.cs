﻿using System.Web;


namespace SiGeP.API.Common.Model
{
    public class JwtTokenConfig
    {
        public string Secret { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public int AccessTokenExpiration { get; set; }

        public int RefreshTokenExpiration { get; set; }

        public string AppClientIds { get; set; }
    }
}