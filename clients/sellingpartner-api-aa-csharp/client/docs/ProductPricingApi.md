# Amazon.SellingPartnerAPIAA.Client.Api.ProductPricingApi

All URIs are relative to *https://sellingpartnerapi-na.amazon.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**GetCompetitivePricing**](ProductPricingApi.md#getcompetitivepricing) | **GET** /products/pricing/v0/competitivePrice | 
[**GetItemOffers**](ProductPricingApi.md#getitemoffers) | **GET** /products/pricing/v0/items/{Asin}/offers | 
[**GetItemOffersBatch**](ProductPricingApi.md#getitemoffersbatch) | **POST** /batches/products/pricing/v0/itemOffers | 
[**GetListingOffers**](ProductPricingApi.md#getlistingoffers) | **GET** /products/pricing/v0/listings/{SellerSKU}/offers | 
[**GetListingOffersBatch**](ProductPricingApi.md#getlistingoffersbatch) | **POST** /batches/products/pricing/v0/listingOffers | 
[**GetPricing**](ProductPricingApi.md#getpricing) | **GET** /products/pricing/v0/price | 


<a name="getcompetitivepricing"></a>
# **GetCompetitivePricing**
> GetPricingResponse GetCompetitivePricing (string marketplaceId, string itemType, List<string> asins = null, List<string> skus = null, string customerType = null)



Returns competitive pricing information for a seller's offer listings based on seller SKU or ASIN.  **Usage Plan:**  | Rate (requests per second) | Burst | | - -- - | - -- - | | 0.5 | 1 |  The `x-amzn-RateLimit-Limit` response header returns the usage plan rate limits that were applied to the requested operation, when available. The table above indicates the default rate and burst values for this operation. Selling partners whose business demands require higher throughput may see higher rate and burst values than those shown here. For more information, see [Usage Plans and Rate Limits in the Selling Partner API](doc:usage-plans-and-rate-limits-in-the-sp-api).

### Example
```csharp
using System;
using System.Diagnostics;
using Amazon.SellingPartnerAPIAA.Client.Api;
using Amazon.SellingPartnerAPIAA.Client.Client;
using Amazon.SellingPartnerAPIAA.Client.Model;

namespace Example
{
    public class GetCompetitivePricingExample
    {
        public void main()
        {
            var apiInstance = new ProductPricingApi();
            var marketplaceId = marketplaceId_example;  // string | A marketplace identifier. Specifies the marketplace for which prices are returned.
            var itemType = itemType_example;  // string | Indicates whether ASIN values or seller SKU values are used to identify items. If you specify Asin, the information in the response will be dependent on the list of Asins you provide in the Asins parameter. If you specify Sku, the information in the response will be dependent on the list of Skus you provide in the Skus parameter. Possible values: Asin, Sku.
            var asins = new List<string>(); // List<string> | A list of up to twenty Amazon Standard Identification Number (ASIN) values used to identify items in the given marketplace. (optional) 
            var skus = new List<string>(); // List<string> | A list of up to twenty seller SKU values used to identify items in the given marketplace. (optional) 
            var customerType = customerType_example;  // string | Indicates whether to request pricing information from the point of view of Consumer or Business buyers. Default is Consumer. (optional) 

            try
            {
                GetPricingResponse result = apiInstance.GetCompetitivePricing(marketplaceId, itemType, asins, skus, customerType);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ProductPricingApi.GetCompetitivePricing: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **marketplaceId** | **string**| A marketplace identifier. Specifies the marketplace for which prices are returned. | 
 **itemType** | **string**| Indicates whether ASIN values or seller SKU values are used to identify items. If you specify Asin, the information in the response will be dependent on the list of Asins you provide in the Asins parameter. If you specify Sku, the information in the response will be dependent on the list of Skus you provide in the Skus parameter. Possible values: Asin, Sku. | 
 **asins** | [**List&lt;string&gt;**](string.md)| A list of up to twenty Amazon Standard Identification Number (ASIN) values used to identify items in the given marketplace. | [optional] 
 **skus** | [**List&lt;string&gt;**](string.md)| A list of up to twenty seller SKU values used to identify items in the given marketplace. | [optional] 
 **customerType** | **string**| Indicates whether to request pricing information from the point of view of Consumer or Business buyers. Default is Consumer. | [optional] 

### Return type

[**GetPricingResponse**](GetPricingResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getitemoffers"></a>
# **GetItemOffers**
> GetOffersResponse GetItemOffers (string marketplaceId, string itemCondition, string asin, string customerType = null)



Returns the lowest priced offers for a single item based on ASIN.  **Usage Plan:**  | Rate (requests per second) | Burst | | - -- - | - -- - | | 0.5 | 1 |  The `x-amzn-RateLimit-Limit` response header returns the usage plan rate limits that were applied to the requested operation, when available. The table above indicates the default rate and burst values for this operation. Selling partners whose business demands require higher throughput may see higher rate and burst values than those shown here. For more information, see [Usage Plans and Rate Limits in the Selling Partner API](doc:usage-plans-and-rate-limits-in-the-sp-api).

### Example
```csharp
using System;
using System.Diagnostics;
using Amazon.SellingPartnerAPIAA.Client.Api;
using Amazon.SellingPartnerAPIAA.Client.Client;
using Amazon.SellingPartnerAPIAA.Client.Model;

namespace Example
{
    public class GetItemOffersExample
    {
        public void main()
        {
            var apiInstance = new ProductPricingApi();
            var marketplaceId = marketplaceId_example;  // string | A marketplace identifier. Specifies the marketplace for which prices are returned.
            var itemCondition = itemCondition_example;  // string | Filters the offer listings to be considered based on item condition. Possible values: New, Used, Collectible, Refurbished, Club.
            var asin = asin_example;  // string | The Amazon Standard Identification Number (ASIN) of the item.
            var customerType = customerType_example;  // string | Indicates whether to request Consumer or Business offers. Default is Consumer. (optional) 

            try
            {
                GetOffersResponse result = apiInstance.GetItemOffers(marketplaceId, itemCondition, asin, customerType);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ProductPricingApi.GetItemOffers: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **marketplaceId** | **string**| A marketplace identifier. Specifies the marketplace for which prices are returned. | 
 **itemCondition** | **string**| Filters the offer listings to be considered based on item condition. Possible values: New, Used, Collectible, Refurbished, Club. | 
 **asin** | **string**| The Amazon Standard Identification Number (ASIN) of the item. | 
 **customerType** | **string**| Indicates whether to request Consumer or Business offers. Default is Consumer. | [optional] 

### Return type

[**GetOffersResponse**](GetOffersResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getitemoffersbatch"></a>
# **GetItemOffersBatch**
> GetItemOffersBatchResponse GetItemOffersBatch (GetItemOffersBatchRequest getItemOffersBatchRequestBody)



Returns the lowest priced offers for a batch of items based on ASIN.  **Usage Plan:**  | Rate (requests per second) | Burst | | - -- - | - -- - | | 0.5 | 1 |  The `x-amzn-RateLimit-Limit` response header returns the usage plan rate limits that were applied to the requested operation, when available. The table above indicates the default rate and burst values for this operation. Selling partners whose business demands require higher throughput may see higher rate and burst values than those shown here. For more information, see [Usage Plans and Rate Limits in the Selling Partner API](doc:usage-plans-and-rate-limits-in-the-sp-api).

### Example
```csharp
using System;
using System.Diagnostics;
using Amazon.SellingPartnerAPIAA.Client.Api;
using Amazon.SellingPartnerAPIAA.Client.Client;
using Amazon.SellingPartnerAPIAA.Client.Model;

namespace Example
{
    public class GetItemOffersBatchExample
    {
        public void main()
        {
            var apiInstance = new ProductPricingApi();
            var getItemOffersBatchRequestBody = new GetItemOffersBatchRequest(); // GetItemOffersBatchRequest | 

            try
            {
                GetItemOffersBatchResponse result = apiInstance.GetItemOffersBatch(getItemOffersBatchRequestBody);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ProductPricingApi.GetItemOffersBatch: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **getItemOffersBatchRequestBody** | [**GetItemOffersBatchRequest**](GetItemOffersBatchRequest.md)|  | 

### Return type

[**GetItemOffersBatchResponse**](GetItemOffersBatchResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getlistingoffers"></a>
# **GetListingOffers**
> GetOffersResponse GetListingOffers (string marketplaceId, string itemCondition, string sellerSKU, string customerType = null)



Returns the lowest priced offers for a single SKU listing.  **Usage Plan:**  | Rate (requests per second) | Burst | | - -- - | - -- - | | 1 | 2 |  The `x-amzn-RateLimit-Limit` response header returns the usage plan rate limits that were applied to the requested operation, when available. The table above indicates the default rate and burst values for this operation. Selling partners whose business demands require higher throughput may see higher rate and burst values than those shown here. For more information, see [Usage Plans and Rate Limits in the Selling Partner API](doc:usage-plans-and-rate-limits-in-the-sp-api).

### Example
```csharp
using System;
using System.Diagnostics;
using Amazon.SellingPartnerAPIAA.Client.Api;
using Amazon.SellingPartnerAPIAA.Client.Client;
using Amazon.SellingPartnerAPIAA.Client.Model;

namespace Example
{
    public class GetListingOffersExample
    {
        public void main()
        {
            var apiInstance = new ProductPricingApi();
            var marketplaceId = marketplaceId_example;  // string | A marketplace identifier. Specifies the marketplace for which prices are returned.
            var itemCondition = itemCondition_example;  // string | Filters the offer listings based on item condition. Possible values: New, Used, Collectible, Refurbished, Club.
            var sellerSKU = sellerSKU_example;  // string | Identifies an item in the given marketplace. SellerSKU is qualified by the seller's SellerId, which is included with every operation that you submit.
            var customerType = customerType_example;  // string | Indicates whether to request Consumer or Business offers. Default is Consumer. (optional) 

            try
            {
                GetOffersResponse result = apiInstance.GetListingOffers(marketplaceId, itemCondition, sellerSKU, customerType);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ProductPricingApi.GetListingOffers: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **marketplaceId** | **string**| A marketplace identifier. Specifies the marketplace for which prices are returned. | 
 **itemCondition** | **string**| Filters the offer listings based on item condition. Possible values: New, Used, Collectible, Refurbished, Club. | 
 **sellerSKU** | **string**| Identifies an item in the given marketplace. SellerSKU is qualified by the seller&#39;s SellerId, which is included with every operation that you submit. | 
 **customerType** | **string**| Indicates whether to request Consumer or Business offers. Default is Consumer. | [optional] 

### Return type

[**GetOffersResponse**](GetOffersResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getlistingoffersbatch"></a>
# **GetListingOffersBatch**
> GetListingOffersBatchResponse GetListingOffersBatch (GetListingOffersBatchRequest getListingOffersBatchRequestBody)



Returns the lowest priced offers for a batch of listings by SKU.  **Usage Plan:**  | Rate (requests per second) | Burst | | - -- - | - -- - | | 0.5 | 1 |  The `x-amzn-RateLimit-Limit` response header returns the usage plan rate limits that were applied to the requested operation, when available. The table above indicates the default rate and burst values for this operation. Selling partners whose business demands require higher throughput may see higher rate and burst values than those shown here. For more information, see [Usage Plans and Rate Limits in the Selling Partner API](doc:usage-plans-and-rate-limits-in-the-sp-api).

### Example
```csharp
using System;
using System.Diagnostics;
using Amazon.SellingPartnerAPIAA.Client.Api;
using Amazon.SellingPartnerAPIAA.Client.Client;
using Amazon.SellingPartnerAPIAA.Client.Model;

namespace Example
{
    public class GetListingOffersBatchExample
    {
        public void main()
        {
            var apiInstance = new ProductPricingApi();
            var getListingOffersBatchRequestBody = new GetListingOffersBatchRequest(); // GetListingOffersBatchRequest | 

            try
            {
                GetListingOffersBatchResponse result = apiInstance.GetListingOffersBatch(getListingOffersBatchRequestBody);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ProductPricingApi.GetListingOffersBatch: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **getListingOffersBatchRequestBody** | [**GetListingOffersBatchRequest**](GetListingOffersBatchRequest.md)|  | 

### Return type

[**GetListingOffersBatchResponse**](GetListingOffersBatchResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getpricing"></a>
# **GetPricing**
> GetPricingResponse GetPricing (string marketplaceId, string itemType, List<string> asins = null, List<string> skus = null, string itemCondition = null, string offerType = null)



Returns pricing information for a seller's offer listings based on seller SKU or ASIN.  **Usage Plan:**  | Rate (requests per second) | Burst | | - -- - | - -- - | | 0.5 | 1 |  The `x-amzn-RateLimit-Limit` response header returns the usage plan rate limits that were applied to the requested operation, when available. The table above indicates the default rate and burst values for this operation. Selling partners whose business demands require higher throughput may see higher rate and burst values than those shown here. For more information, see [Usage Plans and Rate Limits in the Selling Partner API](doc:usage-plans-and-rate-limits-in-the-sp-api).

### Example
```csharp
using System;
using System.Diagnostics;
using Amazon.SellingPartnerAPIAA.Client.Api;
using Amazon.SellingPartnerAPIAA.Client.Client;
using Amazon.SellingPartnerAPIAA.Client.Model;

namespace Example
{
    public class GetPricingExample
    {
        public void main()
        {
            var apiInstance = new ProductPricingApi();
            var marketplaceId = marketplaceId_example;  // string | A marketplace identifier. Specifies the marketplace for which prices are returned.
            var itemType = itemType_example;  // string | Indicates whether ASIN values or seller SKU values are used to identify items. If you specify Asin, the information in the response will be dependent on the list of Asins you provide in the Asins parameter. If you specify Sku, the information in the response will be dependent on the list of Skus you provide in the Skus parameter.
            var asins = new List<string>(); // List<string> | A list of up to twenty Amazon Standard Identification Number (ASIN) values used to identify items in the given marketplace. (optional) 
            var skus = new List<string>(); // List<string> | A list of up to twenty seller SKU values used to identify items in the given marketplace. (optional) 
            var itemCondition = itemCondition_example;  // string | Filters the offer listings based on item condition. Possible values: New, Used, Collectible, Refurbished, Club. (optional) 
            var offerType = offerType_example;  // string | Indicates whether to request pricing information for the seller's B2C or B2B offers. Default is B2C. (optional) 

            try
            {
                GetPricingResponse result = apiInstance.GetPricing(marketplaceId, itemType, asins, skus, itemCondition, offerType);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ProductPricingApi.GetPricing: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **marketplaceId** | **string**| A marketplace identifier. Specifies the marketplace for which prices are returned. | 
 **itemType** | **string**| Indicates whether ASIN values or seller SKU values are used to identify items. If you specify Asin, the information in the response will be dependent on the list of Asins you provide in the Asins parameter. If you specify Sku, the information in the response will be dependent on the list of Skus you provide in the Skus parameter. | 
 **asins** | [**List&lt;string&gt;**](string.md)| A list of up to twenty Amazon Standard Identification Number (ASIN) values used to identify items in the given marketplace. | [optional] 
 **skus** | [**List&lt;string&gt;**](string.md)| A list of up to twenty seller SKU values used to identify items in the given marketplace. | [optional] 
 **itemCondition** | **string**| Filters the offer listings based on item condition. Possible values: New, Used, Collectible, Refurbished, Club. | [optional] 
 **offerType** | **string**| Indicates whether to request pricing information for the seller&#39;s B2C or B2B offers. Default is B2C. | [optional] 

### Return type

[**GetPricingResponse**](GetPricingResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

