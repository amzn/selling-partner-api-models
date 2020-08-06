using System.Collections.Generic;
using Newtonsoft.Json;

namespace Amazon.SellingPartnerAPIAA
{
    public class LWAAccessTokenRequestMeta
    {
        [JsonProperty(PropertyName = "grant_type")]
        public string GrantType { get; set; }

        [JsonProperty(PropertyName = "refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty(PropertyName = "client_id")]
        public string ClientId { get; set; }

        [JsonProperty(PropertyName = "client_secret")]
        public string ClientSecret { get; set; }

        [JsonProperty(PropertyName = "scope")]
        public string Scope { get; set; }

        public override bool Equals(object obj)
        {
            LWAAccessTokenRequestMeta other = obj as LWAAccessTokenRequestMeta;

            return other != null &&
                this.GrantType == other.GrantType &&
                this.RefreshToken == other.RefreshToken &&
                this.ClientId == other.ClientId &&
                this.ClientSecret == other.ClientSecret &&
                this.Scope == other.Scope;
        }
    }
}
