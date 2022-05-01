package com.amazon.SellingPartnerAPIAA;

import lombok.Data;

@Data
public class LWAAccessTokenCacheItem {

    private String accessToken;
    private long accessTokenExpiredTime;

}
