<?php

namespace SpApi\Test\AuthAndAuth;

use Aws\Credentials\AssumeRoleCredentialProvider;
use Aws\Credentials\Credentials;
use Aws\Credentials\CredentialsInterface;
use Aws\Signature\SignatureV4;
use GuzzleHttp\Promise\Promise;
use GuzzleHttp\Psr7\Request;
use PHPUnit\Framework\MockObject\MockObject;
use SpApi\AuthAndAuth\AWSAuthenticationCredentials;
use SpApi\AuthAndAuth\AWSAuthenticationCredentialsProvider;
use SpApi\AuthAndAuth\AWSSigV4Signer;
use PHPUnit\Framework\TestCase;
use Psr\Http\Message\RequestInterface;

final class AWSSigV4SignerTest extends TestCase
{
    private const TEST_ACCESS_KEY_ID = "aKey";
    private const TEST_SECRET_KEY = "sKey";
    private const TEST_REGION = "us-east";
    private const TEST_ROLE_ARN = "arnrole";
    private const TEST_SESSION_NAME = "test";

    private MockObject|SignatureV4 $mockAWS4Signer;

    private AWSSigV4Signer $underTest;

    protected function setUp(): void
    {
        $this->mockAWS4Signer = $this->createMock(SignatureV4::class);
        $this->underTest = new AWSSigV4Signer(
            new AWSAuthenticationCredentials([
                "accessKeyId" => self::TEST_ACCESS_KEY_ID,
                "secretKey" => self::TEST_SECRET_KEY,
                "region" => self::TEST_REGION
            ])
        );
    }

    public function testSignRequestUsingProvidedCredentials()
    {
        $this->underTest->setAws4Signer($this->mockAWS4Signer);

        $this->mockAWS4Signer
            ->method("signRequest")
            ->willReturnCallback(function (
                RequestInterface $requestInput,
                CredentialsInterface $credentials
            ) {
                $this->assertEquals(self::TEST_ACCESS_KEY_ID, $credentials->getAccessKeyId());
                $this->assertEquals(self::TEST_SECRET_KEY, $credentials->getSecretKey());
                return $requestInput;
            });
        $this->underTest->sign(new Request("GET", "https://api.amazon.com"));
    }

    public function testReturnSignedRequest()
    {
        $this->underTest->setAws4Signer($this->mockAWS4Signer);

        $this->mockAWS4Signer
            ->method("signRequest")
            ->willReturnCallback(function (
                RequestInterface $requestInput,
                CredentialsInterface $credentials
            ) {
                return $requestInput;
            });

        $actualSignedRequest = $this->underTest->sign(new Request("GET", "http://api.amazon.com"));

        $this->mockAWS4Signer
            ->method("signRequest")
            ->willReturnCallback(function (
                RequestInterface $requestInput,
                CredentialsInterface $credentials
            ) use ($actualSignedRequest) {
                $this->assertEquals($requestInput->getUri(), $actualSignedRequest->getUri());
                return $requestInput;
            });

        $this->underTest->sign(new Request("GET", "http://api.amazon.com"));
    }

    public function testReturnSignedRequestWithCredentialProvider()
    {
        $mockAWSCredentialsProvider = $this->createMock(AssumeRoleCredentialProvider::class);
        $mockCredentials = $this->createMock(Credentials::class);

        $promise = new Promise();
        $promise->resolve($mockCredentials);

        $underTest = new AWSSigV4Signer(
            new AWSAuthenticationCredentials([
                "accessKeyId" => self::TEST_ACCESS_KEY_ID,
                "secretKey" => self::TEST_SECRET_KEY,
                "region" => self::TEST_REGION
            ]),
            new AWSAuthenticationCredentialsProvider([
                "roleArn" => self::TEST_ROLE_ARN,
                "roleSessionName" => self::TEST_SESSION_NAME
            ])
        );

        $mockAWSCredentialsProvider
            ->method("__invoke")
            ->willReturn($promise);

        $underTest->setAws4Signer($this->mockAWS4Signer);
        $underTest->setAwsCredentialsProvider($mockAWSCredentialsProvider);

        $this->mockAWS4Signer
            ->method("signRequest")
            ->willReturnCallback(function (
                RequestInterface $requestInput,
                CredentialsInterface $credentials
            ) {
                return $requestInput;
            });

        $actualSignedRequest = $underTest->sign(new Request("GET", "http://api.amazon.com"));

        $this->mockAWS4Signer
            ->method("signRequest")
            ->willReturnCallback(function (
                RequestInterface $requestInput,
                CredentialsInterface $credentials
            ) use ($actualSignedRequest) {
                $this->assertEquals($requestInput->getUri(), $actualSignedRequest->getUri());
                return $requestInput;
            });

        $underTest->sign(new Request("GET", "http://api.amazon.com"));
    }
}

