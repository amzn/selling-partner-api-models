<?php

namespace SpApi\AuthAndAuth;

use InvalidArgumentException;

class LWAAuthorizationCredentials
{
    /**
     * LWA Client Id
     * @var string
     */
    private string $clientId;

    /**
     * LWA Client Secret
     * @var string
     */
    private string $clientSecret;

    /**
     * LWA Refresh Token
     * @var string|null
     */
    private ?string $refreshToken;

    /**
     * LWA Authorization Server Endpoint
     * @var string
     */
    private string $endpoint;

    /**
     * LWA Client Scopes
     * @var array|null
     */
    private ?array $scopes;

    public function __construct(array $config)
    {
        if (!(isset($config['refreshToken']) || isset($config['scopes']))) {
            throw new InvalidArgumentException("Requires either refresh token or scopes variable");
        }
        $this->clientId = $config["clientId"];
        $this->clientSecret = $config["clientSecret"];
        $this->refreshToken = $config["refreshToken"] ?? null;
        $this->endpoint = $config["endpoint"];
        $this->scopes = $config["scopes"] ?? null;
    }

    public function getClientId(): string
    {
        return $this->clientId;
    }

    public function setClientId(string $clientId): LWAAuthorizationCredentials
    {
        $this->clientId = $clientId;
        return $this;
    }

    public function getClientSecret(): string
    {
        return $this->clientSecret;
    }

    public function setClientSecret(string $clientSecret): LWAAuthorizationCredentials
    {
        $this->clientSecret = $clientSecret;
        return $this;
    }

    public function getRefreshToken(): ?string
    {
        return $this->refreshToken;
    }

    public function setRefreshToken(?string $refreshToken): LWAAuthorizationCredentials
    {
        $this->refreshToken = $refreshToken;
        return $this;
    }

    public function getEndpoint(): string
    {
        return $this->endpoint;
    }

    public function setEndpoint(string $endpoint): LWAAuthorizationCredentials
    {
        $this->endpoint = $endpoint;
        return $this;
    }

    public function getScopes(): ?array
    {
        return $this->scopes;
    }

    public function setScopes(?array $scopes): LWAAuthorizationCredentials
    {
        $this->scopes = $scopes;
        return $this;
    }
}
