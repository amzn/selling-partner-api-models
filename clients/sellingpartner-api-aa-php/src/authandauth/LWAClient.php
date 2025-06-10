<?php

namespace SpApi\AuthAndAuth;

use GuzzleHttp\Client;
use GuzzleHttp\Psr7\Request;
use GuzzleHttp\Exception\BadResponseException;
use GuzzleHttp\Exception\GuzzleException;
use RuntimeException;
use Exception;

class LWAClient
{
    private Client $client;

    private string $endpoint;

    private ?LWAAccessTokenCache $lwaAccessTokenCache = null;

    public function __construct(string $endpoint)
    {
        $this->client = new Client();
        $this->endpoint = $endpoint;
    }

    public function setLWAAccessTokenCache(?LWAAccessTokenCache $tokenCache): void
    {
        $this->lwaAccessTokenCache = $tokenCache;
    }

    public function getAccessToken(LWAAccessTokenRequestMeta &$lwaAccessTokenRequestMeta): string
    {
        if ($this->lwaAccessTokenCache !== null) {
            return $this->getAccessTokenFromCache($lwaAccessTokenRequestMeta);
        } else {
            return $this->getAccessTokenFromEndpoint($lwaAccessTokenRequestMeta);
        }
    }

    public function getAccessTokenFromCache(LWAAccessTokenRequestMeta &$lwaAccessTokenRequestMeta)
    {
        $requestBody = json_encode($lwaAccessTokenRequestMeta);
        if (!$requestBody) {
            throw new RuntimeException("Request body could not be encoded");
        }
        $accessTokenCacheData = $this->lwaAccessTokenCache->get($requestBody);
        if ($accessTokenCacheData !== null) {
            return $accessTokenCacheData;
        } else {
            return $this->getAccessTokenFromEndpoint($lwaAccessTokenRequestMeta);
        }
    }

    public function getAccessTokenFromEndpoint(LWAAccessTokenRequestMeta &$lwaAccessTokenRequestMeta)
    {
        $requestBody = json_encode($lwaAccessTokenRequestMeta);

        if (!$requestBody) {
            throw new RuntimeException("Request body could not be encoded");
        }

        $contentHeader = [
            "Content-Type" => "application/json",
        ];

        try {
            $lwaRequest = new Request("POST", $this->endpoint, $contentHeader, $requestBody);

            $lwaResponse = $this->client->send($lwaRequest);
            $responseJson = json_decode($lwaResponse->getBody(), true);

            if (!$responseJson["access_token"] || !$responseJson["expires_in"]) {
                throw new RuntimeException("Response did not have required body");
            }

            $accessToken = $responseJson["access_token"];

            if ($this->lwaAccessTokenCache !== null) {
                $timeToTokenExpire = (float)$responseJson["expires_in"];
                $this->lwaAccessTokenCache->set($requestBody, $accessToken, $timeToTokenExpire);
            }
        } catch (BadResponseException $e) {
            //Catches 400 and 500 level error codes
            throw new RuntimeException("Unsuccessful LWA token exchange", $e->getCode());
        } catch (Exception $e) {
            throw new RuntimeException("Error getting LWA Access Token", $e->getCode());
        } catch (GuzzleException $e) {
            throw new RuntimeException("Error getting LWA Access Token", $e->getCode());
        }

        return $accessToken;
    }

    public function setClient(Client $client): void
    {
        $this->client = $client;
    }

    public function getEndpoint(): string
    {
        return $this->endpoint;
    }
}
