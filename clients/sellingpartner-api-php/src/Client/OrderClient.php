<?php

declare(strict_types=1);

namespace Application\Client;

use Application\ApiClientConfiguration;
use Application\ApiTransportInterface;
use Application\RequestSignature;

class OrderClient extends AbstractClient
{
    private string $accessToken;

    public function __construct(
        ApiTransportInterface $apiTransport,
        RequestSignature $requestSignature,
        ApiClientConfiguration $config,
        string $accessToken
    )
    {
        parent::__construct($apiTransport, $requestSignature, $config);

        $this->accessToken = $accessToken;
    }

    public function getOrders(array $marketplaceIds, string $createdAfter): string
    {
        return $this->send(
            'GET',
            '/orders/v0/orders',
            [
                'MarketplaceIds' => implode(',', $marketplaceIds),
                'CreatedAfter' => $createdAfter
            ],
            ["x-amz-access-token" => $this->accessToken]
        );
    }
}
