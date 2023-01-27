# Amazon.SellingPartnerAPIAA.Client.Model.GetOffersResult
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**MarketplaceID** | **string** | A marketplace identifier. | 
**ASIN** | **string** | The Amazon Standard Identification Number (ASIN) of the item. | [optional] 
**SKU** | **string** | The stock keeping unit (SKU) of the item. | [optional] 
**ItemCondition** | **ConditionType** | The condition of the item. | 
**Status** | **string** | The status of the operation. | 
**Identifier** | [**ItemIdentifier**](ItemIdentifier.md) | Metadata that identifies the item. | 
**Summary** | [**Summary**](Summary.md) | Pricing information about the item. | 
**Offers** | [**OfferDetailList**](OfferDetailList.md) | A list of offer details. The list is the same length as the TotalOfferCount in the Summary or 20, whichever is less. | 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

