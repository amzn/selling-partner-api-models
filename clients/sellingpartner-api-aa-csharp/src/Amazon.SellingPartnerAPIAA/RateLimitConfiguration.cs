using System;
using System.Collections.Generic;
using System.Text;

namespace Amazon.SellingPartnerAPIAA
{
    public interface RateLimitConfiguration
    {
        int getRateLimitPermit();
        int getTimeOut();
    }
}