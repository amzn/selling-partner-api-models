<?php

declare(strict_types=1);

use Aws\Credentials\Credentials;
use Aws\Signature\SignatureV4;
use Aws\Sts\StsClient;
use GuzzleHttp\Client;
use Application\ApiClientConfiguration;
use Application\Client\AuthClient;
use Application\Client\OrderClient;
use Application\GazzelHttpApiTransport;
use Application\RequestSignature;

$loader = require __DIR__ . '/vendor/autoload.php';

// In your application you free to get parameters for SP API the way you like.
// In this example we are using .env and loading it manually.
// For more advanced usage of .env you can simply use Symfony\Component\Dotenv.
// Run "composer require symfony/dotenv" and uncomment three lines of code below

//$dot = new Dotenv();
//$dot->usePutenv();
//$dot->load(__DIR__ . '/.env');

$dotEnv = fopen(__DIR__ . '/.env', "rb");
if ($dotEnv) {
    while (($envVar = fgets($dotEnv)) !== false) {
        putenv(trim($envVar));
    }
    fclose($dotEnv);
} else {
    throw new RuntimeException('Error reading .env file!');
}

//// PREPARE REQUEST SIGNING
$awsCredentials = new Credentials(
    getenv('AWS_USER_KEY'),
    getenv('AWS_USER_SECRET')
);

$awsSTSClient = new StsClient([
    'region' => getenv('AWS_REGION'),
    'version' => getenv('AWS_STS_VERSION'),
    'credentials' => $awsCredentials
]);

$awsSignatureV4 = new SignatureV4(
    'execute-api',
    getenv('AWS_REGION')
);

$requestSignature = new RequestSignature(
    $awsSTSClient,
    $awsSignatureV4,
    getenv('AWS_ROLE_ARM')
);

// SET UP YOR API CLIENTS AND DO CALLS
$apiTransport = new GazzelHttpApiTransport(new Client());

//// SET UP AuthClient
$apiClientConfiguration = new ApiClientConfiguration(
    getenv('AUTH_API_HOST')
);
$apiClientConfiguration
    ->setAppClientId(getenv('SP_API_APP_CLIENT_ID'))
    ->setAppClientSecret(getenv('SP_API_APP_CLIENT_SECRET'));

$authClient = new AuthClient(
    $apiTransport,
    $requestSignature,
    $apiClientConfiguration,
);

// GET ACCESS_TOKEN
$oauthResponse = $authClient->refreshAccessToken(getenv('REFRESH_TOKEN'));

$oauthResponseData = json_decode($oauthResponse, true, 512, JSON_THROW_ON_ERROR);

//// SET UP OrderClient
$orderClient = new OrderClient(
    $apiTransport,
    $requestSignature,
    new ApiClientConfiguration(
        getenv('BASE_API_HOST')
    ),
    $oauthResponseData['access_token']
);

// CALL ORDERS API
$ordersResponse = $orderClient->getOrders([getenv('MARKETPLACE_ID')], date("Y-m-d", strtotime( '-1 days' )));

$ordersResponseData = json_decode($ordersResponse, true, 512, JSON_THROW_ON_ERROR);

var_dump($ordersResponseData['payload']);