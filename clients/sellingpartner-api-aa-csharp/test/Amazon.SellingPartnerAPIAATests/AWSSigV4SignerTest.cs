using Moq;
using Xunit;
using RestSharp;
using Amazon.SellingPartnerAPIAA;
using System;

namespace Amazon.SellingPartnerAPIAATests
{
    public class AWSSigV4SignerTest
    {
        private const string TestAccessKeyId = "aKey";
        private const string TestSecretKey = "sKey";
        private const string TestRegion = "us-east-1";
        private const string TestResourcePath = "iam/user";
        private const string TestHost = "sellingpartnerapi.amazon.com";

        private RestRequest request;
        private AWSSigV4Signer sigV4SignerUnderTest;
        private Mock<AWSSignerHelper> mockAWSSignerHelper;

        public AWSSigV4SignerTest()
        {
            request = new RestRequest(TestResourcePath, Method.GET);

            AWSAuthenticationCredentials authenticationCredentials = new AWSAuthenticationCredentials
            {
                AccessKeyId = TestAccessKeyId,
                SecretKey = TestSecretKey,
                Region = TestRegion
            };
            mockAWSSignerHelper = new Mock<AWSSignerHelper>();
            sigV4SignerUnderTest = new AWSSigV4Signer(authenticationCredentials);
            sigV4SignerUnderTest.AwsSignerHelper = mockAWSSignerHelper.Object;
        }

        [Fact]
        public void TestSignRequest()
        {
            DateTime signingDate = DateTime.UtcNow;
            string expectedHashedCanonicalRequest = "b7a5ea4c3179fcebed77f19ccd7d85795d4b7a1810709b55fa7ad3fd79ab6adc";
            string expectedSignedHeaders = "testSignedHeaders";
            string expectedSignature = "testSignature";
            string expectedStringToSign = "testStringToSign";
            mockAWSSignerHelper.Setup(signerHelper => signerHelper.InitializeHeaders(request, TestHost))
                               .Returns(signingDate);
            mockAWSSignerHelper.Setup(signerHelper => signerHelper.ExtractCanonicalURIParameters(request))
                               .Returns("testURIParameters");
            mockAWSSignerHelper.Setup(signerHelper => signerHelper.ExtractCanonicalQueryString(request))
                               .Returns("testCanonicalQueryString");
            mockAWSSignerHelper.Setup(signerHelper => signerHelper.ExtractCanonicalHeaders(request))
                               .Returns("testCanonicalHeaders");
            mockAWSSignerHelper.Setup(signerHelper => signerHelper.ExtractSignedHeaders(request))
                               .Returns(expectedSignedHeaders);
            mockAWSSignerHelper.Setup(signerHelper => signerHelper.HashRequestBody(request))
                               .Returns("testHashRequestBody");
            mockAWSSignerHelper.Setup(signerHelper => signerHelper.BuildStringToSign(signingDate,
                                      expectedHashedCanonicalRequest, TestRegion))
                               .Returns(expectedStringToSign);
            mockAWSSignerHelper.Setup(signerHelper => signerHelper.CalculateSignature(expectedStringToSign,
                                      signingDate, TestSecretKey, TestRegion))
                               .Returns(expectedSignature);

            IRestRequest actualRestRequest = sigV4SignerUnderTest.Sign(request, TestHost);

            mockAWSSignerHelper.Verify(signerHelper => signerHelper.InitializeHeaders(request, TestHost));
            mockAWSSignerHelper.Verify(signerHelper => signerHelper.ExtractCanonicalURIParameters(request));
            mockAWSSignerHelper.Verify(signerHelper => signerHelper.ExtractCanonicalQueryString(request));
            mockAWSSignerHelper.Verify(signerHelper => signerHelper.ExtractCanonicalHeaders(request));
            mockAWSSignerHelper.Verify(signerHelper => signerHelper.ExtractSignedHeaders(request));
            mockAWSSignerHelper.Verify(signerHelper => signerHelper.HashRequestBody(request));
            mockAWSSignerHelper.Verify(signerHelper => signerHelper.BuildStringToSign(signingDate,
                                                                                      expectedHashedCanonicalRequest,
                                                                                      TestRegion));
            mockAWSSignerHelper.Verify(signerHelper => signerHelper.CalculateSignature(expectedStringToSign,
                                                                                       signingDate,
                                                                                       TestSecretKey,
                                                                                       TestRegion));
            mockAWSSignerHelper.Verify(signerHelper => signerHelper.AddSignature(request,
                                                                                 TestAccessKeyId,
                                                                                 expectedSignedHeaders,
                                                                                 expectedSignature,
                                                                                 TestRegion,
                                                                                 signingDate));

            Assert.Equal(request, actualRestRequest);
        }
    }
}
