# Amazon.SellingPartnerAPIAA.Client.Model.PriceType
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**LandedPrice** | [**MoneyType**](MoneyType.md) | The value calculated by adding ListingPrice + Shipping - Points. Note that if the landed price is not returned, the listing price represents the product with the lowest landed price. | [optional] 
**ListingPrice** | [**MoneyType**](MoneyType.md) | The listing price of the item including any promotions that apply. | 
**Shipping** | [**MoneyType**](MoneyType.md) | The shipping cost of the product. Note that the shipping cost is not always available. | [optional] 
**Points** | [**Points**](Points.md) | The number of Amazon Points offered with the purchase of an item, and their monetary value. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

