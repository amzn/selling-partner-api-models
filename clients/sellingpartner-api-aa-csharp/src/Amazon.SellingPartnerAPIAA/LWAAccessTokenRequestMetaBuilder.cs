using System.Linq;

namespace Amazon.SellingPartnerAPIAA
{
    public class LWAAccessTokenRequestMetaBuilder
    {
        public const string SellerAPIGrantType = "refresh_token";
        public const string SellerlessAPIGrantType = "client_credentials";

        private const string Delimiter = " ";

        /// <summary>
        /// Builds an instance of LWAAccessTokenRequestMeta modeling appropriate LWA token
        /// request params based on configured LWAAuthorizationCredentials
        /// </summary>
        /// <param name="lwaAuthorizationCredentials">LWA Authorization Credentials</param>
        /// <returns></returns>
        public virtual LWAAccessTokenRequestMeta Build(LWAAuthorizationCredentials lwaAuthorizationCredentials)
        {
            LWAAccessTokenRequestMeta lwaAccessTokenRequestMeta = new LWAAccessTokenRequestMeta()
            {
                ClientId = lwaAuthorizationCredentials.ClientId,
                ClientSecret = lwaAuthorizationCredentials.ClientSecret,
                RefreshToken = lwaAuthorizationCredentials.RefreshToken
            };

            if (lwaAuthorizationCredentials.Scopes == null || lwaAuthorizationCredentials.Scopes.Count == 0)
            {
                lwaAccessTokenRequestMeta.GrantType = SellerAPIGrantType;
            }
            else
            {
                lwaAccessTokenRequestMeta.Scope = string.Join(Delimiter, lwaAuthorizationCredentials.Scopes);
                lwaAccessTokenRequestMeta.GrantType = SellerlessAPIGrantType;
            }

            return lwaAccessTokenRequestMeta;
        }
    }
}
