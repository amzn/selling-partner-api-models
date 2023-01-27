# Amazon.SellingPartnerAPIAA.Client.Model.BuyBoxPriceType
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Condition** | **string** | Indicates the condition of the item. For example: New, Used, Collectible, Refurbished, or Club. | 
**OfferType** | **OfferCustomerType** | Indicates the type of customer that the offer is valid for.&lt;br&gt;&lt;br&gt;When the offer type is B2C in a quantity discount, the seller is winning the Buy Box because others do not have inventory at that quantity, not because they have a quantity discount on the ASIN. | [optional] 
**QuantityTier** | **int?** | Indicates at what quantity this price becomes active. | [optional] 
**QuantityDiscountType** | **QuantityDiscountType** | Indicates the type of quantity discount this price applies to. | [optional] 
**LandedPrice** | [**MoneyType**](MoneyType.md) | The value calculated by adding ListingPrice + Shipping - Points. | 
**ListingPrice** | [**MoneyType**](MoneyType.md) | The price of the item. | 
**Shipping** | [**MoneyType**](MoneyType.md) | The shipping cost. | 
**Points** | [**Points**](Points.md) | The number of Amazon Points offered with the purchase of an item. | [optional] 
**SellerId** | **string** | The seller identifier for the offer. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

