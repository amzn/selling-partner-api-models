using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Amazon.SellingPartnerAPIAA
{
    public class AWSSignerHelper
    {
        public const string ISO8601BasicDateTimeFormat = "yyyyMMddTHHmmssZ";
        public const string ISO8601BasicDateFormat = "yyyyMMdd";

        public const string XAmzDateHeaderName = "X-Amz-Date";
        public const string AuthorizationHeaderName = "Authorization";
        public const string CredentialSubHeaderName = "Credential";
        public const string SignatureSubHeaderName = "Signature";
        public const string SignedHeadersSubHeaderName = "SignedHeaders";
        public const string HostHeaderName = "host";

        public const string Scheme = "AWS4";
        public const string Algorithm = "HMAC-SHA256";
        public const string TerminationString = "aws4_request";
        public const string ServiceName = "execute-api";
        public const string Slash = "/";

        private readonly static Regex CompressWhitespaceRegex = new Regex("\\s+");

        public virtual IDateHelper DateHelper { get; set; }

        public AWSSignerHelper()
        {
            DateHelper = new SigningDateHelper();
        }

        /// <summary>
        /// Returns URI encoded version of absolute path
        /// </summary>
        /// <param name="request">RestRequest</param>
        /// <returns>URI encoded version of absolute path</returns>
        public virtual string ExtractCanonicalURIParameters(IRestRequest request)
        {
            string resource = request.Resource;
            string canonicalUri = string.Empty;

            if (string.IsNullOrEmpty(resource))
            {
                canonicalUri = Slash;
            }
            else
            {
                if (!resource.StartsWith(Slash))
                {
                    canonicalUri = Slash;
                }
                IDictionary<string, string> pathParameters = request.Parameters
                        .Where(parameter => ParameterType.UrlSegment.Equals(parameter.Type))
                        .ToDictionary(parameter => parameter.Name.Trim().ToString(), parameter => parameter.Value.ToString());

                // Replace path parameter with actual value.
                // Ex: /products/pricing/v0/items/{Asin}/offers -> /products/pricing/v0/items/AB12CD3E4Z/offers
                foreach (string parameter in pathParameters.Keys)
                {
                    resource = resource.Replace("{" + parameter + "}", pathParameters[parameter]);
                }

                //Split path at / into segments
                IEnumerable<string> encodedSegments = resource.Split(new char[] { '/' }, StringSplitOptions.None);

                // Encode twice
                encodedSegments = encodedSegments.Select(segment => Utils.UrlEncode(segment));
                encodedSegments = encodedSegments.Select(segment => Utils.UrlEncode(segment));

                canonicalUri += string.Join(Slash, encodedSegments.ToArray());
            }

            return canonicalUri;
        }

        /// <summary>
        /// Returns query parameters in canonical order with URL encoding
        /// </summary>
        /// <param name="request">RestRequest</param>
        /// <returns>Query parameters in canonical order with URL encoding</returns>
        public virtual string ExtractCanonicalQueryString(IRestRequest request)
        {
            IDictionary<string, string> queryParameters = request.Parameters
                .Where(parameter => ParameterType.QueryString.Equals(parameter.Type))
                .ToDictionary(parameter => parameter.Name.Trim().ToString(), parameter => parameter.Value.ToString());

            SortedDictionary<string, string> sortedQueryParameters = new SortedDictionary<string, string>(queryParameters);

            StringBuilder canonicalQueryString = new StringBuilder();
            foreach (var key in sortedQueryParameters.Keys)
            {
                if (canonicalQueryString.Length > 0)
                {
                    canonicalQueryString.Append("&");
                }
                canonicalQueryString.AppendFormat("{0}={1}",
                    Utils.UrlEncode(key),
                    Utils.UrlEncode(sortedQueryParameters[key]));
            }

            return canonicalQueryString.ToString();
        }

        /// <summary>
        /// Returns Http headers in canonical order with all header names to lowercase
        /// </summary>
        /// <param name="request">RestRequest</param>
        /// <returns>Returns Http headers in canonical order</returns>
        public virtual string ExtractCanonicalHeaders(IRestRequest request)
        {
            IDictionary<string, string> headers = request.Parameters
                .Where(parameter => ParameterType.HttpHeader.Equals(parameter.Type))
                .ToDictionary(header => header.Name.Trim().ToLowerInvariant(), header => header.Value.ToString());

            SortedDictionary<string, string> sortedHeaders = new SortedDictionary<string, string>(headers);

            StringBuilder headerString = new StringBuilder();

            foreach (string headerName in sortedHeaders.Keys)
            {
                headerString.AppendFormat("{0}:{1}\n",
                    headerName,
                    CompressWhitespaceRegex.Replace(sortedHeaders[headerName].Trim(), " "));
            }

            return headerString.ToString();
        }

        /// <summary>
        /// Returns list(as string) of Http headers in canonical order
        /// </summary>
        /// <param name="request">RestRequest</param>
        /// <returns>List of Http headers in canonical order</returns>
        public virtual string ExtractSignedHeaders(IRestRequest request)
        {
            List<string> rawHeaders = request.Parameters.Where(parameter => ParameterType.HttpHeader.Equals(parameter.Type))
                                                        .Select(header => header.Name.Trim().ToLowerInvariant())
                                                        .ToList();
            rawHeaders.Sort(StringComparer.OrdinalIgnoreCase);

            return string.Join(";", rawHeaders);
        }

        /// <summary>
        /// Returns hexadecimal hashed value(using SHA256) of payload in the body of request
        /// </summary>
        /// <param name="request">RestRequest</param>
        /// <returns>Hexadecimal hashed value of payload in the body of request</returns>
        public virtual string HashRequestBody(IRestRequest request)
        {
            var body = request.Parameters.FirstOrDefault(parameter => ParameterType.RequestBody.Equals(parameter.Type));
            string value = body != null ? body.Value.ToString() : string.Empty;
            return Utils.ToHex(Utils.Hash(value));
        }

        /// <summary>
        /// Builds the string for signing using signing date, hashed canonical request and region
        /// </summary>
        /// <param name="signingDate">Signing Date</param>
        /// <param name="hashedCanonicalRequest">Hashed Canonical Request</param>
        /// <param name="region">Region</param>
        /// <returns>String to be used for signing</returns>
        public virtual string BuildStringToSign(DateTime signingDate, string hashedCanonicalRequest, string region)
        {
            string scope = BuildScope(signingDate, region);
            string stringToSign = string.Format(CultureInfo.InvariantCulture, "{0}-{1}\n{2}\n{3}\n{4}",
                Scheme,
                Algorithm,
                signingDate.ToString(ISO8601BasicDateTimeFormat, CultureInfo.InvariantCulture),
                scope,
                hashedCanonicalRequest);

            return stringToSign;
        }

        /// <summary>
        /// Sets AWS4 mandated 'x-amz-date' header, returning the date/time that will
        /// be used throughout the signing process.
        /// </summary>
        /// <param name="restRequest">RestRequest</param>
        /// <param name="host">Request endpoint</param>
        /// <returns>Date and time used for x-amz-date, in UTC</returns>
        public virtual DateTime InitializeHeaders(IRestRequest restRequest, string host)
        {
            restRequest.Parameters.RemoveAll(parameter => ParameterType.HttpHeader.Equals(parameter.Type)
                                                          && parameter.Name == XAmzDateHeaderName);
            restRequest.Parameters.RemoveAll(parameter => ParameterType.HttpHeader.Equals(parameter.Type)
                                                          && parameter.Name == HostHeaderName);

            DateTime signingDate = DateHelper.GetUtcNow();

            restRequest.AddHeader(XAmzDateHeaderName, signingDate.ToString(ISO8601BasicDateTimeFormat, CultureInfo.InvariantCulture));
            restRequest.AddHeader(HostHeaderName, host);

            return signingDate;
        }

        /// <summary>
        /// Calculates AWS4 signature for the string, prepared for signing
        /// </summary>
        /// <param name="stringToSign">String to be signed</param>
        /// <param name="signingDate">Signing Date</param>
        /// <param name="secretKey">Secret Key</param>
        /// <param name="region">Region</param>
        /// <returns>AWS4 Signature</returns>
        public virtual string CalculateSignature(string stringToSign,
                                                 DateTime signingDate,
                                                 string secretKey,
                                                 string region)
        {
            string date = signingDate.ToString(ISO8601BasicDateFormat, CultureInfo.InvariantCulture);
            byte[] kSecret = Encoding.UTF8.GetBytes(Scheme + secretKey);
            byte[] kDate = Utils.GetKeyedHash(kSecret, date);
            byte[] kRegion = Utils.GetKeyedHash(kDate, region);
            byte[] kService = Utils.GetKeyedHash(kRegion, ServiceName);
            byte[] kSigning = Utils.GetKeyedHash(kService, TerminationString);

            // Calculate the signature
            return Utils.ToHex(Utils.GetKeyedHash(kSigning, stringToSign));
        }

        /// <summary>
        /// Add a signature to a request in the form of an 'Authorization' header
        /// </summary>
        /// <param name="restRequest">Request to be signed</param>
        /// <param name="accessKeyId">Access Key Id</param>
        /// <param name="signedHeaders">Signed Headers</param>
        /// <param name="signature">The signature to add</param>
        /// <param name="region">AWS region for the request</param>
        /// <param name="signingDate">Signature date</param>
        public virtual void AddSignature(IRestRequest restRequest,
                                         string accessKeyId,
                                         string signedHeaders,
                                         string signature,
                                         string region,
                                         DateTime signingDate)
        {
            string scope = BuildScope(signingDate, region);
            StringBuilder authorizationHeaderValueBuilder = new StringBuilder();
            authorizationHeaderValueBuilder.AppendFormat("{0}-{1}", Scheme, Algorithm);
            authorizationHeaderValueBuilder.AppendFormat(" {0}={1}/{2},", CredentialSubHeaderName, accessKeyId, scope);
            authorizationHeaderValueBuilder.AppendFormat(" {0}={1},", SignedHeadersSubHeaderName, signedHeaders);
            authorizationHeaderValueBuilder.AppendFormat(" {0}={1}", SignatureSubHeaderName, signature);

            restRequest.AddHeader(AuthorizationHeaderName, authorizationHeaderValueBuilder.ToString());
        }

        private static string BuildScope(DateTime signingDate, string region)
        {
            return string.Format("{0}/{1}/{2}/{3}",
                                 signingDate.ToString(ISO8601BasicDateFormat, CultureInfo.InvariantCulture),
                                 region,
                                 ServiceName,
                                 TerminationString);
        }
    }
}
