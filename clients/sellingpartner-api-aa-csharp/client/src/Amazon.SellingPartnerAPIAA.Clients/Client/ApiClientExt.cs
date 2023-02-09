using RateLimiter;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.SellingPartnerAPIAA.Clients.Client
{
    /// <summary>
    /// API client is mainly responsible for making the HTTP call to the API backend.
    /// </summary>
    public partial class ApiClient
    {
        private readonly string RateLimitLimitHeaderName = "x-amzn-RateLimit-Limit";

        partial void InterceptResponse(IRestRequest request, IRestResponse response)
        {
            try
            {
                var limitHeader = response.Headers.Where(a => a.Name == RateLimitLimitHeaderName).FirstOrDefault();
                if (limitHeader != null)
                {
                    var RateLimitValue = limitHeader.Value.ToString();
                    decimal.TryParse(RateLimitValue, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal rate);
                    rateLimiter = TimeLimiter.GetFromMaxCountByInterval((int)(1 / rate * 1000), TimeSpan.FromSeconds(1));
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
