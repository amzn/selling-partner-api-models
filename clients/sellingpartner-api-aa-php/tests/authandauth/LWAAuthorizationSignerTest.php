<?php

namespace SpApi\Test\AuthAndAuth;

use GuzzleHttp\Client;
use GuzzleHttp\Handler\MockHandler;
use GuzzleHttp\HandlerStack;
use GuzzleHttp\Psr7\Request;
use GuzzleHttp\Psr7\Response;
use SpApi\AuthAndAuth\LWAAccessTokenCache;
use SpApi\AuthAndAuth\LWAAccessTokenRequestMeta;
use SpApi\AuthAndAuth\LWAAuthorizationCredentials;
use SpApi\AuthAndAuth\LWAAuthorizationSigner;
use SpApi\AuthAndAuth\LWAClient;
use SpApi\AuthAndAuth\ScopeConstants;
use PHPUnit\Framework\TestCase;
use Psr\Http\Message\ResponseInterface;

final class LWAAuthorizationSignerTest extends TestCase
{
    private const TEST_REFRESH_TOKEN = "rToken";
    private const TEST_CLIENT_SECRET = "cSecret";
    private const TEST_CLIENT_ID = "cId";
    private const TEST_ENDPOINT = "https://www.amazon.com/lwa";
    private const SCOPE_NOTIFICATIONS_API = ScopeConstants::SCOPE_NOTIFICATIONS_API;
    private const SCOPE_MIGRATION_API = ScopeConstants::SCOPE_MIGRATION_API;
    private const SELLER_TYPE_SELLER = "seller";
    private const SELLER_TYPE_SELLERLESS = "sellerless";

    private Request $request;

    private static ?LWAAuthorizationSigner $underTestSeller = null;
    private static ?LWAAuthorizationSigner $underTestSellerless = null;

    public static function setUpBeforeClass(): void
    {
        self::$underTestSeller = new LWAAuthorizationSigner(
            new LWAAuthorizationCredentials([
                "clientId" => self::TEST_CLIENT_ID,
                "clientSecret" => self::TEST_CLIENT_SECRET,
                "refreshToken" => self::TEST_REFRESH_TOKEN,
                "endpoint" => self::TEST_ENDPOINT
            ])
        );

        self::$underTestSellerless = new LWAAuthorizationSigner(
            new LWAAuthorizationCredentials([
                "clientId" => self::TEST_CLIENT_ID,
                "clientSecret" => self::TEST_CLIENT_SECRET,
                "scopes" => [
                    self::SCOPE_NOTIFICATIONS_API,
                    self::SCOPE_MIGRATION_API
                ],
                "endpoint" => self::TEST_ENDPOINT
            ])
        );
    }

    protected function setUp(): void
    {
        $this->request = new Request("GET", "https://www.amazon.com/api");
    }

    public function lwaSignerProvider(): array
    {
        return [
            [self::SELLER_TYPE_SELLER, &self::$underTestSeller],
            [self::SELLER_TYPE_SELLERLESS, &self::$underTestSellerless]
        ];
    }

    /**
     * @dataProvider lwaSignerProvider
     */
    public function testInitializeLWAClientWithConfiguredEndpoint(
        string $sellerType,
        LWAAuthorizationSigner $testAuthSigner
    ): void {
        $this->assertSame(
            self::TEST_ENDPOINT,
            $testAuthSigner->getLwaClient()->getEndpoint()
        );
    }

