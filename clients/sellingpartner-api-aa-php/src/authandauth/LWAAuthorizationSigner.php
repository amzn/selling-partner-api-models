<?php

namespace SpApi\AuthAndAuth;

use GuzzleHttp\Psr7\Request;

class LWAAuthorizationSigner
{
    public const SIGNED_ACCESS_TOKEN_HEADER_NAME = "x-amz-access-token";

    private LWAClient $lwaClient;

    private LWAAccessTokenRequestMeta $lwaAccessTokenRequestMeta;

    public function __construct(
        LWAAuthorizationCredentials $lwaAuthorizationCredentials,
        ?LWAAccessTokenCache $lwaAccessTokenCache = null
    ) {
        $this->lwaClient = new LWAClient($lwaAuthorizationCredentials->getEndpoint());
        $this->lwaAccessTokenRequestMeta = new LWAAccessTokenRequestMeta($lwaAuthorizationCredentials);
        $this->lwaClient->setLWAAccessTokenCache($lwaAccessTokenCache);
    }

    public function sign(Request $request): Request
    {
        $accessToken = $this->lwaClient->getAccessToken($this->lwaAccessTokenRequestMeta);
        $request = $request->withHeader(static::SIGNED_ACCESS_TOKEN_HEADER_NAME, $accessToken);
        return $request;
    }

    public function getLwaClient(): LWAClient
    {
        return $this->lwaClient;
    }

    public function setLwaClient(LWAClient $lwaClient): void
    {
        $this->lwaClient = $lwaClient;
    }
}
