﻿using System.Web;


namespace SiGeP.API.Common.Model
{
    public class UserContext
    {
        public string UserId { get; set; }

        public string Name { get; set; }

        public string EmailAddress { get; set; }

        public int AuthenticationTypeId { get; set; }

        public string AppClientId { get; set; }

        public string ClientIPAddress { get; set; }

        public int AccessTokenExpiration { get; set; }

        public long Expiration { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }
    }
}