<?php

namespace SpApi\Test\AuthAndAuth;

use Exception;
use GuzzleHttp\Client;
use GuzzleHttp\Handler\MockHandler;
use GuzzleHttp\HandlerStack;
use GuzzleHttp\Psr7\Request;
use GuzzleHttp\Psr7\Response;
use PHPUnit\Framework\MockObject\MockObject;
use RuntimeException;
use SpApi\AuthAndAuth\LWAAccessTokenCache;
use SpApi\AuthAndAuth\LWAAccessTokenRequestMeta;
use SpApi\AuthAndAuth\LWAAuthorizationCredentials;
use SpApi\AuthAndAuth\LWAClient;
use SpApi\AuthAndAuth\ScopeConstants;
use PHPUnit\Framework\TestCase;
use Psr\Http\Message\ResponseInterface;

final class LWAClientTest extends TestCase
{
    private const TEST_REFRESH_TOKEN = "rToken";
    private const TEST_CLIENT_SECRET = "cSecret";
    private const TEST_CLIENT_ID = "cId";
    private const SCOPE_NOTIFICATIONS_API = ScopeConstants::SCOPE_NOTIFICATIONS_API;
    private const SCOPE_MIGRATION_API = ScopeConstants::SCOPE_MIGRATION_API;
    private const SELLER_TYPE_SELLER = "seller";
    private const SELLER_TYPE_SELLERLESS = "sellerless";

    private const TEST_ENDPOINT = "https://www.amazon.com/api";
    private static ?LWAAccessTokenRequestMeta $lwaAccessTokenRequestMetaSeller = null;
    private static ?LWAAccessTokenRequestMeta $lwaAccessTokenRequestMetaSellerless = null;

    private Client|MockObject $mockGuzzleClient;
    private LWAClient $underTest;

    public static function setUpBeforeClass(): void
    {
        $lwaSellerCredentials = new LWAAuthorizationCredentials([
            "clientId" => self::TEST_CLIENT_ID,
            "clientSecret" => self::TEST_CLIENT_SECRET,
            "refreshToken" => self::TEST_REFRESH_TOKEN,
            "endpoint" => self::TEST_ENDPOINT
        ]);
        $lwaSellerlessCredentials = new LWAAuthorizationCredentials([
            "clientId" => self::TEST_CLIENT_ID,
            "clientSecret" => self::TEST_CLIENT_SECRET,
            "scopes" => [self::SCOPE_NOTIFICATIONS_API, self::SCOPE_MIGRATION_API],
            "endpoint" => self::TEST_ENDPOINT
        ]);
        self::$lwaAccessTokenRequestMetaSeller = new LWAAccessTokenRequestMeta($lwaSellerCredentials);
        self::$lwaAccessTokenRequestMetaSellerless = new LWAAccessTokenRequestMeta($lwaSellerlessCredentials);
    }

    protected function setUp(): void
    {
        $this->mockGuzzleClient = $this->createMock(Client::class);
        $this->underTest = new LWAClient(self::TEST_ENDPOINT);
        $this->underTest->setClient($this->mockGuzzleClient);
    }

    public function lwaClientProvider(): array
    {
        return [
            [self::SELLER_TYPE_SELLER, &self::$lwaAccessTokenRequestMetaSeller],
            [self::SELLER_TYPE_SELLERLESS, &self::$lwaAccessTokenRequestMetaSellerless]
        ];
    }

    public function testInitializeEndpoint()
    {
        $this->assertEquals(self::TEST_ENDPOINT, $this->underTest->getEndpoint());
    }

