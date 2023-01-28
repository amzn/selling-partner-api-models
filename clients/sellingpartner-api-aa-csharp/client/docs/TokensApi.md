# Amazon.SellingPartnerAPIAA.Client.Api.TokensApi

All URIs are relative to *https://sellingpartnerapi-na.amazon.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**CreateRestrictedDataToken**](TokensApi.md#createrestricteddatatoken) | **POST** /tokens/2021-03-01/restrictedDataToken | 


<a name="createrestricteddatatoken"></a>
# **CreateRestrictedDataToken**
> CreateRestrictedDataTokenResponse CreateRestrictedDataToken (CreateRestrictedDataTokenRequest body)



Returns a Restricted Data Token (RDT) for one or more restricted resources that you specify. A restricted resource is the HTTP method and path from a restricted operation that returns Personally Identifiable Information (PII), plus a dataElements value that indicates the type of PII requested. See the Tokens API Use Case Guide for a list of restricted operations. Use the RDT returned here as the access token in subsequent calls to the corresponding restricted operations.  **Usage Plans:**  | Plan type | Rate (requests per second) | Burst | | - -- - | - -- - | - -- - | |Default| 1 | 10 | |Selling partner specific| Variable | Variable |  The x-amzn-RateLimit-Limit response header returns the usage plan rate limits that were applied to the requested operation. Rate limits for some selling partners will vary from the default rate and burst shown in the table above. For more information, see \"Usage Plans and Rate Limits\" in the Selling Partner API documentation.

### Example
```csharp
using System;
using System.Diagnostics;
using Amazon.SellingPartnerAPIAA.Client.Api;
using Amazon.SellingPartnerAPIAA.Client.Client;
using Amazon.SellingPartnerAPIAA.Client.Model;

namespace Example
{
    public class CreateRestrictedDataTokenExample
    {
        public void main()
        {
            var apiInstance = new TokensApi();
            var body = new CreateRestrictedDataTokenRequest(); // CreateRestrictedDataTokenRequest | The restricted data token request details.

            try
            {
                CreateRestrictedDataTokenResponse result = apiInstance.CreateRestrictedDataToken(body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling TokensApi.CreateRestrictedDataToken: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**CreateRestrictedDataTokenRequest**](CreateRestrictedDataTokenRequest.md)| The restricted data token request details. | 

### Return type

[**CreateRestrictedDataTokenResponse**](CreateRestrictedDataTokenResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

