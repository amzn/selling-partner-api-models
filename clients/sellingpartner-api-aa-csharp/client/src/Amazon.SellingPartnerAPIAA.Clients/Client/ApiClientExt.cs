using Amazon.SellingPartnerAPIAA.Clients.Utils;
using RateLimiter;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Amazon.SellingPartnerAPIAA.Clients.Utils.ApiUrl;

namespace Amazon.SellingPartnerAPIAA.Clients.Client
{
    /// <summary>
    /// API client is mainly responsible for making the HTTP call to the API backend.
    /// </summary>
    public partial class ApiClient
    {
        //private readonly string RateLimitLimitHeaderName = "x-amzn-RateLimit-Limit";

        partial void InterceptResponse(IRestRequest request, IRestResponse response)
        {
            //try
            //{
            //    var rateLimit = RateLimitDefs.GetRateLimit(
            //        response.ResponseUri.AbsolutePath, 
            //        request.Parameters.Where(p => p.Type == ParameterType.UrlSegment).ToList());
            //    var limitHeader = response.Headers.FirstOrDefault(a => a.Name == RateLimitLimitHeaderName);
            //    if (limitHeader != null)
            //    {
            //        var RateLimitValue = limitHeader.Value.ToString();
            //        double.TryParse(RateLimitValue, NumberStyles.Any, CultureInfo.InvariantCulture, out double rate);
            //        rateLimiter. = TimeLimiter.GetFromMaxCountByInterval(
            //            rateLimit?.BurstValue ?? 1, 
            //            TimeSpan.FromSeconds(1 / rate));
            //    }
            //    else if (rateLimit != null)
            //    {
            //        rateLimiter = TimeLimiter.GetFromMaxCountByInterval(
            //            rateLimit.BurstValue, 
            //            TimeSpan.FromSeconds(1 / rateLimit.RateLimitValue));
            //    }
            //}
            //catch (Exception)
            //{

            //}
        }
    }

}
