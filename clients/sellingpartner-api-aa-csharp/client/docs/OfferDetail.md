# Amazon.SellingPartnerAPIAA.Client.Model.OfferDetail
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**MyOffer** | **bool?** | When true, this is the seller&#39;s offer. | [optional] 
**OfferType** | **OfferCustomerType** | Indicates the type of customer that the offer is valid for. | [optional] 
**SubCondition** | **string** | The subcondition of the item. Subcondition values: New, Mint, Very Good, Good, Acceptable, Poor, Club, OEM, Warranty, Refurbished Warranty, Refurbished, Open Box, or Other. | 
**SellerId** | **string** | The seller identifier for the offer. | [optional] 
**ConditionNotes** | **string** | Information about the condition of the item. | [optional] 
**SellerFeedbackRating** | [**SellerFeedbackType**](SellerFeedbackType.md) | Information about the seller&#39;s feedback, including the percentage of positive feedback, and the total number of ratings received. | [optional] 
**ShippingTime** | [**DetailedShippingTimeType**](DetailedShippingTimeType.md) | The maximum time within which the item will likely be shipped once an order has been placed. | 
**ListingPrice** | [**MoneyType**](MoneyType.md) | The price of the item. | 
**QuantityDiscountPrices** | [**List&lt;QuantityDiscountPriceType&gt;**](QuantityDiscountPriceType.md) |  | [optional] 
**Points** | [**Points**](Points.md) | The number of Amazon Points offered with the purchase of an item. | [optional] 
**Shipping** | [**MoneyType**](MoneyType.md) | The shipping cost. | 
**ShipsFrom** | [**ShipsFromType**](ShipsFromType.md) | The state and country from where the item is shipped. | [optional] 
**IsFulfilledByAmazon** | **bool?** | When true, the offer is fulfilled by Amazon. | 
**PrimeInformation** | [**PrimeInformationType**](PrimeInformationType.md) | Amazon Prime information. | [optional] 
**IsBuyBoxWinner** | **bool?** | When true, the offer is currently in the Buy Box. There can be up to two Buy Box winners at any time per ASIN, one that is eligible for Prime and one that is not eligible for Prime. | [optional] 
**IsFeaturedMerchant** | **bool?** | When true, the seller of the item is eligible to win the Buy Box. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

