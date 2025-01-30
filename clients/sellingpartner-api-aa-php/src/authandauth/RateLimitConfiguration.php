<?php

namespace SpApi\AuthAndAuth;

interface RateLimitConfiguration
{
    public function getRateLimitType(): string;

    public function getRateLimitToken(): int;

    public function getRateLimitTokenLimit(): int;

    public function getTimeOut(): float;
}
