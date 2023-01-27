# Amazon.SellingPartnerAPIAA.Client.Api.OrdersV0Api

All URIs are relative to *https://sellingpartnerapi-na.amazon.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**ConfirmShipment**](OrdersV0Api.md#confirmshipment) | **POST** /orders/v0/orders/{orderId}/shipmentConfirmation | 
[**GetOrder**](OrdersV0Api.md#getorder) | **GET** /orders/v0/orders/{orderId} | 
[**GetOrderAddress**](OrdersV0Api.md#getorderaddress) | **GET** /orders/v0/orders/{orderId}/address | 
[**GetOrderBuyerInfo**](OrdersV0Api.md#getorderbuyerinfo) | **GET** /orders/v0/orders/{orderId}/buyerInfo | 
[**GetOrderItems**](OrdersV0Api.md#getorderitems) | **GET** /orders/v0/orders/{orderId}/orderItems | 
[**GetOrderItemsBuyerInfo**](OrdersV0Api.md#getorderitemsbuyerinfo) | **GET** /orders/v0/orders/{orderId}/orderItems/buyerInfo | 
[**GetOrderRegulatedInfo**](OrdersV0Api.md#getorderregulatedinfo) | **GET** /orders/v0/orders/{orderId}/regulatedInfo | 
[**GetOrders**](OrdersV0Api.md#getorders) | **GET** /orders/v0/orders | 
[**UpdateVerificationStatus**](OrdersV0Api.md#updateverificationstatus) | **PATCH** /orders/v0/orders/{orderId}/regulatedInfo | 


<a name="confirmshipment"></a>
# **ConfirmShipment**
> void ConfirmShipment (string orderId, ConfirmShipmentRequest payload)



Updates the shipment confirmation status for a specified order.  **Usage Plan:**  | Rate (requests per second) | Burst | | - -- - | - -- - | | 2 | 10 |  The `x-amzn-RateLimit-Limit` response header returns the usage plan rate limits that were applied to the requested operation, when available. The table above indicates the default rate and burst values for this operation. Selling partners whose business demands require higher throughput may see higher rate and burst values then those shown here. For more information, see [Usage Plans and Rate Limits in the Selling Partner API](doc:usage-plans-and-rate-limits-in-the-sp-api).

### Example
```csharp
using System;
using System.Diagnostics;
using Amazon.SellingPartnerAPIAA.Client.Api;
using Amazon.SellingPartnerAPIAA.Client.Client;
using Amazon.SellingPartnerAPIAA.Client.Model;

namespace Example
{
    public class ConfirmShipmentExample
    {
        public void main()
        {
            var apiInstance = new OrdersV0Api();
            var orderId = orderId_example;  // string | An Amazon-defined order identifier, in 3-7-7 format.
            var payload = new ConfirmShipmentRequest(); // ConfirmShipmentRequest | Request body of confirmShipment.

            try
            {
                apiInstance.ConfirmShipment(orderId, payload);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling OrdersV0Api.ConfirmShipment: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **orderId** | **string**| An Amazon-defined order identifier, in 3-7-7 format. | 
 **payload** | [**ConfirmShipmentRequest**](ConfirmShipmentRequest.md)| Request body of confirmShipment. | 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getorder"></a>
# **GetOrder**
> GetOrderResponse GetOrder (string orderId)



Returns the order that you specify.  **Usage Plan:**  | Rate (requests per second) | Burst | | - -- - | - -- - | | 0.0167 | 20 |  The `x-amzn-RateLimit-Limit` response header returns the usage plan rate limits that were applied to the requested operation, when available. The table above indicates the default rate and burst values for this operation. Selling partners whose business demands require higher throughput may see higher rate and burst values then those shown here. For more information, see [Usage Plans and Rate Limits in the Selling Partner API](doc:usage-plans-and-rate-limits-in-the-sp-api).

### Example
```csharp
using System;
using System.Diagnostics;
using Amazon.SellingPartnerAPIAA.Client.Api;
using Amazon.SellingPartnerAPIAA.Client.Client;
using Amazon.SellingPartnerAPIAA.Client.Model;

namespace Example
{
    public class GetOrderExample
    {
        public void main()
        {
            var apiInstance = new OrdersV0Api();
            var orderId = orderId_example;  // string | An Amazon-defined order identifier, in 3-7-7 format.

            try
            {
                GetOrderResponse result = apiInstance.GetOrder(orderId);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling OrdersV0Api.GetOrder: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **orderId** | **string**| An Amazon-defined order identifier, in 3-7-7 format. | 

### Return type

[**GetOrderResponse**](GetOrderResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getorderaddress"></a>
# **GetOrderAddress**
> GetOrderAddressResponse GetOrderAddress (string orderId)



Returns the shipping address for the order that you specify.  **Usage Plan:**  | Rate (requests per second) | Burst | | - -- - | - -- - | | 0.0167 | 20 |  The `x-amzn-RateLimit-Limit` response header returns the usage plan rate limits that were applied to the requested operation, when available. The table above indicates the default rate and burst values for this operation. Selling partners whose business demands require higher throughput may see higher rate and burst values then those shown here. For more information, see [Usage Plans and Rate Limits in the Selling Partner API](doc:usage-plans-and-rate-limits-in-the-sp-api).

### Example
```csharp
using System;
using System.Diagnostics;
using Amazon.SellingPartnerAPIAA.Client.Api;
using Amazon.SellingPartnerAPIAA.Client.Client;
using Amazon.SellingPartnerAPIAA.Client.Model;

namespace Example
{
    public class GetOrderAddressExample
    {
        public void main()
        {
            var apiInstance = new OrdersV0Api();
            var orderId = orderId_example;  // string | An orderId is an Amazon-defined order identifier, in 3-7-7 format.

            try
            {
                GetOrderAddressResponse result = apiInstance.GetOrderAddress(orderId);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling OrdersV0Api.GetOrderAddress: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **orderId** | **string**| An orderId is an Amazon-defined order identifier, in 3-7-7 format. | 

### Return type

[**GetOrderAddressResponse**](GetOrderAddressResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getorderbuyerinfo"></a>
# **GetOrderBuyerInfo**
> GetOrderBuyerInfoResponse GetOrderBuyerInfo (string orderId)



Returns buyer information for the order that you specify.  **Usage Plan:**  | Rate (requests per second) | Burst | | - -- - | - -- - | | 0.0167 | 20 |  The `x-amzn-RateLimit-Limit` response header returns the usage plan rate limits that were applied to the requested operation, when available. The table above indicates the default rate and burst values for this operation. Selling partners whose business demands require higher throughput may see higher rate and burst values then those shown here. For more information, see [Usage Plans and Rate Limits in the Selling Partner API](doc:usage-plans-and-rate-limits-in-the-sp-api).

### Example
```csharp
using System;
using System.Diagnostics;
using Amazon.SellingPartnerAPIAA.Client.Api;
using Amazon.SellingPartnerAPIAA.Client.Client;
using Amazon.SellingPartnerAPIAA.Client.Model;

namespace Example
{
    public class GetOrderBuyerInfoExample
    {
        public void main()
        {
            var apiInstance = new OrdersV0Api();
            var orderId = orderId_example;  // string | An orderId is an Amazon-defined order identifier, in 3-7-7 format.

            try
            {
                GetOrderBuyerInfoResponse result = apiInstance.GetOrderBuyerInfo(orderId);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling OrdersV0Api.GetOrderBuyerInfo: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **orderId** | **string**| An orderId is an Amazon-defined order identifier, in 3-7-7 format. | 

### Return type

[**GetOrderBuyerInfoResponse**](GetOrderBuyerInfoResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getorderitems"></a>
# **GetOrderItems**
> GetOrderItemsResponse GetOrderItems (string orderId, string nextToken = null)



Returns detailed order item information for the order that you specify. If NextToken is provided, it's used to retrieve the next page of order items.  __Note__: When an order is in the Pending state (the order has been placed but payment has not been authorized), the getOrderItems operation does not return information about pricing, taxes, shipping charges, gift status or promotions for the order items in the order. After an order leaves the Pending state (this occurs when payment has been authorized) and enters the Unshipped, Partially Shipped, or Shipped state, the getOrderItems operation returns information about pricing, taxes, shipping charges, gift status and promotions for the order items in the order.  **Usage Plan:**  | Rate (requests per second) | Burst | | - -- - | - -- - | | 0.5 | 30 |  The `x-amzn-RateLimit-Limit` response header returns the usage plan rate limits that were applied to the requested operation, when available. The table above indicates the default rate and burst values for this operation. Selling partners whose business demands require higher throughput may see higher rate and burst values then those shown here. For more information, see [Usage Plans and Rate Limits in the Selling Partner API](doc:usage-plans-and-rate-limits-in-the-sp-api).

### Example
```csharp
using System;
using System.Diagnostics;
using Amazon.SellingPartnerAPIAA.Client.Api;
using Amazon.SellingPartnerAPIAA.Client.Client;
using Amazon.SellingPartnerAPIAA.Client.Model;

namespace Example
{
    public class GetOrderItemsExample
    {
        public void main()
        {
            var apiInstance = new OrdersV0Api();
            var orderId = orderId_example;  // string | An Amazon-defined order identifier, in 3-7-7 format.
            var nextToken = nextToken_example;  // string | A string token returned in the response of your previous request. (optional) 

            try
            {
                GetOrderItemsResponse result = apiInstance.GetOrderItems(orderId, nextToken);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling OrdersV0Api.GetOrderItems: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **orderId** | **string**| An Amazon-defined order identifier, in 3-7-7 format. | 
 **nextToken** | **string**| A string token returned in the response of your previous request. | [optional] 

### Return type

[**GetOrderItemsResponse**](GetOrderItemsResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getorderitemsbuyerinfo"></a>
# **GetOrderItemsBuyerInfo**
> GetOrderItemsBuyerInfoResponse GetOrderItemsBuyerInfo (string orderId, string nextToken = null)



Returns buyer information for the order items in the order that you specify.  **Usage Plan:**  | Rate (requests per second) | Burst | | - -- - | - -- - | | 0.5 | 30 |  The `x-amzn-RateLimit-Limit` response header returns the usage plan rate limits that were applied to the requested operation, when available. The table above indicates the default rate and burst values for this operation. Selling partners whose business demands require higher throughput may see higher rate and burst values then those shown here. For more information, see [Usage Plans and Rate Limits in the Selling Partner API](doc:usage-plans-and-rate-limits-in-the-sp-api).

### Example
```csharp
using System;
using System.Diagnostics;
using Amazon.SellingPartnerAPIAA.Client.Api;
using Amazon.SellingPartnerAPIAA.Client.Client;
using Amazon.SellingPartnerAPIAA.Client.Model;

namespace Example
{
    public class GetOrderItemsBuyerInfoExample
    {
        public void main()
        {
            var apiInstance = new OrdersV0Api();
            var orderId = orderId_example;  // string | An Amazon-defined order identifier, in 3-7-7 format.
            var nextToken = nextToken_example;  // string | A string token returned in the response of your previous request. (optional) 

            try
            {
                GetOrderItemsBuyerInfoResponse result = apiInstance.GetOrderItemsBuyerInfo(orderId, nextToken);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling OrdersV0Api.GetOrderItemsBuyerInfo: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **orderId** | **string**| An Amazon-defined order identifier, in 3-7-7 format. | 
 **nextToken** | **string**| A string token returned in the response of your previous request. | [optional] 

### Return type

[**GetOrderItemsBuyerInfoResponse**](GetOrderItemsBuyerInfoResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getorderregulatedinfo"></a>
# **GetOrderRegulatedInfo**
> GetOrderRegulatedInfoResponse GetOrderRegulatedInfo (string orderId)



Returns regulated information for the order that you specify.  **Usage Plan:**  | Rate (requests per second) | Burst | | - -- - | - -- - | | 0.5 | 30 |  The `x-amzn-RateLimit-Limit` response header returns the usage plan rate limits that were applied to the requested operation, when available. The table above indicates the default rate and burst values for this operation. Selling partners whose business demands require higher throughput may see higher rate and burst values then those shown here. For more information, see [Usage Plans and Rate Limits in the Selling Partner API](doc:usage-plans-and-rate-limits-in-the-sp-api).

### Example
```csharp
using System;
using System.Diagnostics;
using Amazon.SellingPartnerAPIAA.Client.Api;
using Amazon.SellingPartnerAPIAA.Client.Client;
using Amazon.SellingPartnerAPIAA.Client.Model;

namespace Example
{
    public class GetOrderRegulatedInfoExample
    {
        public void main()
        {
            var apiInstance = new OrdersV0Api();
            var orderId = orderId_example;  // string | An orderId is an Amazon-defined order identifier, in 3-7-7 format.

            try
            {
                GetOrderRegulatedInfoResponse result = apiInstance.GetOrderRegulatedInfo(orderId);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling OrdersV0Api.GetOrderRegulatedInfo: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **orderId** | **string**| An orderId is an Amazon-defined order identifier, in 3-7-7 format. | 

### Return type

[**GetOrderRegulatedInfoResponse**](GetOrderRegulatedInfoResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getorders"></a>
# **GetOrders**
> GetOrdersResponse GetOrders (List<string> marketplaceIds, string createdAfter = null, string createdBefore = null, string lastUpdatedAfter = null, string lastUpdatedBefore = null, List<string> orderStatuses = null, List<string> fulfillmentChannels = null, List<string> paymentMethods = null, string buyerEmail = null, string sellerOrderId = null, int? maxResultsPerPage = null, List<string> easyShipShipmentStatuses = null, List<string> electronicInvoiceStatuses = null, string nextToken = null, List<string> amazonOrderIds = null, string actualFulfillmentSupplySourceId = null, bool? isISPU = null, string storeChainStoreId = null, List<ItemApprovalType> itemApprovalTypes = null, List<ItemApprovalStatus> itemApprovalStatus = null)



Returns orders created or updated during the time frame indicated by the specified parameters. You can also apply a range of filtering criteria to narrow the list of orders returned. If NextToken is present, that will be used to retrieve the orders instead of other criteria.  **Usage Plan:**  | Rate (requests per second) | Burst | | - -- - | - -- - | | 0.0167 | 20 |  The `x-amzn-RateLimit-Limit` response header returns the usage plan rate limits that were applied to the requested operation, when available. The table above indicates the default rate and burst values for this operation. Selling partners whose business demands require higher throughput may see higher rate and burst values then those shown here. For more information, see [Usage Plans and Rate Limits in the Selling Partner API](doc:usage-plans-and-rate-limits-in-the-sp-api).

### Example
```csharp
using System;
using System.Diagnostics;
using Amazon.SellingPartnerAPIAA.Client.Api;
using Amazon.SellingPartnerAPIAA.Client.Client;
using Amazon.SellingPartnerAPIAA.Client.Model;

namespace Example
{
    public class GetOrdersExample
    {
        public void main()
        {
            var apiInstance = new OrdersV0Api();
            var marketplaceIds = new List<string>(); // List<string> | A list of MarketplaceId values. Used to select orders that were placed in the specified marketplaces.  See the [Selling Partner API Developer Guide](doc:marketplace-ids) for a complete list of marketplaceId values.
            var createdAfter = createdAfter_example;  // string | A date used for selecting orders created after (or at) a specified time. Only orders placed after the specified time are returned. Either the CreatedAfter parameter or the LastUpdatedAfter parameter is required. Both cannot be empty. The date must be in ISO 8601 format. (optional) 
            var createdBefore = createdBefore_example;  // string | A date used for selecting orders created before (or at) a specified time. Only orders placed before the specified time are returned. The date must be in ISO 8601 format. (optional) 
            var lastUpdatedAfter = lastUpdatedAfter_example;  // string | A date used for selecting orders that were last updated after (or at) a specified time. An update is defined as any change in order status, including the creation of a new order. Includes updates made by Amazon and by the seller. The date must be in ISO 8601 format. (optional) 
            var lastUpdatedBefore = lastUpdatedBefore_example;  // string | A date used for selecting orders that were last updated before (or at) a specified time. An update is defined as any change in order status, including the creation of a new order. Includes updates made by Amazon and by the seller. The date must be in ISO 8601 format. (optional) 
            var orderStatuses = new List<string>(); // List<string> | A list of `OrderStatus` values used to filter the results.  **Possible values:** - `PendingAvailability` (This status is available for pre-orders only. The order has been placed, payment has not been authorized, and the release date of the item is in the future.) - `Pending` (The order has been placed but payment has not been authorized.) - `Unshipped` (Payment has been authorized and the order is ready for shipment, but no items in the order have been shipped.) - `PartiallyShipped` (One or more, but not all, items in the order have been shipped.) - `Shipped` (All items in the order have been shipped.) - `InvoiceUnconfirmed` (All items in the order have been shipped. The seller has not yet given confirmation to Amazon that the invoice has been shipped to the buyer.) - `Canceled` (The order has been canceled.) - `Unfulfillable` (The order cannot be fulfilled. This state applies only to Multi-Channel Fulfillment orders.) (optional) 
            var fulfillmentChannels = new List<string>(); // List<string> | A list that indicates how an order was fulfilled. Filters the results by fulfillment channel. Possible values: AFN (Fulfillment by Amazon); MFN (Fulfilled by the seller). (optional) 
            var paymentMethods = new List<string>(); // List<string> | A list of payment method values. Used to select orders paid using the specified payment methods. Possible values: COD (Cash on delivery); CVS (Convenience store payment); Other (Any payment method other than COD or CVS). (optional) 
            var buyerEmail = buyerEmail_example;  // string | The email address of a buyer. Used to select orders that contain the specified email address. (optional) 
            var sellerOrderId = sellerOrderId_example;  // string | An order identifier that is specified by the seller. Used to select only the orders that match the order identifier. If SellerOrderId is specified, then FulfillmentChannels, OrderStatuses, PaymentMethod, LastUpdatedAfter, LastUpdatedBefore, and BuyerEmail cannot be specified. (optional) 
            var maxResultsPerPage = 56;  // int? | A number that indicates the maximum number of orders that can be returned per page. Value must be 1 - 100. Default 100. (optional) 
            var easyShipShipmentStatuses = new List<string>(); // List<string> | A list of `EasyShipShipmentStatus` values. Used to select Easy Ship orders with statuses that match the specified values. If `EasyShipShipmentStatus` is specified, only Amazon Easy Ship orders are returned.  **Possible values:** - `PendingSchedule` (The package is awaiting the schedule for pick-up.) - `PendingPickUp` (Amazon has not yet picked up the package from the seller.) - `PendingDropOff` (The seller will deliver the package to the carrier.) - `LabelCanceled` (The seller canceled the pickup.) - `PickedUp` (Amazon has picked up the package from the seller.) - `DroppedOff` (The package is delivered to the carrier by the seller.) - `AtOriginFC` (The packaged is at the origin fulfillment center.) - `AtDestinationFC` (The package is at the destination fulfillment center.) - `Delivered` (The package has been delivered.) - `RejectedByBuyer` (The package has been rejected by the buyer.) - `Undeliverable` (The package cannot be delivered.) - `ReturningToSeller` (The package was not delivered and is being returned to the seller.) - `ReturnedToSeller` (The package was not delivered and was returned to the seller.) - `Lost` (The package is lost.) - `OutForDelivery` (The package is out for delivery.) - `Damaged` (The package was damaged by the carrier.) (optional) 
            var electronicInvoiceStatuses = new List<string>(); // List<string> | A list of `ElectronicInvoiceStatus` values. Used to select orders with electronic invoice statuses that match the specified values.  **Possible values:** - `NotRequired` (Electronic invoice submission is not required for this order.) - `NotFound` (The electronic invoice was not submitted for this order.) - `Processing` (The electronic invoice is being processed for this order.) - `Errored` (The last submitted electronic invoice was rejected for this order.) - `Accepted` (The last submitted electronic invoice was submitted and accepted.) (optional) 
            var nextToken = nextToken_example;  // string | A string token returned in the response of your previous request. (optional) 
            var amazonOrderIds = new List<string>(); // List<string> | A list of AmazonOrderId values. An AmazonOrderId is an Amazon-defined order identifier, in 3-7-7 format. (optional) 
            var actualFulfillmentSupplySourceId = actualFulfillmentSupplySourceId_example;  // string | Denotes the recommended sourceId where the order should be fulfilled from. (optional) 
            var isISPU = true;  // bool? | When true, this order is marked to be picked up from a store rather than delivered. (optional) 
            var storeChainStoreId = storeChainStoreId_example;  // string | The store chain store identifier. Linked to a specific store in a store chain. (optional) 
            var itemApprovalTypes = new List<ItemApprovalType>(); // List<ItemApprovalType> | When set, only return orders that contain items which approval type is contained in the specified approval types. (optional) 
            var itemApprovalStatus = new List<ItemApprovalStatus>(); // List<ItemApprovalStatus> | When set, only return orders that contain items which approval status is contained in the specified approval status. (optional) 

            try
            {
                GetOrdersResponse result = apiInstance.GetOrders(marketplaceIds, createdAfter, createdBefore, lastUpdatedAfter, lastUpdatedBefore, orderStatuses, fulfillmentChannels, paymentMethods, buyerEmail, sellerOrderId, maxResultsPerPage, easyShipShipmentStatuses, electronicInvoiceStatuses, nextToken, amazonOrderIds, actualFulfillmentSupplySourceId, isISPU, storeChainStoreId, itemApprovalTypes, itemApprovalStatus);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling OrdersV0Api.GetOrders: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **marketplaceIds** | [**List&lt;string&gt;**](string.md)| A list of MarketplaceId values. Used to select orders that were placed in the specified marketplaces.  See the [Selling Partner API Developer Guide](doc:marketplace-ids) for a complete list of marketplaceId values. | 
 **createdAfter** | **string**| A date used for selecting orders created after (or at) a specified time. Only orders placed after the specified time are returned. Either the CreatedAfter parameter or the LastUpdatedAfter parameter is required. Both cannot be empty. The date must be in ISO 8601 format. | [optional] 
 **createdBefore** | **string**| A date used for selecting orders created before (or at) a specified time. Only orders placed before the specified time are returned. The date must be in ISO 8601 format. | [optional] 
 **lastUpdatedAfter** | **string**| A date used for selecting orders that were last updated after (or at) a specified time. An update is defined as any change in order status, including the creation of a new order. Includes updates made by Amazon and by the seller. The date must be in ISO 8601 format. | [optional] 
 **lastUpdatedBefore** | **string**| A date used for selecting orders that were last updated before (or at) a specified time. An update is defined as any change in order status, including the creation of a new order. Includes updates made by Amazon and by the seller. The date must be in ISO 8601 format. | [optional] 
 **orderStatuses** | [**List&lt;string&gt;**](string.md)| A list of &#x60;OrderStatus&#x60; values used to filter the results.  **Possible values:** - &#x60;PendingAvailability&#x60; (This status is available for pre-orders only. The order has been placed, payment has not been authorized, and the release date of the item is in the future.) - &#x60;Pending&#x60; (The order has been placed but payment has not been authorized.) - &#x60;Unshipped&#x60; (Payment has been authorized and the order is ready for shipment, but no items in the order have been shipped.) - &#x60;PartiallyShipped&#x60; (One or more, but not all, items in the order have been shipped.) - &#x60;Shipped&#x60; (All items in the order have been shipped.) - &#x60;InvoiceUnconfirmed&#x60; (All items in the order have been shipped. The seller has not yet given confirmation to Amazon that the invoice has been shipped to the buyer.) - &#x60;Canceled&#x60; (The order has been canceled.) - &#x60;Unfulfillable&#x60; (The order cannot be fulfilled. This state applies only to Multi-Channel Fulfillment orders.) | [optional] 
 **fulfillmentChannels** | [**List&lt;string&gt;**](string.md)| A list that indicates how an order was fulfilled. Filters the results by fulfillment channel. Possible values: AFN (Fulfillment by Amazon); MFN (Fulfilled by the seller). | [optional] 
 **paymentMethods** | [**List&lt;string&gt;**](string.md)| A list of payment method values. Used to select orders paid using the specified payment methods. Possible values: COD (Cash on delivery); CVS (Convenience store payment); Other (Any payment method other than COD or CVS). | [optional] 
 **buyerEmail** | **string**| The email address of a buyer. Used to select orders that contain the specified email address. | [optional] 
 **sellerOrderId** | **string**| An order identifier that is specified by the seller. Used to select only the orders that match the order identifier. If SellerOrderId is specified, then FulfillmentChannels, OrderStatuses, PaymentMethod, LastUpdatedAfter, LastUpdatedBefore, and BuyerEmail cannot be specified. | [optional] 
 **maxResultsPerPage** | **int?**| A number that indicates the maximum number of orders that can be returned per page. Value must be 1 - 100. Default 100. | [optional] 
 **easyShipShipmentStatuses** | [**List&lt;string&gt;**](string.md)| A list of &#x60;EasyShipShipmentStatus&#x60; values. Used to select Easy Ship orders with statuses that match the specified values. If &#x60;EasyShipShipmentStatus&#x60; is specified, only Amazon Easy Ship orders are returned.  **Possible values:** - &#x60;PendingSchedule&#x60; (The package is awaiting the schedule for pick-up.) - &#x60;PendingPickUp&#x60; (Amazon has not yet picked up the package from the seller.) - &#x60;PendingDropOff&#x60; (The seller will deliver the package to the carrier.) - &#x60;LabelCanceled&#x60; (The seller canceled the pickup.) - &#x60;PickedUp&#x60; (Amazon has picked up the package from the seller.) - &#x60;DroppedOff&#x60; (The package is delivered to the carrier by the seller.) - &#x60;AtOriginFC&#x60; (The packaged is at the origin fulfillment center.) - &#x60;AtDestinationFC&#x60; (The package is at the destination fulfillment center.) - &#x60;Delivered&#x60; (The package has been delivered.) - &#x60;RejectedByBuyer&#x60; (The package has been rejected by the buyer.) - &#x60;Undeliverable&#x60; (The package cannot be delivered.) - &#x60;ReturningToSeller&#x60; (The package was not delivered and is being returned to the seller.) - &#x60;ReturnedToSeller&#x60; (The package was not delivered and was returned to the seller.) - &#x60;Lost&#x60; (The package is lost.) - &#x60;OutForDelivery&#x60; (The package is out for delivery.) - &#x60;Damaged&#x60; (The package was damaged by the carrier.) | [optional] 
 **electronicInvoiceStatuses** | [**List&lt;string&gt;**](string.md)| A list of &#x60;ElectronicInvoiceStatus&#x60; values. Used to select orders with electronic invoice statuses that match the specified values.  **Possible values:** - &#x60;NotRequired&#x60; (Electronic invoice submission is not required for this order.) - &#x60;NotFound&#x60; (The electronic invoice was not submitted for this order.) - &#x60;Processing&#x60; (The electronic invoice is being processed for this order.) - &#x60;Errored&#x60; (The last submitted electronic invoice was rejected for this order.) - &#x60;Accepted&#x60; (The last submitted electronic invoice was submitted and accepted.) | [optional] 
 **nextToken** | **string**| A string token returned in the response of your previous request. | [optional] 
 **amazonOrderIds** | [**List&lt;string&gt;**](string.md)| A list of AmazonOrderId values. An AmazonOrderId is an Amazon-defined order identifier, in 3-7-7 format. | [optional] 
 **actualFulfillmentSupplySourceId** | **string**| Denotes the recommended sourceId where the order should be fulfilled from. | [optional] 
 **isISPU** | **bool?**| When true, this order is marked to be picked up from a store rather than delivered. | [optional] 
 **storeChainStoreId** | **string**| The store chain store identifier. Linked to a specific store in a store chain. | [optional] 
 **itemApprovalTypes** | [**List&lt;ItemApprovalType&gt;**](ItemApprovalType.md)| When set, only return orders that contain items which approval type is contained in the specified approval types. | [optional] 
 **itemApprovalStatus** | [**List&lt;ItemApprovalStatus&gt;**](ItemApprovalStatus.md)| When set, only return orders that contain items which approval status is contained in the specified approval status. | [optional] 

### Return type

[**GetOrdersResponse**](GetOrdersResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="updateverificationstatus"></a>
# **UpdateVerificationStatus**
> void UpdateVerificationStatus (string orderId, UpdateVerificationStatusRequest payload)



Updates (approves or rejects) the verification status of an order containing regulated products.  **Usage Plan:**  | Rate (requests per second) | Burst | | - -- - | - -- - | | 0.5 | 30 |  The `x-amzn-RateLimit-Limit` response header returns the usage plan rate limits that were applied to the requested operation, when available. The table above indicates the default rate and burst values for this operation. Selling partners whose business demands require higher throughput may see higher rate and burst values then those shown here. For more information, see [Usage Plans and Rate Limits in the Selling Partner API](doc:usage-plans-and-rate-limits-in-the-sp-api).

### Example
```csharp
using System;
using System.Diagnostics;
using Amazon.SellingPartnerAPIAA.Client.Api;
using Amazon.SellingPartnerAPIAA.Client.Client;
using Amazon.SellingPartnerAPIAA.Client.Model;

namespace Example
{
    public class UpdateVerificationStatusExample
    {
        public void main()
        {
            var apiInstance = new OrdersV0Api();
            var orderId = orderId_example;  // string | An orderId is an Amazon-defined order identifier, in 3-7-7 format.
            var payload = new UpdateVerificationStatusRequest(); // UpdateVerificationStatusRequest | The request body for the updateVerificationStatus operation.

            try
            {
                apiInstance.UpdateVerificationStatus(orderId, payload);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling OrdersV0Api.UpdateVerificationStatus: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **orderId** | **string**| An orderId is an Amazon-defined order identifier, in 3-7-7 format. | 
 **payload** | [**UpdateVerificationStatusRequest**](UpdateVerificationStatusRequest.md)| The request body for the updateVerificationStatus operation. | 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

