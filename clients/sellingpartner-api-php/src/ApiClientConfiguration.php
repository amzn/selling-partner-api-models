<?php

/**
 * BCP
 *
 * @package SandboxApp
 * @license Proprietary Software
 * @author  Pavlo Cherniavskyi
 *
 */

declare(strict_types=1);

namespace Application;

class ApiClientConfiguration
{
    private string $host;
    private string $appClientId;
    private string $appClientSecret;

    public function __construct(string $host)
    {
        $this->host = $host;
    }

    public function getHost(): string
    {
        return $this->host;
    }

    public function setHost(string $host): ApiClientConfiguration
    {
        $this->host = $host;
        return $this;
    }

    public function getAppClientId(): string
    {
        return $this->appClientId;
    }

    public function setAppClientId(string $appClientId): ApiClientConfiguration
    {
        $this->appClientId = $appClientId;
        return $this;
    }

    public function getAppClientSecret(): string
    {
        return $this->appClientSecret;
    }

    public function setAppClientSecret(string $appClientSecret): ApiClientConfiguration
    {
        $this->appClientSecret = $appClientSecret;
        return $this;
    }

}
