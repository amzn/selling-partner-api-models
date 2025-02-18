<?php

namespace SpApi\AuthAndAuth;

use JsonSerializable;

class LWAAccessTokenRequestMeta implements JsonSerializable
{
    private const GRANT_TYPE_SERIALIZED = "grant_type";
    private const CLIENT_ID_SERIALIZED = "client_id";
    private const CLIENT_SECRET_SERIALIZED = "client_secret";
    private const REFRESH_TOKEN_SERIALIZED = "refresh_token";
    private const SCOPE_SERIALIZED = "scope";

    private string $grantType;

    private ?string $refreshToken;

    private string $clientId;

    private string $clientSecret;

    private ?array $scopes;

    public function __construct(LWAAuthorizationCredentials $lwaAuthorizationCredentials)
    {
        $this->refreshToken = $lwaAuthorizationCredentials->getRefreshToken();
        $this->clientId = $lwaAuthorizationCredentials->getClientId();
        $this->clientSecret = $lwaAuthorizationCredentials->getClientSecret();
        $this->scopes = $lwaAuthorizationCredentials->getScopes();

        if (!empty($lwaAuthorizationCredentials->getScopes())) {
            $this->grantType = "client_credentials";
        } else {
            $this->grantType = "refresh_token";
        }
    }

    public function jsonSerialize(): array
    {
        return [
            static::GRANT_TYPE_SERIALIZED => $this->grantType,
            static::CLIENT_ID_SERIALIZED => $this->clientId,
            static::CLIENT_SECRET_SERIALIZED => $this->clientSecret,
            static::REFRESH_TOKEN_SERIALIZED => $this->refreshToken,
            static::SCOPE_SERIALIZED => $this->scopes ? implode(" ", $this->scopes) : null
        ];
    }

    public function getGrantType(): string
    {
        return $this->grantType;
    }

    public function setGrantType(string $grantType)
    {
        $this->grantType = $grantType;
    }

    public function getRefreshToken(): ?string
    {
        return $this->refreshToken;
    }

    public function setRefreshToken(string $refreshToken)
    {
        $this->refreshToken = $refreshToken;
    }

    public function getClientId(): string
    {
        return $this->clientId;
    }

    public function setClientId(string $clientId)
    {
        $this->clientId = $clientId;
    }

    public function getClientSecret(): string
    {
        return $this->clientSecret;
    }

    public function setClientSecret(string $clientSecret)
    {
        $this->clientSecret = $clientSecret;
    }

    public function getScopes(): ?array
    {
        return $this->scopes;
    }

    public function setScopes(?array $scopes)
    {
        $this->scopes = $scopes;
    }
}
