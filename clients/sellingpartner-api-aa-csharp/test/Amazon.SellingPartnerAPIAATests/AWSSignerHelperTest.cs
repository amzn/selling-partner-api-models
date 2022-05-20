using System;
using Xunit;
using RestSharp;
using Amazon.SellingPartnerAPIAA;
using System.Text;
using Moq;

namespace Amazon.SellingPartnerAPIAATests
{
    public class AWSSignerHelperTest
    {
        private const string Slash = "/";
        private const string ISOSigningDateTime = "20200504T121212Z";
        private const string ISOSigningDate = "20200504";
        private const string TestAccessKeyId = "aKey";
        private const string TestSecretKey = "sKey";
        private const string TestRegion = "us-east-1";
        private const string TestResourcePath = "iam/user";
        private const string TestHost = "sellingpartnerapi.amazon.com";
        private const string JsonMediaType = "application/json; charset=utf-8";

        private static readonly DateTime SigningDate = DateTime.Parse("2020-05-04 12:12:12");

        private AWSSignerHelper awsSignerHelperUnderTest;

        public AWSSignerHelperTest()
        {
            var mockDateHelper = new Mock<IDateHelper>();
            mockDateHelper.Setup(dateHelper => dateHelper.GetUtcNow()).Returns(SigningDate);
            awsSignerHelperUnderTest = new AWSSignerHelper() { DateHelper = mockDateHelper.Object };
        }

        [Fact]
        public void TestExtractCanonicalURIParameters()
        {
            IRestRequest request = new RestRequest(TestResourcePath, Method.GET);
            string result = awsSignerHelperUnderTest.ExtractCanonicalURIParameters(request);
            Assert.Equal(Slash + TestResourcePath, result);
        }

        [Fact]
        public void TestExtractCanonicalURIParameters_UrlSegments()
        {
            IRestRequest request = new RestRequest("products/pricing/v0/items/{Asin}/offers/{SellerSKU}", Method.GET);
            request.AddUrlSegment("Asin", "AB12CD3E4Z");
            request.AddUrlSegment("SellerSKU", "1234567890");
            string result = awsSignerHelperUnderTest.ExtractCanonicalURIParameters(request);
            Assert.Equal("/products/pricing/v0/items/AB12CD3E4Z/offers/1234567890", result);
        }

        [Fact]
        public void TestExtractCanonicalURIParameters_IncorrectUrlSegment()
        {
            IRestRequest request = new RestRequest("products/pricing/v0/items/{Asin}/offers", Method.GET);
            request.AddUrlSegment("asin", "AB12CD3E4Z");
            string result = awsSignerHelperUnderTest.ExtractCanonicalURIParameters(request);
            Assert.Equal("/products/pricing/v0/items/%257BAsin%257D/offers", result);
        }

        [Fact]
        public void TestExtractCanonicalURIParameters_ResourcePathWithSpace()
        {
            IRestRequest request = new RestRequest("iam/ user", Method.GET);
            string result = awsSignerHelperUnderTest.ExtractCanonicalURIParameters(request);
            Assert.Equal("/iam/%2520user", result);
        }

        [Fact]
        public void TestExtractCanonicalURIParameters_EmptyResourcePath()
        {
            IRestRequest request = new RestRequest(string.Empty, Method.GET);
            string result = awsSignerHelperUnderTest.ExtractCanonicalURIParameters(request);
            Assert.Equal(Slash, result);
        }

        [Fact]
        public void TestExtractCanonicalURIParameters_NullResourcePath()
        {
            string result = awsSignerHelperUnderTest.ExtractCanonicalURIParameters(new RestRequest());
            Assert.Equal(Slash, result);
        }

        [Fact]
        public void TestExtractCanonicalURIParameters_SlashPath()
        {
            IRestRequest request = new RestRequest(Slash, Method.GET);
            string result = awsSignerHelperUnderTest.ExtractCanonicalURIParameters(request);
            Assert.Equal(Slash, result);
        }