    /**
     * @dataProvider lwaClientProvider
     */
    public function testMakeRequestFromMeta(
        string $sellerType,
        LWAAccessTokenRequestMeta $testLwaAccessTokenRequestMeta
    ) {
        $this->mockGuzzleClient
            ->expects($this->once())
            ->method("send")
            ->willReturnCallback(function (Request $requestInput) use ($sellerType) {
                $this->assertEquals(self::TEST_ENDPOINT, $requestInput->getUri());
                $this->assertEquals("POST", $requestInput->getMethod());
                $requestJson = json_decode((string)$requestInput->getBody(), true);
                if ($sellerType == self::SELLER_TYPE_SELLER) {
                    $this->assertEquals("rToken", $requestJson["refresh_token"]);
                } elseif ($sellerType == self::SELLER_TYPE_SELLERLESS) {
                    $this->assertNotNull($requestJson["scope"]);
                }
                $this->assertEquals("cId", $requestJson["client_id"]);
                $this->assertEquals("cSecret", $requestJson["client_secret"]);
                $this->assertEquals("application/json", $requestInput->getHeader("Content-Type")[0]);
                return $this->buildResponse(200, "foo");
            });

        $this->underTest->setClient($this->mockGuzzleClient);
        $this->underTest->getAccessToken($testLwaAccessTokenRequestMeta);
    }

    /**
     * @dataProvider lwaClientProvider
     */
    public function testReturnAccessTokenFromResponse(
        string $sellerType,
        LWAAccessTokenRequestMeta $testLwaAccessTokenRequestMeta
    ) {
        $this->mockGuzzleClient
            ->method("send")
            ->willReturn(
                $this->buildResponse(200, "Azta|foo")
            );

        $this->assertEquals("Azta|foo", $this->underTest->getAccessToken($testLwaAccessTokenRequestMeta));
    }

    /**
     * @dataProvider lwaClientProvider
     */
    public function testUnsuccessfulPostThrowsException(
        string $sellerType,
        LWAAccessTokenRequestMeta $testLwaAccessTokenRequestMeta
    ) {
        //Guzzlehttp automatically throws exceptions for 400/500 level errors
        //Using phpunit mocking doesn't mock that behavior
        $mockHandler = new MockHandler([
            $this->buildResponse(400, "Azta|foo")
        ]);

        $handlerStack = HandlerStack::create($mockHandler);
        $mockGuzzleExceptionClient = new Client(["handler" => $handlerStack]);

        $exceptionTest = new LWAClient(self::TEST_ENDPOINT);
        $exceptionTest->setClient($mockGuzzleExceptionClient);

        $this->expectException(RuntimeException::class);

        $exceptionTest->getAccessToken($testLwaAccessTokenRequestMeta);
    }

    /**
     * @dataProvider lwaClientProvider
     */
    public function testMissingAccessTokenInResponseThrowsException(
        string $sellerType,
        LWAAccessTokenRequestMeta $testLwaAccessTokenRequestMeta
    ) {
        $this->mockGuzzleClient
            ->method("send")
            ->willReturn(
                $this->buildResponse(200, "")
            );

        $this->expectException(RuntimeException::class);

        $this->underTest->getAccessToken($testLwaAccessTokenRequestMeta);
    }

    public function testReturnAccessTokenFromCache()
    {
        $this->mockGuzzleClient
            ->method("send")
            ->willThrowException(new Exception())
            ->willReturn(
                $this->buildResponse(200, "Azta|foo", "120")
            );

        $this->underTest->setLWAAccessTokenCache(new LWAAccessTokenCache());

        //First call should get from Endpoint
        $this->assertEquals("Azta|foo", $this->underTest->getAccessToken(self::$lwaAccessTokenRequestMetaSeller));
        //Second call when the cache is still valid, if it goes to Endpoint it will return Exception
        $this->assertEquals("Azta|foo", $this->underTest->getAccessToken(self::$lwaAccessTokenRequestMetaSeller));
    }

    public function testReturnAccessTokenFromCacheWithExpiry()
    {
        $client = new LWAClient(self::TEST_ENDPOINT);
        $client->setClient($this->mockGuzzleClient);

        $this->mockGuzzleClient
            ->method("send")
            ->willReturnOnConsecutiveCalls(
                $this->buildResponse(200, "Azta|foo", "1"),
                $this->buildResponse(200, "Azta|foo1", "1")
            );

        //First call should get from Endpoint
        $this->assertEquals("Azta|foo", $client->getAccessToken(self::$lwaAccessTokenRequestMetaSeller));
        //Second call should again go to the Endpoint because accessToken is expired after expiry adjustment
        $this->assertEquals("Azta|foo1", $client->getAccessToken(self::$lwaAccessTokenRequestMetaSeller));
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
