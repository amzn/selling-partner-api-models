<?php

declare(strict_types=1);

namespace Application\Client;

use Application\ApiClientConfiguration;
use Application\ApiTransportInterface;
use Application\RequestSignature;
use GuzzleHttp\Psr7\Request;

abstract class AbstractClient
{
    protected const DEFAULT_HTTP_VERSION = '1.1';

    protected ApiTransportInterface $apiTransport;
    protected RequestSignature $requestSignature;
    protected ApiClientConfiguration $config;

    public function __construct(
        ApiTransportInterface $apiTransport,
        RequestSignature $requestSignature,
        ApiClientConfiguration $config
    )
    {
        $this->apiTransport = $apiTransport;
        $this->requestSignature = $requestSignature;
        $this->config = $config;
    }

    /**
     * Abstract send method to sign each API request
     */
    protected function send(
        $method,
        $resourcePath,
        $queryParams = null,
        $headers = [],
        $body = null,
        $version = self::DEFAULT_HTTP_VERSION,
        array $options = []
    ): string
    {
        $uri = $this->config->getHost() . $resourcePath;

        if (!empty($queryParams)) {
            $uri .= '?' . http_build_query($queryParams);
        }

        return $this->apiTransport->send(
            $this->requestSignature->signRequest(
                new Request(
                    $method,
                    $uri,
                    $headers,
                    $body,
                    $version
                )
            ),
            $options
        )->getBody()->getContents();
    }
}
