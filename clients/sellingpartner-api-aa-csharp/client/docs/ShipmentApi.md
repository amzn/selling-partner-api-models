# Amazon.SellingPartnerAPIAA.Clients.API.ShipmentApi

All URIs are relative to *https://sellingpartnerapi-na.amazon.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**UpdateShipmentStatus**](ShipmentApi.md#updateshipmentstatus) | **POST** /orders/v0/orders/{orderId}/shipment | 


<a name="updateshipmentstatus"></a>
# **UpdateShipmentStatus**
> void UpdateShipmentStatus (string orderId, UpdateShipmentStatusRequest payload)



Update the shipment status for an order that you specify.  **Usage Plan:**  | Rate (requests per second) | Burst | | - -- - | - -- - | | 5 | 15 |  The `x-amzn-RateLimit-Limit` response header returns the usage plan rate limits that were applied to the requested operation, when available. The table above indicates the default rate and burst values for this operation. Selling partners whose business demands require higher throughput may see higher rate and burst values then those shown here. For more information, see [Usage Plans and Rate Limits in the Selling Partner API](doc:usage-plans-and-rate-limits-in-the-sp-api).

### Example
```csharp
using System;
using System.Diagnostics;
using Amazon.SellingPartnerAPIAA.Clients.API;
using Amazon.SellingPartnerAPIAA.Clients.Client;
using Amazon.SellingPartnerAPIAA.Clients.Models.Orders;

namespace Example
{
    public class UpdateShipmentStatusExample
    {
        public void main()
        {
            var apiInstance = new ShipmentApi();
            var orderId = orderId_example;  // string | An Amazon-defined order identifier, in 3-7-7 format.
            var payload = new UpdateShipmentStatusRequest(); // UpdateShipmentStatusRequest | The request body for the updateShipmentStatus operation.

            try
            {
                apiInstance.UpdateShipmentStatus(orderId, payload);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ShipmentApi.UpdateShipmentStatus: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **orderId** | **string**| An Amazon-defined order identifier, in 3-7-7 format. | 
 **payload** | [**UpdateShipmentStatusRequest**](UpdateShipmentStatusRequest.md)| The request body for the updateShipmentStatus operation. | 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

