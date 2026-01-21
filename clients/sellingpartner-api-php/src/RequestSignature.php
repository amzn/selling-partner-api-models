<?php


declare(strict_types=1);

namespace Application;

use Aws\Credentials\Credentials;
use Aws\Signature\SignatureV4;
use Aws\Sts\StsClient;
use Psr\Http\Message\RequestInterface;

class RequestSignature
{
    private StsClient $stsClient;
    private SignatureV4 $signatureV4;
    private string $roleArn;
    private ?Credentials $credentials = null;

    public function __construct(
        StsClient $stsClient,
        SignatureV4 $signatureV4,
        string $roleArn
    )
    {
        $this->stsClient = $stsClient;
        $this->signatureV4 = $signatureV4;
        $this->roleArn = $roleArn;
    }

    public function signRequest(RequestInterface $request): RequestInterface
    {
        $this->init();
        $request = $this->signatureV4->signRequest($request, $this->credentials);
        return $request;
    }

    // asumeRoleandGetCredentialsForSignature needed just once
    private function init()
    {
        if (null === $this->credentials) {
            $this->credentials = $this->asumeRoleAndGetCredentialsForSignature();
        }
    }

    private function asumeRoleAndGetCredentialsForSignature(): Credentials
    {
        $stsResult = $this->stsClient->assumeRole(['RoleArn' => $this->roleArn, 'RoleSessionName' => 'api-session']);

        $credentialsRes = $stsResult->get('Credentials');

        return new Credentials(
            $credentialsRes['AccessKeyId'],
            $credentialsRes['SecretAccessKey'],
            $credentialsRes['SessionToken']
        );
    }
}
