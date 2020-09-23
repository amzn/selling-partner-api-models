package com.amazon.SellingPartnerAPIAA;

public interface LWAAccessTokenCache {
  String get(Object key);
  void put(Object key, String accessToken, long tokenTTLInSeconds);
}
