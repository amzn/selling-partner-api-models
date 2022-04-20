using System;
using System.Collections.Generic;
using System.Text;

namespace Amazon.SellingPartnerAPIAA
{
    public class RateLimitConfigurationOnRequests : RateLimitConfiguration
    {
        /**
     * RateLimiter Permit
     */
        public int RateLimitPermit;

        /**
         * Timeout for RateLimiter
         */
        public int WaitTimeOutInMilliSeconds;

        public int getRateLimitPermit()
        {
            return RateLimitPermit;
        }

        public int getTimeOut()
        {
            return WaitTimeOutInMilliSeconds;
        }
    }
}