        [Fact]
        public void TestExtractCanonicalQueryString()
        {
            IRestRequest request = new RestRequest();
            request.AddQueryParameter("Version", "2010-05-08");
            request.AddQueryParameter("Action", "ListUsers");
            request.AddQueryParameter("RequestId", "1");

            string result = awsSignerHelperUnderTest.ExtractCanonicalQueryString(request);
            //Query parameters in canonical order
            Assert.Equal("Action=ListUsers&RequestId=1&Version=2010-05-08", result);
        }

        [Fact]
        public void TestExtractCanonicalQueryString_EmptyQueryParameters()
        {
            string result = awsSignerHelperUnderTest.ExtractCanonicalQueryString(new RestRequest());
            Assert.Empty(result);
        }

        [Fact]
        public void TestExtractCanonicalQueryString_WithUrlEncoding()
        {
            IRestRequest request = new RestRequest();
            request.AddQueryParameter("Action^", "ListUsers$Roles");
            string result = awsSignerHelperUnderTest.ExtractCanonicalQueryString(request);
            Assert.Equal("Action%5E=ListUsers%24Roles", result);
        }

        [Fact]
        public void TestExtractCanonicalHeaders()
        {
            IRestRequest request = new RestRequest();
            request.AddHeader("X-Amz-Date", "20150830T123600Z");
            request.AddHeader("Host", "iam.amazonaws.com");
            request.AddHeader("Content-Type", JsonMediaType);

            string result = awsSignerHelperUnderTest.ExtractCanonicalHeaders(request);
            Assert.Equal("content-type:application/json; charset=utf-8\nhost:iam.amazonaws.com\n" +
                "x-amz-date:20150830T123600Z\n", result);
        }

        [Fact]
        public void TestExtractCanonicalHeaders_NoHeader()
        {
            string result = awsSignerHelperUnderTest.ExtractCanonicalHeaders(new RestRequest());
            Assert.Empty(result);
        }

        [Fact]
        public void TestExtractSignedHeaders()
        {
            IRestRequest request = new RestRequest();
            request.AddHeader("X-Amz-Date", "20150830T123600Z");
            request.AddHeader("Host", "iam.amazonaws.com");
            request.AddHeader("Content-Type", JsonMediaType);

            string result = awsSignerHelperUnderTest.ExtractSignedHeaders(request);
            Assert.Equal("content-type;host;x-amz-date", result);
        }

        [Fact]
        public void TestExtractSignedHeaders_NoHeader()
        {
            string result = awsSignerHelperUnderTest.ExtractSignedHeaders(new RestRequest());
            Assert.Empty(result);
        }

