using System;
using Xunit;
using Moq;
using RestSharp;
using Amazon.SellingPartnerAPIAA;

namespace Amazon.SellingPartnerAPIAATests
{
    public class LWAAuthorizationSignerTest
    {
        private static readonly LWAAuthorizationCredentials LWAAuthorizationCredentials = new LWAAuthorizationCredentials()
        {
            ClientId = "cid",
            ClientSecret = "csecret",
            Endpoint = new Uri("https://www.amazon.com")
        };

        private LWAAuthorizationSigner lwaAuthorizationSignerUnderTest;

        public LWAAuthorizationSignerTest()
        {
            lwaAuthorizationSignerUnderTest = new LWAAuthorizationSigner(LWAAuthorizationCredentials);
        }

        [Fact]
        public void ConstructorInitializesLWAClientWithCredentials()
        {
            Assert.Equal(LWAAuthorizationCredentials, lwaAuthorizationSignerUnderTest.LWAClient.LWAAuthorizationCredentials);
        }

        [Fact]
        public void RequestIsSignedFromLWAClientProvidedToken()
        {
            string expectedAccessToken = "foo";

            var mockLWAClient = new Mock<LWAClient>(LWAAuthorizationCredentials);
            mockLWAClient.Setup(lwaClient => lwaClient.GetAccessToken()).Returns(expectedAccessToken);
            lwaAuthorizationSignerUnderTest.LWAClient = mockLWAClient.Object;

            IRestRequest restRequest = new RestRequest();
            restRequest = lwaAuthorizationSignerUnderTest.Sign(restRequest);

            Parameter actualAccessTokenHeader = restRequest.Parameters.Find(parameter =>
                ParameterType.HttpHeader.Equals(parameter.Type) && parameter.Name == LWAAuthorizationSigner.AccessTokenHeaderName);

            Assert.Equal(expectedAccessToken, actualAccessTokenHeader.Value);
        }
    }
}
