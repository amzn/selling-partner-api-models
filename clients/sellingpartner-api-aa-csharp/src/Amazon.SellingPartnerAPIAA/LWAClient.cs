using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Amazon.SellingPartnerAPIAA
{
    public class LWAClient
    {
        public const string AccessTokenKey = "access_token";
        public const string ErrorCodeKey = "error";
        public const string ErrorDescKey = "error_description";
        public const string JsonMediaType = "application/json; charset=utf-8";

        public IRestClient RestClient { get; set; }
        public LWAAccessTokenRequestMetaBuilder LWAAccessTokenRequestMetaBuilder { get; set; }
        public LWAAuthorizationCredentials LWAAuthorizationCredentials { get; private set; }


        public LWAClient(LWAAuthorizationCredentials lwaAuthorizationCredentials)
        {
            LWAAuthorizationCredentials = lwaAuthorizationCredentials;
            LWAAccessTokenRequestMetaBuilder = new LWAAccessTokenRequestMetaBuilder();
            RestClient = new RestClient(LWAAuthorizationCredentials.Endpoint.GetLeftPart(System.UriPartial.Authority));
        }

        /// <summary>
        /// Retrieves access token from LWA
        /// </summary>
        /// <param name="lwaAccessTokenRequestMeta">LWA AccessTokenRequest metadata</param>
        /// <returns>LWA Access Token</returns>
        public virtual string GetAccessToken()
        {
            LWAAccessTokenRequestMeta lwaAccessTokenRequestMeta = LWAAccessTokenRequestMetaBuilder.Build(LWAAuthorizationCredentials);
            var accessTokenRequest = new RestRequest(LWAAuthorizationCredentials.Endpoint.AbsolutePath, Method.POST);

            string jsonRequestBody = JsonConvert.SerializeObject(lwaAccessTokenRequestMeta);

            accessTokenRequest.AddParameter(JsonMediaType, jsonRequestBody, ParameterType.RequestBody);

            string accessToken;

            LWAExceptionErrorCode errorCode;

            try
            {
                var response = RestClient.Execute(accessTokenRequest);
                JObject responseJson = !String.IsNullOrEmpty(response.Content) ? JObject.Parse(response.Content) : null;

                if (!IsSuccessful(response))
                {
                    if (responseJson != null)
                    {
                        // If error code is present check error code if its one of the defined LWAExceptionErrorCode values
                        var parsedErrorCode = responseJson.ContainsKey(ErrorCodeKey) ? Enum.TryParse(responseJson.GetValue(ErrorCodeKey).ToString(), out errorCode) : false;

                        if (parsedErrorCode) // throw error code and description if matches defined values
                        {
                            throw new LWAException(responseJson.GetValue(ErrorCodeKey).ToString(), responseJson.GetValue(ErrorDescKey).ToString(), "Error getting LWA Access Token");
                        }
                    } //else throw general LWA exception
                    throw new LWAException(LWAExceptionErrorCode.other.ToString(), "Other LWA Exception", "Error getting LWA Access Token");
                }
                accessToken = responseJson.GetValue(AccessTokenKey).ToString();
            }
            catch (LWAException e)
            {
                throw new LWAException(e.getErrorCode(), e.getErrorMessage(), e.Message);
            }
            catch (Exception e)
            {
                throw new SystemException("Error getting LWA Access Token", e);
            }

            return accessToken;
        }

        private bool IsSuccessful(IRestResponse response)
        {
            int statusCode = (int)response.StatusCode;
            return statusCode >= 200 && statusCode <= 299 && response.ResponseStatus == ResponseStatus.Completed;
        }
    }
}
