# Amazon.SellingPartnerAPIAA.Client.Model.Summary
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**TotalOfferCount** | **int?** | The number of unique offers contained in NumberOfOffers. | 
**NumberOfOffers** | [**NumberOfOffers**](NumberOfOffers.md) | A list that contains the total number of offers for the item for the given conditions and fulfillment channels. | [optional] 
**LowestPrices** | [**LowestPrices**](LowestPrices.md) | A list of the lowest prices for the item. | [optional] 
**BuyBoxPrices** | [**BuyBoxPrices**](BuyBoxPrices.md) | A list of item prices. | [optional] 
**ListPrice** | [**MoneyType**](MoneyType.md) | The list price of the item as suggested by the manufacturer. | [optional] 
**CompetitivePriceThreshold** | [**MoneyType**](MoneyType.md) | This price is based on competitive prices from other retailers (excluding other Amazon sellers). The offer may be ineligible for the Buy Box if the seller&#39;s price + shipping (minus Amazon Points) is greater than this competitive price. | [optional] 
**SuggestedLowerPricePlusShipping** | [**MoneyType**](MoneyType.md) | The suggested lower price of the item, including shipping and Amazon Points. The suggested lower price is based on a range of factors, including historical selling prices, recent Buy Box-eligible prices, and input from customers for your products. | [optional] 
**SalesRankings** | [**SalesRankList**](SalesRankList.md) | A list that contains the sales rank of the item in the given product categories. | [optional] 
**BuyBoxEligibleOffers** | [**BuyBoxEligibleOffers**](BuyBoxEligibleOffers.md) | A list that contains the total number of offers that are eligible for the Buy Box for the given conditions and fulfillment channels. | [optional] 
**OffersAvailableTime** | **DateTime?** | When the status is ActiveButTooSoonForProcessing, this is the time when the offers will be available for processing. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

