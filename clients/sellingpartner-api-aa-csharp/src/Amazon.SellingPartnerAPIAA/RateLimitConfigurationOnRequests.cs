using System;
using System.Collections.Generic;
using System.Text;

namespace Amazon.SellingPartnerAPIAA
{
    public class RateLimitConfigurationOnRequests : RateLimitConfiguration
    {
        /// <summary>
        /// Burst rate for the operation path
        /// </summary>
        public int BurstRate;

        /// <summary>
        /// The rate at which tokens are replenished to the bucket
        /// </summary>
        public double ReplenishRate;

        /// <summary>
        /// Maximum size of the token bucket
        /// </summary>
        /// <returns>int</returns>
        public int getRateLimitPermit()
        {
            return BurstRate;
        }

        /// <summary>
        /// Timeout for the cancellation token passed to the TimeLimiter
        /// </summary>
        public int getTimeOut()
        {
            return (int)(getInterval() * 1000 + 1000);
        }

        /// <summary>
        /// The rate in milliseconds at which calls should be made in order to avoid throttling
        /// </summary>
        /// <returns></returns>
        public double getInterval()
        {
            return 1 / ReplenishRate;
        }
    }
}