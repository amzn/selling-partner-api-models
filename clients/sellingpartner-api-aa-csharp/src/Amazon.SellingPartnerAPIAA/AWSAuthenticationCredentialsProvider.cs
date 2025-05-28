using System;
using System.Collections.Generic;
using System.Text;

namespace Amazon.SellingPartnerAPIAA
{
    /**
    * AWSAuthenticationCredentialsProvider
    */
    public class AWSAuthenticationCredentialsProvider
    {
        /**
        * AWS IAM Role ARN
        */
        public String RoleArn { get; set; }

        /**
         * AWS IAM Role Session Name
        */
        public String RoleSessionName { get; set; }
    }
}