    /**
     * @dataProvider lwaSignerProvider
     */
    public function testRequestLWAAccessTokenFromConfiguration(
        string $sellerType,
        LWAAuthorizationSigner $testAuthSigner
    ) {
        $mockLWAClient = $this->createMock(LWAClient::class);
        $mockLWAClient
            ->method("getAccessToken")
            ->willReturnCallback(function (LWAAccessTokenRequestMeta &$lwaAccessTokenRequestMeta) use ($sellerType) {
                $this->assertEquals(
                    self::TEST_CLIENT_SECRET,
                    $lwaAccessTokenRequestMeta->getClientSecret()
                );
                $this->assertEquals(
                    self::TEST_CLIENT_ID,
                    $lwaAccessTokenRequestMeta->getClientId()
                );

                if ($sellerType == self::SELLER_TYPE_SELLER) {
                    $this->assertEmpty($lwaAccessTokenRequestMeta->getScopes());
                    $this->assertEquals("refresh_token", $lwaAccessTokenRequestMeta->getGrantType());
                    $this->assertEquals(
                        self::TEST_REFRESH_TOKEN,
                        $lwaAccessTokenRequestMeta->getRefreshToken()
                    );
                } elseif ($sellerType == self::SELLER_TYPE_SELLERLESS) {
                    $this->assertEquals(
                        [
                            self::SCOPE_NOTIFICATIONS_API,
                            self::SCOPE_MIGRATION_API
                        ],
                        $lwaAccessTokenRequestMeta->getScopes()
                    );
                    $this->assertEquals("client_credentials", $lwaAccessTokenRequestMeta->getGrantType());
                    $this->assertNull($lwaAccessTokenRequestMeta->getRefreshToken());
                }
                return "foo";
            });

        $testAuthSigner->setLwaClient($mockLWAClient);
        $testAuthSigner->sign($this->request);
    }

    /**
     * @dataProvider lwaSignerProvider
     */
    public function testReturnSignedRequestWithAccessTokenFromLWAClient(
        string $sellerType,
        LWAAuthorizationSigner $testAuthSigner
    ) {
        $mockLWAClient = $this->createMock(LWAClient::class);

        $mockLWAClient->method("getAccessToken")->willReturn("Azta|Foo");

        $testAuthSigner->setLwaClient($mockLWAClient);
        $actualSignedRequest = $testAuthSigner->sign($this->request);
        $this->assertEquals("Azta|Foo", $actualSignedRequest->getHeader("x-amz-access-token")[0]);
    }

    /**
     * @dataProvider lwaSignerProvider
     */
    public function testOriginalRequestIsImmutable(string $sellerType, LWAAuthorizationSigner $testAuthSigner)
    {
        $mockLWAClient = $this->createMock(LWAClient::class);

        $mockLWAClient->method("getAccessToken")->willReturn("Azta|Foo");

        $testAuthSigner->setLwaClient($mockLWAClient);
        $this->assertNotSame($this->request, $testAuthSigner->sign($this->request));
    }

    public function testReturnSignedRequestWithAccessTokenFromLWACache()
    {
        $mockHandler = new MockHandler([
            $this->buildResponse(200, "Azta|foo", "120"),
            $this->buildResponse(200, "Azta|foo1", "1")
        ]);

        $handlerStack = HandlerStack::create($mockHandler);
        $mockGuzzleClient = new Client(["handler" => $handlerStack]);

        $testLWAClient = new LWAClient(self::TEST_ENDPOINT);
        $testLWAClient->setClient($mockGuzzleClient);

        $testLWACache = new LWAAccessTokenCache();

        $testlwaSigner = new LWAAuthorizationSigner(
            new LWAAuthorizationCredentials([
                "clientId" => self::TEST_CLIENT_ID,
                "clientSecret" => self::TEST_CLIENT_SECRET,
                "refreshToken" => self::TEST_REFRESH_TOKEN,
                "endpoint" => self::TEST_ENDPOINT
            ]),
            $testLWACache
        );

        $testlwaSigner->setLwaClient($testLWAClient);
        $testLWAClient->setLWAAccessTokenCache($testLWACache);
        $actualSignedRequest = $testlwaSigner->sign($this->request);
        $actualSignedSecondRequest = $testlwaSigner->sign($this->request);

        $this->assertEquals("Azta|foo", $actualSignedSecondRequest->getHeader("x-amz-access-token")[0]);
    }

    private function buildResponse(int $code, string $accessToken, string $expiryInSeconds = "3600"): ResponseInterface
    {
        $contentHeader = [
            "Content-Type" => "application/json",
        ];
        return new Response(
            $code,
            $contentHeader,
            json_encode(["access_token" => $accessToken, "expires_in" => $expiryInSeconds])
        );
    }
}