        [Fact]
        public void TestHashRequestBody()
        {
            IRestRequest request = new RestRequest(TestResourcePath, Method.POST);
            request.AddJsonBody("{\"test\":\"payload\"}");

            string result = awsSignerHelperUnderTest.HashRequestBody(request);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void TestHashRequestBody_NoBody()
        {
            string result = awsSignerHelperUnderTest.HashRequestBody(new RestRequest());
            Assert.NotEmpty(result);
        }

        [Fact]
        public void TestBuildStringToSign()
        {
            string expectedCanonicalHash = "foo";
            StringBuilder expectedStringBuilder = new StringBuilder();
            expectedStringBuilder.AppendLine("AWS4-HMAC-SHA256");
            expectedStringBuilder.AppendLine(ISOSigningDateTime);
            expectedStringBuilder.AppendFormat("{0}/{1}/execute-api/aws4_request\n", ISOSigningDate, TestRegion);
            expectedStringBuilder.Append(expectedCanonicalHash);

            string result = awsSignerHelperUnderTest.BuildStringToSign(SigningDate, expectedCanonicalHash, TestRegion);

            Assert.Equal(expectedStringBuilder.ToString(), result);
        }

        [Fact]
        public void TestInitializeHeadersReturnsUtcNow()
        {
            Assert.Equal(SigningDate, awsSignerHelperUnderTest.InitializeHeaders(new RestRequest(), TestHost));
        }

        [Fact]
        public void TestInitializeHeadersSetsUtcNowXAmzDateHeader()
        {
            IRestRequest request = new RestRequest();
            awsSignerHelperUnderTest.InitializeHeaders(request, TestHost);

            Parameter actualParameter = request.Parameters.Find(parameter =>
                ParameterType.HttpHeader.Equals(parameter.Type) && parameter.Name == AWSSignerHelper.XAmzDateHeaderName);

            Assert.Equal(ISOSigningDateTime, actualParameter.Value);
        }

        [Fact]
        public void TestInitializeHeadersOverwritesXAmzDateHeader()
        {
            IRestRequest request = new RestRequest();
            request.AddHeader(AWSSignerHelper.XAmzDateHeaderName, "foobar");

            awsSignerHelperUnderTest.InitializeHeaders(request, TestHost);

            Parameter actualParameter = request.Parameters.Find(parameter =>
                ParameterType.HttpHeader.Equals(parameter.Type) && parameter.Name == AWSSignerHelper.XAmzDateHeaderName);

            Assert.Equal(ISOSigningDateTime, actualParameter.Value);
        }

        [Fact]
        public void TestAddSignatureToRequest()
        {
            IRestRequest restRequest = new RestRequest();
            string expectedAccessKeyId = TestAccessKeyId;
            string expectedRegion = TestRegion;
            string expectedSignature = "testCalculatedSignature";
            string expectedSignedHeaders = "header1;header2";

            string expectedAuthorizationHeaderValue = string.Format("AWS4-HMAC-SHA256 " +
                "Credential={0}/{1}/{2}/execute-api/aws4_request, SignedHeaders={3}, Signature={4}",
                expectedAccessKeyId,
                ISOSigningDate,
                expectedRegion,
                expectedSignedHeaders,
                expectedSignature);

            awsSignerHelperUnderTest.AddSignature(restRequest,
                                                  expectedAccessKeyId,
                                                  expectedSignedHeaders,
                                                  expectedSignature,
                                                  expectedRegion,
                                                  SigningDate);

            Parameter actualParameter = restRequest.Parameters.Find(parameter =>
                ParameterType.HttpHeader.Equals(parameter.Type) && parameter.Name == AWSSignerHelper.AuthorizationHeaderName);

            Assert.Equal(expectedAuthorizationHeaderValue, actualParameter.Value);
        }

        [Fact]
        public void TestCalculateSignature()
        {
            string signature = awsSignerHelperUnderTest.CalculateSignature("testString",
                                                                           SigningDate,
                                                                           TestSecretKey,
                                                                           TestRegion);
            Assert.Equal("7e2c7c2e330123ef7468b41d8ddaf3841e6ef56959b9116b44ded5466cf96405", signature);
        }

        [Fact]
        public void TestInitializeHeadersSetsHostHeader()
        {
            IRestRequest restRequest = new RestRequest();

            awsSignerHelperUnderTest.InitializeHeaders(restRequest, TestHost);

            Parameter actualParamter = restRequest.Parameters.Find(parameter =>
                ParameterType.HttpHeader.Equals(parameter.Type) && parameter.Name == AWSSignerHelper.HostHeaderName);

            Assert.Equal(TestHost, actualParamter.Value);
        }

        [Fact]
        public void TestInitializeHeadersOverwritesHostHeader()
        {
            IRestRequest restRequest = new RestRequest();

            restRequest.AddHeader(AWSSignerHelper.HostHeaderName, "foobar");

            awsSignerHelperUnderTest.InitializeHeaders(restRequest, TestHost);

            Parameter actualParamter = restRequest.Parameters.Find(parameter =>
                ParameterType.HttpHeader.Equals(parameter.Type) && parameter.Name == AWSSignerHelper.HostHeaderName);

            Assert.Equal(TestHost, actualParamter.Value);
        }
    }
}
