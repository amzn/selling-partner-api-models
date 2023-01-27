# Amazon.SellingPartnerAPIAA.Client.Api.ApprovalsApi

All URIs are relative to *https://sellingpartnerapi-na.amazon.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**GetOrderItemsApprovals**](ApprovalsApi.md#getorderitemsapprovals) | **GET** /orders/v0/orders/{orderId}/approvals | 
[**UpdateOrderItemsApprovals**](ApprovalsApi.md#updateorderitemsapprovals) | **POST** /orders/v0/orders/{orderId}/approvals | 


<a name="getorderitemsapprovals"></a>
# **GetOrderItemsApprovals**
> GetOrderApprovalsResponse GetOrderItemsApprovals (string orderId, string nextToken = null, List<ItemApprovalType> itemApprovalTypes = null, List<ItemApprovalStatus> itemApprovalStatus = null)



Returns detailed order items approvals information for the order specified. If NextToken is provided, it's used to retrieve the next page of order items approvals.  **Usage Plans:**  | Plan type | Rate (requests per second) | Burst | | - -- - | - -- - | - -- - | |Default| 0.5 | 30 | |Selling partner specific| Variable | Variable |  The x-amzn-RateLimit-Limit response header returns the usage plan rate limits that were applied to the requested operation. Rate limits for some selling partners will vary from the default rate and burst shown in the table above. For more information, see \"Usage Plans and Rate Limits\" in the Selling Partner API documentation.

### Example
```csharp
using System;
using System.Diagnostics;
using Amazon.SellingPartnerAPIAA.Client.Api;
using Amazon.SellingPartnerAPIAA.Client.Client;
using Amazon.SellingPartnerAPIAA.Client.Model;

namespace Example
{
    public class GetOrderItemsApprovalsExample
    {
        public void main()
        {
            var apiInstance = new ApprovalsApi();
            var orderId = orderId_example;  // string | An Amazon-defined order identifier, in 3-7-7 format, e.g. 933-1671587-0818628.
            var nextToken = nextToken_example;  // string | A string token returned in the response of your previous request. (optional) 
            var itemApprovalTypes = new List<ItemApprovalType>(); // List<ItemApprovalType> | When set, only return approvals for items which approval type is contained in the specified approval types. (optional) 
            var itemApprovalStatus = new List<ItemApprovalStatus>(); // List<ItemApprovalStatus> | When set, only return approvals that contain items which approval status is contained in the specified approval status. (optional) 

            try
            {
                GetOrderApprovalsResponse result = apiInstance.GetOrderItemsApprovals(orderId, nextToken, itemApprovalTypes, itemApprovalStatus);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ApprovalsApi.GetOrderItemsApprovals: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **orderId** | **string**| An Amazon-defined order identifier, in 3-7-7 format, e.g. 933-1671587-0818628. | 
 **nextToken** | **string**| A string token returned in the response of your previous request. | [optional] 
 **itemApprovalTypes** | [**List&lt;ItemApprovalType&gt;**](ItemApprovalType.md)| When set, only return approvals for items which approval type is contained in the specified approval types. | [optional] 
 **itemApprovalStatus** | [**List&lt;ItemApprovalStatus&gt;**](ItemApprovalStatus.md)| When set, only return approvals that contain items which approval status is contained in the specified approval status. | [optional] 

### Return type

[**GetOrderApprovalsResponse**](GetOrderApprovalsResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="updateorderitemsapprovals"></a>
# **UpdateOrderItemsApprovals**
> void UpdateOrderItemsApprovals (string orderId, UpdateOrderApprovalsRequest payload)



Update the order items approvals for an order that you specify.  **Usage Plan:**  | Rate (requests per second) | Burst | | - -- - | - -- - | | 5 | 15 |  The `x-amzn-RateLimit-Limit` response header returns the usage plan rate limits that were applied to the requested operation, when available. The table above indicates the default rate and burst values for this operation. Selling partners whose business demands require higher throughput may see higher rate and burst values then those shown here. For more information, see [Usage Plans and Rate Limits in the Selling Partner API](doc:usage-plans-and-rate-limits-in-the-sp-api).

### Example
```csharp
using System;
using System.Diagnostics;
using Amazon.SellingPartnerAPIAA.Client.Api;
using Amazon.SellingPartnerAPIAA.Client.Client;
using Amazon.SellingPartnerAPIAA.Client.Model;

namespace Example
{
    public class UpdateOrderItemsApprovalsExample
    {
        public void main()
        {
            var apiInstance = new ApprovalsApi();
            var orderId = orderId_example;  // string | An Amazon-defined order identifier, in 3-7-7 format.
            var payload = new UpdateOrderApprovalsRequest(); // UpdateOrderApprovalsRequest | The request body for the updateOrderItemsApprovals operation.

            try
            {
                apiInstance.UpdateOrderItemsApprovals(orderId, payload);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ApprovalsApi.UpdateOrderItemsApprovals: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **orderId** | **string**| An Amazon-defined order identifier, in 3-7-7 format. | 
 **payload** | [**UpdateOrderApprovalsRequest**](UpdateOrderApprovalsRequest.md)| The request body for the updateOrderItemsApprovals operation. | 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

