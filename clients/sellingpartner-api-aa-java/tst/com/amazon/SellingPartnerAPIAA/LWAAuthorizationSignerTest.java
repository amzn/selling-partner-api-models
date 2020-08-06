package com.amazon.SellingPartnerAPIAA;

import com.squareup.okhttp.Request;
import org.junit.Assert;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.Arguments;
import org.junit.jupiter.params.provider.MethodSource;
import org.mockito.ArgumentCaptor;

import java.util.Arrays;
import java.util.HashSet;
import java.util.stream.Stream;

import static com.amazon.SellingPartnerAPIAA.ScopeConstants.SCOPE_MIGRATION_API;
import static com.amazon.SellingPartnerAPIAA.ScopeConstants.SCOPE_NOTIFICATIONS_API;
import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertNotSame;
import static org.mockito.ArgumentMatchers.any;
import static org.mockito.Mockito.mock;
import static org.mockito.Mockito.when;

public class LWAAuthorizationSignerTest {
    private static final String TEST_REFRESH_TOKEN = "rToken";
    private static final String TEST_CLIENT_SECRET = "cSecret";
    private static final String TEST_CLIENT_ID = "cId";
    private static final String TEST_ENDPOINT = "https://www.amazon.com/lwa";
    private static final String TEST_SCOPE_1 = SCOPE_NOTIFICATIONS_API;
    private static final String TEST_SCOPE_2 = SCOPE_MIGRATION_API;
    private static final String SELLER_TYPE_SELLER = "seller";
    private static final String SELLER_TYPE_SELLERLESS = "sellerless";


    private Request request;
    private static LWAAuthorizationSigner underTestSeller;
    private static LWAAuthorizationSigner underTestSellerless;

    static {

        underTestSeller = new LWAAuthorizationSigner(LWAAuthorizationCredentials.builder()
                .clientId(TEST_CLIENT_ID)
                .clientSecret(TEST_CLIENT_SECRET)
                .refreshToken(TEST_REFRESH_TOKEN)
                .endpoint(TEST_ENDPOINT)
                .build());

        underTestSellerless = new LWAAuthorizationSigner(LWAAuthorizationCredentials.builder()
                .clientId(TEST_CLIENT_ID)
                .clientSecret(TEST_CLIENT_SECRET)
                .withScopes(TEST_SCOPE_1, TEST_SCOPE_2)
                .endpoint(TEST_ENDPOINT)
                .build());
    }

    @BeforeEach
    public void init() {
        request = new Request.Builder()
                .url("https://www.amazon.com/api")
                .build();

    }

    public static Stream<Arguments> lwaAuthSigner(){

        return Stream.of(
                Arguments.of(SELLER_TYPE_SELLER, underTestSeller),
                Arguments.of(SELLER_TYPE_SELLERLESS, underTestSellerless)
        );
    }

    @ParameterizedTest
    @MethodSource("lwaAuthSigner")
    public void initializeLWAClientWithConfiguredEndpoint(String sellerType, LWAAuthorizationSigner testAuthSigner) {
        assertEquals(TEST_ENDPOINT, testAuthSigner.getLwaClient().getEndpoint());
    }

    @ParameterizedTest
    @MethodSource("lwaAuthSigner")
    public void requestLWAAccessTokenFromConfiguration(String sellerType, LWAAuthorizationSigner testAuthSigner) {
        LWAClient mockLWAClient = mock(LWAClient.class);
        ArgumentCaptor<LWAAccessTokenRequestMeta> lwaAccessTokenRequestMetaArgumentCaptor = ArgumentCaptor.forClass(
                LWAAccessTokenRequestMeta.class);

        when(mockLWAClient.getAccessToken(lwaAccessTokenRequestMetaArgumentCaptor.capture()))
                .thenReturn("foo");

        testAuthSigner.setLwaClient(mockLWAClient);
        testAuthSigner.sign(request);

        LWAAccessTokenRequestMeta actualLWAAccessTokenRequestMeta = lwaAccessTokenRequestMetaArgumentCaptor.getValue();
        assertEquals(TEST_REFRESH_TOKEN, actualLWAAccessTokenRequestMeta.getRefreshToken());
        assertEquals(TEST_CLIENT_SECRET, actualLWAAccessTokenRequestMeta.getClientSecret());
        assertEquals(TEST_CLIENT_ID, actualLWAAccessTokenRequestMeta.getClientId());

        if(sellerType.equals(SELLER_TYPE_SELLER)){
            Assert.assertTrue(actualLWAAccessTokenRequestMeta.getScopes().getScopes().isEmpty());
            assertEquals("refresh_token", actualLWAAccessTokenRequestMeta.getGrantType());
        }
        else if (sellerType.equals(SELLER_TYPE_SELLERLESS)){
            assertEquals(new HashSet<String>(Arrays.asList(TEST_SCOPE_1, TEST_SCOPE_2)), actualLWAAccessTokenRequestMeta.getScopes().getScopes());
            assertEquals("client_credentials", actualLWAAccessTokenRequestMeta.getGrantType());
        }

    }

    @ParameterizedTest
    @MethodSource("lwaAuthSigner")
    public void returnSignedRequestWithAccessTokenFromLWAClient(String sellerType, LWAAuthorizationSigner testAuthSigner) {
        LWAClient mockLWAClient = mock(LWAClient.class);

        when(mockLWAClient.getAccessToken(any(LWAAccessTokenRequestMeta.class)))
                .thenReturn("Azta|Foo");

        testAuthSigner.setLwaClient(mockLWAClient);
        Request actualSignedRequest = testAuthSigner.sign(request);

        assertEquals("Azta|Foo", actualSignedRequest.header("x-amz-access-token"));
    }

    @ParameterizedTest
    @MethodSource("lwaAuthSigner")
    public void originalRequestIsImmutable(String sellerType, LWAAuthorizationSigner testAuthSigner) {
        LWAClient mockLWAClient = mock(LWAClient.class);

        when(mockLWAClient.getAccessToken(any(LWAAccessTokenRequestMeta.class)))
                .thenReturn("Azta|Foo");

        testAuthSigner.setLwaClient(mockLWAClient);
        assertNotSame(request, testAuthSigner.sign(request));
    }
}
