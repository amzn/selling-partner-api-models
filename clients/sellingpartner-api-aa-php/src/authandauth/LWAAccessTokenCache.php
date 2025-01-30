<?php

namespace SpApi\AuthAndAuth;

use Aws\CacheInterface;

class LWAAccessTokenCache implements CacheInterface
{
    private const EXPIRATION_BUFFER = 60 * 1000;
    private const SECOND_TO_MILLIS = 1000;
    private const ACCESS_TOKEN = "accessToken";
    private const EXPIRED_TIME = "accessTokenExpiredTime";
    private array $tokenStorage = [];

    public function set($key, $value, $ttl = 0)
    {
        $currTimeMillis = floor(microtime(true) * 1000);
        $accessTokenExpiredTimeMillis = $currTimeMillis + ($ttl * static::SECOND_TO_MILLIS);
        $accessTokenCacheItem = [
            static::ACCESS_TOKEN => $value,
            static::EXPIRED_TIME => $accessTokenExpiredTimeMillis
        ];
        $this->tokenStorage[$key] = $accessTokenCacheItem;
    }

    public function get($key): ?string
    {
        if (array_key_exists($key, $this->tokenStorage)) {
            $accessTokenValue = $this->tokenStorage[$key];
            $accessToken = $accessTokenValue[static::ACCESS_TOKEN];
            $accessTokenExpiredTimeMillis = $accessTokenValue[static::EXPIRED_TIME];
            $currTimeMillis = floor(microtime(true) * 1000);
            $accessTokenExpiredWindow = $accessTokenExpiredTimeMillis - static::EXPIRATION_BUFFER;
            if ($currTimeMillis < $accessTokenExpiredWindow) {
                return $accessToken;
            }
        }
        return null;
    }

    public function remove($key)
    {
        unset($this->tokenStorage[$key]);
    }
}
