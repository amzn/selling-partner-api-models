<?php

namespace SpApi\AuthAndAuth;

use GuzzleHttp\Psr7\Request;

class RestrictedDataTokenSigner
{
    /**
     * Sign the request with a Restricted Data Token
     *
     * @param Request $request Request to sign
     * @param string $restrictedDataToken The Restricted Data Token
     * @param string $operationName The operation name in format 'ClassName-operationId'
     * @return Request Signed request
     */
    public static function sign(Request $request, string $restrictedDataToken, string $operationName): Request
    {
            $isRestricted = self::isRestrictedOperation($operationName);
            if (!$isRestricted) {
                throw new \InvalidArgumentException(
                    "Operation '{$operationName}' does not require a Restricted Data Token (RDT). " .
                    "Remove the RDT parameter for non-restricted operations."
                );
            }

        return $request->withHeader(LWAAuthorizationSigner::SIGNED_ACCESS_TOKEN_HEADER_NAME, $restrictedDataToken);
    }

    /**
     * Check if an operation requires RDT access
     *
     * @param string $operationName The operation name in format 'ClassName-operationId'
     * @return bool True if the operation requires RDT, false otherwise
     */
    public static function isRestrictedOperation(string $operationName): bool
    {
        // List of operations that require RDT access
        $restrictedOperations = [
            // Direct Fulfillment Orders API
            'VendorOrdersApi-getOrders',
            'VendorOrdersApi-getOrder',

            // Direct Fulfillment Shipping API
            'VendorShippingLabelsApi-getShippingLabel',
            'VendorShippingLabelsApi-getShippingLabels',
            'VendorShippingApi-getPackingSlips',
            'VendorShippingApi-getPackingSlip',
            'VendorShippingLabelsApi-getCustomerInvoice',
            'VendorShippingLabelsApi-getCustomerInvoices',
            'VendorShippingLabelsApi-createShippingLabels',

            // Easy Ship API v2022-03-23
            'EasyShipApi-createScheduledPackageBulk',

            // Orders API
            'OrdersV0Api-getOrders',
            'OrdersV0Api-getOrder',
            'OrdersV0Api-getOrderBuyerInfo',
            'OrdersV0Api-getOrderAddress',
            'OrdersV0Api-getOrderItems',
            'OrdersV0Api-getOrderItemsBuyerInfo',
            'OrdersV0Api-getOrderRegulatedInfo',

            // Merchant Fulfillment API
            'MerchantFulfillmentApi-getShipment',
            'MerchantFulfillmentApi-cancelShipment',
            'MerchantFulfillmentApi-createShipment',

            // Shipment Invoicing
            'ShipmentInvoiceApi-getShipmentDetails',

            // Reports API
            'ReportsApi-getReportDocument'
        ];

        return in_array($operationName, $restrictedOperations);
    }
}


