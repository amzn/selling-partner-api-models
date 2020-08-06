namespace Amazon.SellingPartnerAPIAA
{
    public class AWSAuthenticationCredentials
    {
        /**
        * AWS IAM User Access Key Id
        */
        public string AccessKeyId { get; set; }

        /**
        * AWS IAM User Secret Key
        */
        public string SecretKey { get; set; }

        /**
        * AWS Region
        */
        public string Region { get; set; }
    }
}
