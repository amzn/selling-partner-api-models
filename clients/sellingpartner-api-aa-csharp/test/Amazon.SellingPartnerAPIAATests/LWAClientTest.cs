﻿using System;
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

            var requestBody = request.Parameters
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
        public void UnsuccessfulPostThrowsException()
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

            SystemException systemException = Assert.Throws<SystemException>(() => lwaClientUnderTest.GetAccessToken());
            Assert.IsType<IOException>(systemException.GetBaseException());
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
