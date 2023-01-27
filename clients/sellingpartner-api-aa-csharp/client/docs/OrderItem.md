# Amazon.SellingPartnerAPIAA.Client.Model.OrderItem
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**ASIN** | **string** | The Amazon Standard Identification Number (ASIN) of the item. | 
**SellerSKU** | **string** | The seller stock keeping unit (SKU) of the item. | [optional] 
**OrderItemId** | **string** | An Amazon-defined order item identifier. | 
**Title** | **string** | The name of the item. | [optional] 
**QuantityOrdered** | **int?** | The number of items in the order.  | 
**QuantityShipped** | **int?** | The number of items shipped. | [optional] 
**ProductInfo** | [**ProductInfoDetail**](ProductInfoDetail.md) | Product information for the item. | [optional] 
**PointsGranted** | [**PointsGrantedDetail**](PointsGrantedDetail.md) | The number and value of Amazon Points granted with the purchase of an item. | [optional] 
**ItemPrice** | [**Money**](Money.md) | The selling price of the order item. Note that an order item is an item and a quantity. This means that the value of ItemPrice is equal to the selling price of the item multiplied by the quantity ordered. Note that ItemPrice excludes ShippingPrice and GiftWrapPrice. | [optional] 
**ShippingPrice** | [**Money**](Money.md) | The shipping price of the item. | [optional] 
**ItemTax** | [**Money**](Money.md) | The tax on the item price. | [optional] 
**ShippingTax** | [**Money**](Money.md) | The tax on the shipping price. | [optional] 
**ShippingDiscount** | [**Money**](Money.md) | The discount on the shipping price. | [optional] 
**ShippingDiscountTax** | [**Money**](Money.md) | The tax on the discount on the shipping price. | [optional] 
**PromotionDiscount** | [**Money**](Money.md) | The total of all promotional discounts in the offer. | [optional] 
**PromotionDiscountTax** | [**Money**](Money.md) | The tax on the total of all promotional discounts in the offer. | [optional] 
**PromotionIds** | [**PromotionIdList**](PromotionIdList.md) |  | [optional] 
**CODFee** | [**Money**](Money.md) | The fee charged for COD service. | [optional] 
**CODFeeDiscount** | [**Money**](Money.md) | The discount on the COD fee. | [optional] 
**IsGift** | **bool?** | When true, the item is a gift. | [optional] 
**ConditionNote** | **string** | The condition of the item as described by the seller. | [optional] 
**ConditionId** | **string** | The condition of the item.  Possible values: New, Used, Collectible, Refurbished, Preorder, Club. | [optional] 
**ConditionSubtypeId** | **string** | The subcondition of the item.  Possible values: New, Mint, Very Good, Good, Acceptable, Poor, Club, OEM, Warranty, Refurbished Warranty, Refurbished, Open Box, Any, Other. | [optional] 
**ScheduledDeliveryStartDate** | **string** | The start date of the scheduled delivery window in the time zone of the order destination. In ISO 8601 date time format. | [optional] 
**ScheduledDeliveryEndDate** | **string** | The end date of the scheduled delivery window in the time zone of the order destination. In ISO 8601 date time format. | [optional] 
**PriceDesignation** | **string** | Indicates that the selling price is a special price that is available only for Amazon Business orders. For more information about the Amazon Business Seller Program, see the [Amazon Business website](https://www.amazon.com/b2b/info/amazon-business).   Possible values: BusinessPrice - A special price that is available only for Amazon Business orders. | [optional] 
**TaxCollection** | [**TaxCollection**](TaxCollection.md) | Information about withheld taxes. | [optional] 
**SerialNumberRequired** | **bool?** | When true, the product type for this item has a serial number.  Returned only for Amazon Easy Ship orders. | [optional] 
**IsTransparency** | **bool?** | When true, transparency codes are required. | [optional] 
**IossNumber** | **string** | The IOSS number for the marketplace. Sellers shipping to the European Union (EU) from outside of the EU must provide this IOSS number to their carrier when Amazon has collected the VAT on the sale. | [optional] 
**StoreChainStoreId** | **string** | The store chain store identifier. Linked to a specific store in a store chain. | [optional] 
**DeemedResellerCategory** | **string** | The category of deemed reseller. This applies to selling partners that are not based in the EU and is used to help them meet the VAT Deemed Reseller tax laws in the EU and UK. | [optional] 
**BuyerInfo** | [**ItemBuyerInfo**](ItemBuyerInfo.md) |  | [optional] 
**BuyerRequestedCancel** | [**BuyerRequestedCancel**](BuyerRequestedCancel.md) | Information about whether or not a buyer requested cancellation. | [optional] 
**ItemApprovalContext** | [**ItemApprovalContext**](ItemApprovalContext.md) | Item approval context containing the information regarding the status and progress of the item approval. | [optional] 
**SerialNumbers** | **List&lt;string&gt;** | A list of serial numbers for electronic products that are shipped to customers. Returned for FBA orders only. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

