using System;
using Xunit;
using Amazon.SellingPartnerAPIAA;
using System.Text;
using Moq;
using System.Net.Http;
using System.Web;
using System.Linq;

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
        private const string TestUri = "https://" + TestHost + "/" + TestResourcePath;
        private const string JsonMediaType = "application/json; charset=utf-8";

        private static readonly DateTime SigningDate = DateTime.Parse("2020-05-04 12:12:12");

        private AWSSignerHelper awsSignerHelperUnderTest;

        public AWSSignerHelperTest()
        {
            var mockDateHelper = new Mock<IDateHelper>();
            mockDateHelper.Setup(dateHelper => dateHelper.GetUtcNow()).Returns(SigningDate);
            awsSignerHelperUnderTest = new AWSSignerHelper() { DateHelper = mockDateHelper.Object };
        }

        //[Fact]
        //public void TestExtractCanonicalURIParameters()
        //{
        //    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, TestUri);
        //    string result = awsSignerHelperUnderTest.ExtractCanonicalURIParameters(Utils.GetResourceFromUri(request.RequestUri));
        //    Assert.Equal(Slash + TestUri, result);
        //}

        //[Fact]
        //public void TestExtractCanonicalURIParameters_UrlSegments()
        //{
        //    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "products/pricing/v0/items/AB12CD3E4Z/offers/1234567890");
        //    string result = awsSignerHelperUnderTest.ExtractCanonicalURIParameters(Utils.GetResourceFromUri(request.RequestUri));
        //    Assert.Equal("/products/pricing/v0/items/AB12CD3E4Z/offers/1234567890", result);
        //}

        //[Fact]
        //public void TestExtractCanonicalURIParameters_IncorrectUrlSegment()
        //{
        //    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "products/pricing/v0/items/{Asin}/offers");
        //    string result = awsSignerHelperUnderTest.ExtractCanonicalURIParameters(Utils.GetResourceFromUri(request.RequestUri));
        //    Assert.Equal("/products/pricing/v0/items/%257BAsin%257D/offers", result);
        //}

        //[Fact]
        //public void TestExtractCanonicalURIParameters_ResourcePathWithSpace()
        //{
        //    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "iam/ user");
        //    string result = awsSignerHelperUnderTest.ExtractCanonicalURIParameters(Utils.GetResourceFromUri(request.RequestUri));
        //    Assert.Equal("/iam/%2520user", result);
        //}

        //[Fact]
        //public void TestExtractCanonicalURIParameters_EmptyResourcePath()
        //{
        //    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "");
        //    string result = awsSignerHelperUnderTest.ExtractCanonicalURIParameters(Utils.GetResourceFromUri(request.RequestUri));
        //    Assert.Equal(Slash, result);
        //}

        //[Fact]
        //public void TestExtractCanonicalURIParameters_NullResourcePath()
        //{
        //    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "");
        //    string result = awsSignerHelperUnderTest.ExtractCanonicalURIParameters(Utils.GetResourceFromUri(request.RequestUri));
        //    Assert.Equal(Slash, result);
        //}

        //[Fact]
        //public void TestExtractCanonicalURIParameters_SlashPath()
        //{
        //    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Slash);
        //    string result = awsSignerHelperUnderTest.ExtractCanonicalURIParameters(Utils.GetResourceFromUri(request.RequestUri));
        //    Assert.Equal(Slash, result);
        //}

        [Fact]
        public void TestExtractCanonicalQueryString()
        {
            Uri requestUri = new Uri(TestUri);
            requestUri = Utils.AddUriParameter(requestUri, "Version", "2010-05-08");
            requestUri = Utils.AddUriParameter(requestUri, "Action", "ListUsers");
            requestUri = Utils.AddUriParameter(requestUri, "RequestId", "1");
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            string result = awsSignerHelperUnderTest.ExtractCanonicalQueryString(request);
            //Query parameters in canonical order
            Assert.Equal("Action=ListUsers&RequestId=1&Version=2010-05-08", result);
        }

        [Fact]
        public void TestExtractCanonicalQueryString_EmptyQueryParameters()
        {
            string result = awsSignerHelperUnderTest.ExtractCanonicalQueryString(new HttpRequestMessage(HttpMethod.Get, TestUri));
            Assert.Empty(result);
        }

        [Fact]
        public void TestExtractCanonicalQueryString_WithUrlEncoding()
        {
            Uri requestUri = new Uri(TestUri);
            requestUri = Utils.AddUriParameter(requestUri, "Action^", "ListUsers$Roles");
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            string result = awsSignerHelperUnderTest.ExtractCanonicalQueryString(request);
            Assert.Equal("Action%5E=ListUsers%24Roles", result);
        }

        [Fact]
        public void TestExtractCanonicalHeaders()
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.Headers.Add("X-Amz-Date", "20150830T123600Z");
            request.Headers.Add("Host", "iam.amazonaws.com");

            string result = awsSignerHelperUnderTest.ExtractCanonicalHeaders(request);
            Assert.Equal("host:iam.amazonaws.com\n" +
                "x-amz-date:20150830T123600Z\n", result);
        }

        [Fact]
        public void TestExtractCanonicalHeaders_NoHeader()
        {
            string result = awsSignerHelperUnderTest.ExtractCanonicalHeaders(new HttpRequestMessage());
            Assert.Empty(result);
        }

        [Fact]
        public void TestExtractSignedHeaders()
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.Headers.Add("X-Amz-Date", "20150830T123600Z");
            request.Headers.Add("Host", "iam.amazonaws.com");

            string result = awsSignerHelperUnderTest.ExtractSignedHeaders(request);
            Assert.Equal("host;x-amz-date", result);
        }

        [Fact]
        public void TestExtractSignedHeaders_NoHeader()
        {
            string result = awsSignerHelperUnderTest.ExtractSignedHeaders(new HttpRequestMessage());
            Assert.Empty(result);
        }

        [Fact]
        public void TestHashRequestBody()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, TestResourcePath);
            request.Content = new StringContent("{\"test\":\"payload\"}");

            string result = awsSignerHelperUnderTest.HashRequestBody(request);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void TestHashRequestBody_NoBody()
        {
            string result = awsSignerHelperUnderTest.HashRequestBody(new HttpRequestMessage());
            Assert.NotEmpty(result);
        }

        [Fact]
        public void TestBuildStringToSign()
        {
            string expectedCanonicalHash = "foo";
            StringBuilder expectedStringBuilder = new StringBuilder();
            expectedStringBuilder.Append("AWS4-HMAC-SHA256\n");
            expectedStringBuilder.Append(ISOSigningDateTime);
            expectedStringBuilder.Append("\n");
            expectedStringBuilder.AppendFormat("{0}/{1}/execute-api/aws4_request\n", ISOSigningDate, TestRegion);
            expectedStringBuilder.Append(expectedCanonicalHash);

            string result = awsSignerHelperUnderTest.BuildStringToSign(SigningDate, expectedCanonicalHash, TestRegion);

            Assert.Equal(expectedStringBuilder.ToString(), result);
        }

        [Fact]
        public void TestInitializeHeadersReturnsUtcNow()
        {
            Assert.Equal(SigningDate, awsSignerHelperUnderTest.InitializeHeaders(new HttpRequestMessage(HttpMethod.Get, TestUri)));
        }

        [Fact]
        public void TestInitializeHeadersSetsUtcNowXAmzDateHeader()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, TestUri);
            awsSignerHelperUnderTest.InitializeHeaders(request);

            string XAmzDateHeaderValue = request.Headers.GetValues(AWSSignerHelper.XAmzDateHeaderName).FirstOrDefault();

            Assert.Equal(ISOSigningDateTime, XAmzDateHeaderValue);
        }

        [Fact]
        public void TestInitializeHeadersOverwritesXAmzDateHeader()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, TestUri);
            request.Headers.Add(AWSSignerHelper.XAmzDateHeaderName, "foobar");

            awsSignerHelperUnderTest.InitializeHeaders(request);

            string XAmzDateHeaderValue = request.Headers.GetValues(AWSSignerHelper.XAmzDateHeaderName).FirstOrDefault();

            Assert.Equal(ISOSigningDateTime, XAmzDateHeaderValue);
        }

        [Fact]
        public void TestAddSignatureToRequest()
        {
            HttpRequestMessage restRequest = new HttpRequestMessage();
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

            string SignatureHeaderValue = restRequest.Headers.GetValues(AWSSignerHelper.AuthorizationHeaderName).FirstOrDefault();

            Assert.Equal(expectedAuthorizationHeaderValue, SignatureHeaderValue);
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
            HttpRequestMessage restRequest = new HttpRequestMessage(HttpMethod.Get, TestUri);

            awsSignerHelperUnderTest.InitializeHeaders(restRequest);

            string HostHeaderValue = restRequest.Headers.GetValues(AWSSignerHelper.HostHeaderName).FirstOrDefault();

            Assert.Equal(TestHost, HostHeaderValue);
        }

        [Fact]
        public void TestInitializeHeadersOverwritesHostHeader()
        {
            HttpRequestMessage restRequest = new HttpRequestMessage(HttpMethod.Get, TestUri);

            restRequest.Headers.Add(AWSSignerHelper.HostHeaderName, "foobar");

            awsSignerHelperUnderTest.InitializeHeaders(restRequest);
            
            string HostHeaderValue = restRequest.Headers.GetValues(AWSSignerHelper.HostHeaderName).FirstOrDefault();

            Assert.Equal(TestHost, HostHeaderValue);
        }
    }
}
