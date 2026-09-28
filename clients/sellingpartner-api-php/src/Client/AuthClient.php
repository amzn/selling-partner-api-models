<?php

declare(strict_types=1);

namespace Application\Client;

class AuthClient extends AbstractClient
{
    /**
     * @see Request a LWA access token https://bit.ly/3lciGKD
     */
    public function refreshAccessToken(string $refreshToken): string
    {
        return $this->send(
            'POST',
            '/auth/o2/token',
            null,
            [
                'Content-Type' => 'application/x-www-form-urlencoded'
            ],
            null,
            '1.1',
            [
                'form_params' => [
                    'grant_type' => 'refresh_token',
                    'refresh_token' => $refreshToken,
                    'client_id' => $this->config->getAppClientId(),
                    'client_secret' => $this->config->getAppClientSecret(),
                ]
            ]
        );
    }

    /**
     * @see Exchange the LWA authorization code for an LWA refresh token https://bit.ly/2JiRA7o
     */
    public function exchangesAuthorizationCodeForRefreshToken(string $authorizationCode): string
    {
        return $this->send(
            'POST',
            '/auth/o2/token',
            null,
            [
                'Content-Type' => 'application/x-www-form-urlencoded'
            ],
            null,
            '1.1',
            [
                'form_params' => [
                    'grant_type' => 'authorization_code',
                    'code' => $authorizationCode,
                    'client_id' => $this->config->getAppClientId(),
                    'client_secret' => $this->config->getAppClientSecret(),
                ]
            ]
        );
    }
}
