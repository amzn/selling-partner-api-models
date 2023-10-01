using System;
using System.IO;
using System.Linq;
using System.Net;
using Moq;
using Newtonsoft.Json.Linq;
using RestSharp;
using Amazon.SellingPartnerAPIAA;
using Xunit;

namespace Amazon.SellingPartnerAPIAATests
{
    public class LWAClientTest
    {
        private const string TestClientSecret = "cSecret";
        private const string TestClientId = "cId";
        private const string TestRefreshGrantType = "rToken";
        private const string LWAExceptionMessage = "Error getting LWA Access Token";
        private const string LWAClientAuthMessage = "Client authentication failed";
        private const string LWAAccessDeniedMessage = "The customer or authorization server denied the request";
        private Mock<RestClient> mockRestClient;
        private Mock<LWAAccessTokenRequestMetaBuilder> mockLWAAccessTokenRequestMetaBuilder;

        private static readonly Uri TestEndpoint = new Uri("https://www.amazon.com/lwa");
        private static readonly LWAAuthorizationCredentials LWAAuthorizationCredentials = new LWAAuthorizationCredentials
        {
            ClientId = TestClientId,
            ClientSecret = TestClientSecret,
            RefreshToken = TestRefreshGrantType,
            Endpoint = TestEndpoint
        };
        private static readonly IRestResponse Response = new RestResponse
        {
            StatusCode = HttpStatusCode.OK,
            ResponseStatus = ResponseStatus.Completed,
            Content = @"{access_token:'Azta|foo'}"
        };

        public LWAClientTest()
        {
            mockRestClient = new Mock<RestClient>();
            mockLWAAccessTokenRequestMetaBuilder = new Mock<LWAAccessTokenRequestMetaBuilder>();
        }

        [Fact]
        public void InitializeLWAAuthorizationCredentials()
        {
            LWAClient lwaClientUnderTest = new LWAClient(LWAAuthorizationCredentials);
            Assert.Equal(LWAAuthorizationCredentials, lwaClientUnderTest.LWAAuthorizationCredentials);
        }

        [Fact]
        public void MakeRequestFromMeta()
        {
            IRestRequest request = new RestRequest();
            LWAAccessTokenRequestMeta expectedLWAAccessTokenRequestMeta = new LWAAccessTokenRequestMeta()
            {
                ClientSecret = "expectedSecret",
                ClientId = "expectedClientId",
                RefreshToken = "expectedRefreshToken",
                GrantType = "expectedGrantType"
            };

            mockRestClient.Setup(client => client.Execute(It.IsAny<IRestRequest>()))
                .Callback((IRestRequest req) => { request = req; })
                .Returns(Response);

            mockLWAAccessTokenRequestMetaBuilder.Setup(builder => builder.Build(LWAAuthorizationCredentials))
                .Returns(expectedLWAAccessTokenRequestMeta);

            LWAClient lwaClientUnderTest = new LWAClient(LWAAuthorizationCredentials);
            lwaClientUnderTest.RestClient = mockRestClient.Object;
            lwaClientUnderTest.LWAAccessTokenRequestMetaBuilder = mockLWAAccessTokenRequestMetaBuilder.Object;
            lwaClientUnderTest.GetAccessToken();

            Parameter requestBody = request.Parameters
                .FirstOrDefault(parameter => parameter.Type.Equals(ParameterType.RequestBody));

            JObject jsonRequestBody = JObject.Parse(requestBody.Value.ToString());

            Assert.Equal(Method.POST, request.Method);
            Assert.Equal(TestEndpoint.AbsolutePath, request.Resource);
            Assert.Equal(expectedLWAAccessTokenRequestMeta.RefreshToken, jsonRequestBody.GetValue("refresh_token"));
            Assert.Equal(expectedLWAAccessTokenRequestMeta.GrantType, jsonRequestBody.GetValue("grant_type"));
            Assert.Equal(expectedLWAAccessTokenRequestMeta.ClientId, jsonRequestBody.GetValue("client_id"));
            Assert.Equal(expectedLWAAccessTokenRequestMeta.ClientSecret, jsonRequestBody.GetValue("client_secret"));
        }

        [Fact]
        public void ReturnAccessTokenFromResponse()
        {
            IRestRequest request = new RestRequest();

            mockRestClient.Setup(client => client.Execute(It.IsAny<IRestRequest>()))
                .Callback((IRestRequest req) => { request = req; })
                .Returns(Response);

            LWAClient lwaClientUnderTest = new LWAClient(LWAAuthorizationCredentials);
            lwaClientUnderTest.RestClient = mockRestClient.Object;

            string accessToken = lwaClientUnderTest.GetAccessToken();

            Assert.Equal("Azta|foo", accessToken);
        }

