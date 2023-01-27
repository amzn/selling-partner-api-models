# Amazon.SellingPartnerAPIAA.Client.Model.OfferType
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**_OfferType** | **OfferCustomerType** | Indicates the type of customer that the offer is valid for. | [optional] 
**BuyingPrice** | [**PriceType**](PriceType.md) | Contains pricing information that includes promotions and contains the shipping cost. | 
**RegularPrice** | [**MoneyType**](MoneyType.md) | The current price excluding any promotions that apply to the product. Excludes the shipping cost. | 
**BusinessPrice** | [**MoneyType**](MoneyType.md) | The current listing price for Business buyers. | [optional] 
**QuantityDiscountPrices** | [**List&lt;QuantityDiscountPriceType&gt;**](QuantityDiscountPriceType.md) |  | [optional] 
**FulfillmentChannel** | **string** | The fulfillment channel for the offer listing. Possible values:  * Amazon - Fulfilled by Amazon. * Merchant - Fulfilled by the seller. | 
**ItemCondition** | **string** | The item condition for the offer listing. Possible values: New, Used, Collectible, Refurbished, or Club. | 
**ItemSubCondition** | **string** | The item subcondition for the offer listing. Possible values: New, Mint, Very Good, Good, Acceptable, Poor, Club, OEM, Warranty, Refurbished Warranty, Refurbished, Open Box, or Other. | 
**SellerSKU** | **string** | The seller stock keeping unit (SKU) of the item. | 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

