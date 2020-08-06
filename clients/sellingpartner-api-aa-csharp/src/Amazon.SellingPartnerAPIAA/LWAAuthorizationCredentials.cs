using System;
using System.Collections.Generic;

namespace Amazon.SellingPartnerAPIAA
{
    public class LWAAuthorizationCredentials
    {
        public LWAAuthorizationCredentials()
        {
            this.Scopes = new List<string>();
        }

        /**
         * LWA Client Id
         */
        public string ClientId { get; set; }

        /**
         * LWA Client Secret
         */
        public string ClientSecret { get; set; }

        /**
         * LWA Refresh Token
         */
        public string RefreshToken { get; set; }

        /**
         * LWA Authorization Server Endpoint
         */
        public Uri Endpoint { get; set; }

        /**
         * LWA Authorization Scopes
         */
        public List<string> Scopes { get; set; }
    }
}

