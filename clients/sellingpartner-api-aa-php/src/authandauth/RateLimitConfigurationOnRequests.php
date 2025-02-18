<?php

namespace SpApi\AuthAndAuth;

use InvalidArgumentException;

class RateLimitConfigurationOnRequests implements RateLimitConfiguration
{
    /**
     * RateLimiter Type
     */
    private string $rateLimitType;

    /**
     * RateLimiter Token
     */
    private int $rateLimitToken;

    /**
     * RateLimiter Token Limit
     */
    private int $rateLimitTokenLimit;

    /**
     * Timeout for RateLimiter
     */
    private float $waitTimeOutInMilliSeconds;

    public function __construct(array $config)
    {
        if (!(isset($config["rateLimitToken"]) && isset($config["rateLimitTokenLimit"]))) {
            throw new InvalidArgumentException("Both rateLimitToken and rateLimitTokenLimit are required");
        }
        $this->rateLimitType = $config["rateLimitType"] ?? "token_bucket";
        $this->rateLimitToken = $config["rateLimitToken"];
        $this->rateLimitTokenLimit = $config["rateLimitTokenLimit"];

        $this->waitTimeOutInMilliSeconds = $config["waitTimeOutInMilliSeconds"] ?? 0;
        if ($this->rateLimitType === "sliding_window" && $this->waitTimeOutInMilliSeconds != 0) {
            throw new InvalidArgumentException("Sliding Window RateLimiter cannot reserve tokens");
        }
    }

    public function getRateLimitType(): string
    {
        return $this->rateLimitType;
    }

    public function setRateLimitType(string $rateLimitType): RateLimitConfigurationOnRequests
    {
        $this->rateLimitType = $rateLimitType;
        return $this;
    }

    public function getRateLimitToken(): int
    {
        return $this->rateLimitToken;
    }

    public function setRateLimitToken(int $rateLimitToken): RateLimitConfigurationOnRequests
    {
        $this->rateLimitToken = $rateLimitToken;
        return $this;
    }

    public function getRateLimitTokenLimit(): int
    {
        return $this->rateLimitTokenLimit;
    }

    public function setRateLimitTokenLimit(int $rateLimitTokenLimit): RateLimitConfigurationOnRequests
    {
        $this->rateLimitTokenLimit = $rateLimitTokenLimit;
        return $this;
    }

    public function getTimeOut(): float
    {
        return $this->waitTimeOutInMilliSeconds;
    }

    public function setTimeOut(float $waitTimeOutInMilliSeconds): RateLimitConfigurationOnRequests
    {
        $this->waitTimeOutInMilliSeconds = $waitTimeOutInMilliSeconds;
        return $this;
    }
}
