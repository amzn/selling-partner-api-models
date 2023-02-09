# Amazon.SellingPartnerAPIAA.Client.Api.DefinitionsApi

All URIs are relative to *https://sellingpartnerapi-na.amazon.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**GetDefinitionsProductType**](DefinitionsApi.md#getdefinitionsproducttype) | **GET** /definitions/2020-09-01/productTypes/{productType} | 
[**SearchDefinitionsProductTypes**](DefinitionsApi.md#searchdefinitionsproducttypes) | **GET** /definitions/2020-09-01/productTypes | 


<a name="getdefinitionsproducttype"></a>
# **GetDefinitionsProductType**
> ProductTypeDefinition GetDefinitionsProductType (string productType, List<string> marketplaceIds, string sellerId = null, string productTypeVersion = null, string requirements = null, string requirementsEnforced = null, string locale = null)



Retrieve an Amazon product type definition.  **Usage Plans:**  | Plan type | Rate (requests per second) | Burst | | - -- - | - -- - | - -- - | |Default| 5 | 10 | |Selling partner specific| Variable | Variable |  The x-amzn-RateLimit-Limit response header returns the usage plan rate limits that were applied to the requested operation. Rate limits for some selling partners will vary from the default rate and burst shown in the table above. For more information, see [Usage Plans and Rate Limits in the Selling Partner API](doc:usage-plans-and-rate-limits-in-the-sp-api).

### Example
```csharp
using System;
using System.Diagnostics;
using Amazon.SellingPartnerAPIAA.Client.Api;
using Amazon.SellingPartnerAPIAA.Client.Client;
using Amazon.SellingPartnerAPIAA.Client.Model;

namespace Example
{
    public class GetDefinitionsProductTypeExample
    {
        public void main()
        {
            var apiInstance = new DefinitionsApi();
            var productType = LUGGAGE;  // string | The Amazon product type name.
            var marketplaceIds = new List<string>(); // List<string> | A comma-delimited list of Amazon marketplace identifiers for the request. Note: This parameter is limited to one marketplaceId at this time.
            var sellerId = sellerId_example;  // string | A selling partner identifier. When provided, seller-specific requirements and values are populated within the product type definition schema, such as brand names associated with the selling partner. (optional) 
            var productTypeVersion = LATEST;  // string | The version of the Amazon product type to retrieve. Defaults to \"LATEST\",. Prerelease versions of product type definitions may be retrieved with \"RELEASE_CANDIDATE\". If no prerelease version is currently available, the \"LATEST\" live version will be provided. (optional)  (default to LATEST)
            var requirements = LISTING;  // string | The name of the requirements set to retrieve requirements for. (optional)  (default to LISTING)
            var requirementsEnforced = ENFORCED;  // string | Identifies if the required attributes for a requirements set are enforced by the product type definition schema. Non-enforced requirements enable structural validation of individual attributes without all the required attributes being present (such as for partial updates). (optional)  (default to ENFORCED)
            var locale = DEFAULT;  // string | Locale for retrieving display labels and other presentation details. Defaults to the default language of the first marketplace in the request. (optional)  (default to DEFAULT)

            try
            {
                ProductTypeDefinition result = apiInstance.GetDefinitionsProductType(productType, marketplaceIds, sellerId, productTypeVersion, requirements, requirementsEnforced, locale);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DefinitionsApi.GetDefinitionsProductType: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **productType** | **string**| The Amazon product type name. | 
 **marketplaceIds** | [**List&lt;string&gt;**](string.md)| A comma-delimited list of Amazon marketplace identifiers for the request. Note: This parameter is limited to one marketplaceId at this time. | 
 **sellerId** | **string**| A selling partner identifier. When provided, seller-specific requirements and values are populated within the product type definition schema, such as brand names associated with the selling partner. | [optional] 
 **productTypeVersion** | **string**| The version of the Amazon product type to retrieve. Defaults to \&quot;LATEST\&quot;,. Prerelease versions of product type definitions may be retrieved with \&quot;RELEASE_CANDIDATE\&quot;. If no prerelease version is currently available, the \&quot;LATEST\&quot; live version will be provided. | [optional] [default to LATEST]
 **requirements** | **string**| The name of the requirements set to retrieve requirements for. | [optional] [default to LISTING]
 **requirementsEnforced** | **string**| Identifies if the required attributes for a requirements set are enforced by the product type definition schema. Non-enforced requirements enable structural validation of individual attributes without all the required attributes being present (such as for partial updates). | [optional] [default to ENFORCED]
 **locale** | **string**| Locale for retrieving display labels and other presentation details. Defaults to the default language of the first marketplace in the request. | [optional] [default to DEFAULT]

### Return type

[**ProductTypeDefinition**](ProductTypeDefinition.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="searchdefinitionsproducttypes"></a>
# **SearchDefinitionsProductTypes**
> ProductTypeList SearchDefinitionsProductTypes (List<string> marketplaceIds, List<string> keywords = null)



Search for and return a list of Amazon product types that have definitions available.  **Usage Plans:**  | Plan type | Rate (requests per second) | Burst | | - -- - | - -- - | - -- - | |Default| 5 | 10 | |Selling partner specific| Variable | Variable |  The x-amzn-RateLimit-Limit response header returns the usage plan rate limits that were applied to the requested operation. Rate limits for some selling partners will vary from the default rate and burst shown in the table above. For more information, see [Usage Plans and Rate Limits in the Selling Partner API](doc:usage-plans-and-rate-limits-in-the-sp-api).

### Example
```csharp
using System;
using System.Diagnostics;
using Amazon.SellingPartnerAPIAA.Client.Api;
using Amazon.SellingPartnerAPIAA.Client.Client;
using Amazon.SellingPartnerAPIAA.Client.Model;

namespace Example
{
    public class SearchDefinitionsProductTypesExample
    {
        public void main()
        {
            var apiInstance = new DefinitionsApi();
            var marketplaceIds = new List<string>(); // List<string> | A comma-delimited list of Amazon marketplace identifiers for the request.
            var keywords = new List<string>(); // List<string> | A comma-delimited list of keywords to search product types by. (optional) 

            try
            {
                ProductTypeList result = apiInstance.SearchDefinitionsProductTypes(marketplaceIds, keywords);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DefinitionsApi.SearchDefinitionsProductTypes: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **marketplaceIds** | [**List&lt;string&gt;**](string.md)| A comma-delimited list of Amazon marketplace identifiers for the request. | 
 **keywords** | [**List&lt;string&gt;**](string.md)| A comma-delimited list of keywords to search product types by. | [optional] 

### Return type

[**ProductTypeList**](ProductTypeList.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