        [Fact]
        public void UnsuccessfulPostMissingContentThrowsOtherException()
        {
            IRestResponse response = new RestResponse
            {
                StatusCode = HttpStatusCode.BadRequest,
                ResponseStatus = ResponseStatus.Completed,
                Content = string.Empty
            };

            IRestRequest request = new RestRequest();

            mockRestClient.Setup(client => client.Execute(It.IsAny<IRestRequest>()))
               .Callback((IRestRequest req) => { request = req; })
               .Returns(response);

            LWAClient lwaClientUnderTest = new LWAClient(LWAAuthorizationCredentials);
            lwaClientUnderTest.RestClient = mockRestClient.Object;

            LWAException lwaException = Assert.Throws<LWAException>(() => lwaClientUnderTest.GetAccessToken());
            Assert.Equal(LWAExceptionErrorCode.other.ToString(), lwaException.getErrorCode());
            Assert.Equal(LWAExceptionMessage, lwaException.Message);
        }

        [Fact]
        public void UnsuccessfulPostThrowsInvalidClientException()
        {
            IRestResponse response = new RestResponse
            {
                StatusCode = HttpStatusCode.BadRequest,
                ResponseStatus = ResponseStatus.Completed,
                Content = @"{error:'invalid_client', error_description:'Client authentication failed'}"
            };

            IRestRequest request = new RestRequest();

            mockRestClient.Setup(client => client.Execute(It.IsAny<IRestRequest>()))
               .Callback((IRestRequest req) => { request = req; })
               .Returns(response);

            LWAClient lwaClientUnderTest = new LWAClient(LWAAuthorizationCredentials);
            lwaClientUnderTest.RestClient = mockRestClient.Object;

            LWAException lwaException = Assert.Throws<LWAException>(() => lwaClientUnderTest.GetAccessToken());
            Assert.Equal(LWAExceptionErrorCode.invalid_client.ToString(), lwaException.getErrorCode());
            Assert.Equal(LWAClientAuthMessage, lwaException.getErrorMessage());
        }

        [Fact]
        public void UnsuccessfulPostThrowsAccessDeniedException()
        {
            IRestResponse response = new RestResponse
            {
                StatusCode = HttpStatusCode.BadRequest,
                ResponseStatus = ResponseStatus.Completed,
                Content = @"{error:'access_denied', error_description:'The customer or authorization server denied the request'}"
            };

            IRestRequest request = new RestRequest();

            mockRestClient.Setup(client => client.Execute(It.IsAny<IRestRequest>()))
               .Callback((IRestRequest req) => { request = req; })
               .Returns(response);

            LWAClient lwaClientUnderTest = new LWAClient(LWAAuthorizationCredentials);
            lwaClientUnderTest.RestClient = mockRestClient.Object;

            LWAException lwaException = Assert.Throws<LWAException>(() => lwaClientUnderTest.GetAccessToken());
            Assert.Equal(LWAExceptionErrorCode.access_denied.ToString(), lwaException.getErrorCode());
            Assert.Equal(LWAAccessDeniedMessage, lwaException.getErrorMessage());
        }

        [Fact]
        public void UnsuccessfulPostThrowsOtherException()
        {
            IRestResponse response = new RestResponse
            {
                StatusCode = HttpStatusCode.BadRequest,
                ResponseStatus = ResponseStatus.Completed,
                Content = @"{error:'invalid_token'}"
            };

            IRestRequest request = new RestRequest();

            mockRestClient.Setup(client => client.Execute(It.IsAny<IRestRequest>()))
               .Callback((IRestRequest req) => { request = req; })
               .Returns(response);

            LWAClient lwaClientUnderTest = new LWAClient(LWAAuthorizationCredentials);
            lwaClientUnderTest.RestClient = mockRestClient.Object;

            LWAException lwaException = Assert.Throws<LWAException>(() => lwaClientUnderTest.GetAccessToken());
            Assert.Equal(LWAExceptionErrorCode.other.ToString(), lwaException.getErrorCode());
            Assert.Equal(LWAExceptionMessage, lwaException.Message);
        }

        [Fact]
        public void MissingAccessTokenInResponseThrowsException()
        {
            IRestResponse response = new RestResponse
            {
                StatusCode = HttpStatusCode.OK,
                ResponseStatus = ResponseStatus.Completed,
                Content = string.Empty
            };

            IRestRequest request = new RestRequest();

            mockRestClient.Setup(client => client.Execute(It.IsAny<RestRequest>()))
               .Callback((IRestRequest req) => { request = (RestRequest)req; })
               .Returns(response);

            LWAClient lwaClientUnderTest = new LWAClient(LWAAuthorizationCredentials);
            lwaClientUnderTest.RestClient = mockRestClient.Object;

            Assert.Throws<SystemException>(() => lwaClientUnderTest.GetAccessToken());
        }
    }
}
