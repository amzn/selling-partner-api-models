package com.amazon.SellingPartnerAPIAA;


import org.junit.Test;

import java.util.Collections;

import static org.junit.Assert.assertEquals;

public class LWAAccessTokenRequestMetaTest {

    @Test
    public void testEquals() {
        LWAAccessTokenRequestMeta meta1 = new LWAAccessTokenRequestMeta(
                "test","test","test","test",
                new LWAClientScopes(Collections.emptySet()));
        LWAAccessTokenRequestMeta meta2 = new LWAAccessTokenRequestMeta(
                "test","test","test","test",
                new LWAClientScopes(Collections.emptySet()));
        assertEquals(meta1, meta2);
    }
}