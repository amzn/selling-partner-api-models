# Amazon.SellingPartnerAPIAA.Client.Api.CatalogApi

All URIs are relative to *https://sellingpartnerapi-na.amazon.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**GetCatalogItem**](CatalogApi.md#getcatalogitem) | **GET** /catalog/2022-04-01/items/{asin} | 
[**SearchCatalogItems**](CatalogApi.md#searchcatalogitems) | **GET** /catalog/2022-04-01/items | 


<a name="getcatalogitem"></a>
# **GetCatalogItem**
> Item GetCatalogItem (string asin, List<string> marketplaceIds, List<string> includedData = null, string locale = null)



Retrieves details for an item in the Amazon catalog.  **Usage Plan:**  | Rate (requests per second) | Burst | | - -- - | - -- - | | 5 | 5 |  The `x-amzn-RateLimit-Limit` response header returns the usage plan rate limits that were applied to the requested operation, when available. The table above indicates the default rate and burst values for this operation. Selling partners whose business demands require higher throughput may observe higher rate and burst values than those shown here. For more information, refer to the [Usage Plans and Rate Limits in the Selling Partner API](doc:usage-plans-and-rate-limits-in-the-sp-api).

### Example
```csharp
using System;
using System.Diagnostics;
using Amazon.SellingPartnerAPIAA.Client.Api;
using Amazon.SellingPartnerAPIAA.Client.Client;
using Amazon.SellingPartnerAPIAA.Client.Model;

namespace Example
{
    public class GetCatalogItemExample
    {
        public void main()
        {
            var apiInstance = new CatalogApi();
            var asin = asin_example;  // string | The Amazon Standard Identification Number (ASIN) of the item.
            var marketplaceIds = new List<string>(); // List<string> | A comma-delimited list of Amazon marketplace identifiers. Data sets in the response contain data only for the specified marketplaces.
            var includedData = summaries;  // List<string> | A comma-delimited list of data sets to include in the response. Default: `summaries`. (optional)  (default to ["summaries"])
            var locale = en_US;  // string | Locale for retrieving localized summaries. Defaults to the primary locale of the marketplace. (optional) 

            try
            {
                Item result = apiInstance.GetCatalogItem(asin, marketplaceIds, includedData, locale);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CatalogApi.GetCatalogItem: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **asin** | **string**| The Amazon Standard Identification Number (ASIN) of the item. | 
 **marketplaceIds** | [**List&lt;string&gt;**](string.md)| A comma-delimited list of Amazon marketplace identifiers. Data sets in the response contain data only for the specified marketplaces. | 
 **includedData** | **List&lt;string&gt;**| A comma-delimited list of data sets to include in the response. Default: &#x60;summaries&#x60;. | [optional] [default to [&quot;summaries&quot;]]
 **locale** | **string**| Locale for retrieving localized summaries. Defaults to the primary locale of the marketplace. | [optional] 

### Return type

[**Item**](Item.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="searchcatalogitems"></a>
# **SearchCatalogItems**
> ItemSearchResults SearchCatalogItems (List<string> marketplaceIds, List<string> identifiers = null, string identifiersType = null, List<string> includedData = null, string locale = null, string sellerId = null, List<string> keywords = null, List<string> brandNames = null, List<string> classificationIds = null, int? pageSize = null, string pageToken = null, string keywordsLocale = null)



Search for and return a list of Amazon catalog items and associated information either by identifier or by keywords.  **Usage Plans:**  | Rate (requests per second) | Burst | | - -- - | - -- - | | 5 | 5 |  The `x-amzn-RateLimit-Limit` response header returns the usage plan rate limits that were applied to the requested operation, when available. The table above indicates the default rate and burst values for this operation. Selling partners whose business demands require higher throughput may observe higher rate and burst values than those shown here. For more information, refer to the [Usage Plans and Rate Limits in the Selling Partner API](doc:usage-plans-and-rate-limits-in-the-sp-api).

### Example
```csharp
using System;
using System.Diagnostics;
using Amazon.SellingPartnerAPIAA.Client.Api;
using Amazon.SellingPartnerAPIAA.Client.Client;
using Amazon.SellingPartnerAPIAA.Client.Model;

namespace Example
{
    public class SearchCatalogItemsExample
    {
        public void main()
        {
            var apiInstance = new CatalogApi();
            var marketplaceIds = new List<string>(); // List<string> | A comma-delimited list of Amazon marketplace identifiers for the request.
            var identifiers = new List<string>(); // List<string> | A comma-delimited list of product identifiers to search the Amazon catalog for. **Note:** Cannot be used with `keywords`. (optional) 
            var identifiersType = ASIN;  // string | Type of product identifiers to search the Amazon catalog for. **Note:** Required when `identifiers` are provided. (optional) 
            var includedData = summaries;  // List<string> | A comma-delimited list of data sets to include in the response. Default: `summaries`. (optional)  (default to ["summaries"])
            var locale = en_US;  // string | Locale for retrieving localized summaries. Defaults to the primary locale of the marketplace. (optional) 
            var sellerId = sellerId_example;  // string | A selling partner identifier, such as a seller account or vendor code. **Note:** Required when `identifiersType` is `SKU`. (optional) 
            var keywords = new List<string>(); // List<string> | A comma-delimited list of words to search the Amazon catalog for. **Note:** Cannot be used with `identifiers`. (optional) 
            var brandNames = new List<string>(); // List<string> | A comma-delimited list of brand names to limit the search for `keywords`-based queries. **Note:** Cannot be used with `identifiers`. (optional) 
            var classificationIds = new List<string>(); // List<string> | A comma-delimited list of classification identifiers to limit the search for `keywords`-based queries. **Note:** Cannot be used with `identifiers`. (optional) 
            var pageSize = 9;  // int? | Number of results to be returned per page. (optional)  (default to 10)
            var pageToken = sdlkj234lkj234lksjdflkjwdflkjsfdlkj234234234234;  // string | A token to fetch a certain page when there are multiple pages worth of results. (optional) 
            var keywordsLocale = en_US;  // string | The language of the keywords provided for `keywords`-based queries. Defaults to the primary locale of the marketplace. **Note:** Cannot be used with `identifiers`. (optional) 

            try
            {
                ItemSearchResults result = apiInstance.SearchCatalogItems(marketplaceIds, identifiers, identifiersType, includedData, locale, sellerId, keywords, brandNames, classificationIds, pageSize, pageToken, keywordsLocale);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CatalogApi.SearchCatalogItems: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **marketplaceIds** | [**List&lt;string&gt;**](string.md)| A comma-delimited list of Amazon marketplace identifiers for the request. | 
 **identifiers** | [**List&lt;string&gt;**](string.md)| A comma-delimited list of product identifiers to search the Amazon catalog for. **Note:** Cannot be used with &#x60;keywords&#x60;. | [optional] 
 **identifiersType** | **string**| Type of product identifiers to search the Amazon catalog for. **Note:** Required when &#x60;identifiers&#x60; are provided. | [optional] 
 **includedData** | **List&lt;string&gt;**| A comma-delimited list of data sets to include in the response. Default: &#x60;summaries&#x60;. | [optional] [default to [&quot;summaries&quot;]]
 **locale** | **string**| Locale for retrieving localized summaries. Defaults to the primary locale of the marketplace. | [optional] 
 **sellerId** | **string**| A selling partner identifier, such as a seller account or vendor code. **Note:** Required when &#x60;identifiersType&#x60; is &#x60;SKU&#x60;. | [optional] 
 **keywords** | [**List&lt;string&gt;**](string.md)| A comma-delimited list of words to search the Amazon catalog for. **Note:** Cannot be used with &#x60;identifiers&#x60;. | [optional] 
 **brandNames** | [**List&lt;string&gt;**](string.md)| A comma-delimited list of brand names to limit the search for &#x60;keywords&#x60;-based queries. **Note:** Cannot be used with &#x60;identifiers&#x60;. | [optional] 
 **classificationIds** | [**List&lt;string&gt;**](string.md)| A comma-delimited list of classification identifiers to limit the search for &#x60;keywords&#x60;-based queries. **Note:** Cannot be used with &#x60;identifiers&#x60;. | [optional] 
 **pageSize** | **int?**| Number of results to be returned per page. | [optional] [default to 10]
 **pageToken** | **string**| A token to fetch a certain page when there are multiple pages worth of results. | [optional] 
 **keywordsLocale** | **string**| The language of the keywords provided for &#x60;keywords&#x60;-based queries. Defaults to the primary locale of the marketplace. **Note:** Cannot be used with &#x60;identifiers&#x60;. | [optional] 

### Return type

[**ItemSearchResults**](ItemSearchResults.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

