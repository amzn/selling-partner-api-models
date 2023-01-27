# Amazon.SellingPartnerAPIAA.Client.Model.Order
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**AmazonOrderId** | **string** | An Amazon-defined order identifier, in 3-7-7 format. | 
**SellerOrderId** | **string** | A seller-defined order identifier. | [optional] 
**PurchaseDate** | **string** | The date when the order was created. | 
**LastUpdateDate** | **string** | The date when the order was last updated.  __Note__: LastUpdateDate is returned with an incorrect date for orders that were last updated before 2009-04-01. | 
**OrderStatus** | **string** | The current order status. | 
**FulfillmentChannel** | **string** | Whether the order was fulfilled by Amazon (AFN) or by the seller (MFN). | [optional] 
**SalesChannel** | **string** | The sales channel of the first item in the order. | [optional] 
**OrderChannel** | **string** | The order channel of the first item in the order. | [optional] 
**ShipServiceLevel** | **string** | The shipment service level of the order. | [optional] 
**OrderTotal** | [**Money**](Money.md) | The total charge for this order. | [optional] 
**NumberOfItemsShipped** | **int?** | The number of items shipped. | [optional] 
**NumberOfItemsUnshipped** | **int?** | The number of items unshipped. | [optional] 
**PaymentExecutionDetail** | [**PaymentExecutionDetailItemList**](PaymentExecutionDetailItemList.md) | Information about sub-payment methods for a Cash On Delivery (COD) order.  __Note__: For a COD order that is paid for using one sub-payment method, one PaymentExecutionDetailItem object is returned, with PaymentExecutionDetailItem/PaymentMethod &#x3D; COD. For a COD order that is paid for using multiple sub-payment methods, two or more PaymentExecutionDetailItem objects are returned. | [optional] 
**PaymentMethod** | **string** | The payment method for the order. This property is limited to Cash On Delivery (COD) and Convenience Store (CVS) payment methods. Unless you need the specific COD payment information provided by the PaymentExecutionDetailItem object, we recommend using the PaymentMethodDetails property to get payment method information. | [optional] 
**PaymentMethodDetails** | [**PaymentMethodDetailItemList**](PaymentMethodDetailItemList.md) | A list of payment methods for the order. | [optional] 
**MarketplaceId** | **string** | The identifier for the marketplace where the order was placed. | [optional] 
**ShipmentServiceLevelCategory** | **string** | The shipment service level category of the order.  Possible values: Expedited, FreeEconomy, NextDay, SameDay, SecondDay, Scheduled, Standard. | [optional] 
**EasyShipShipmentStatus** | **EasyShipShipmentStatus** | The status of the Amazon Easy Ship order. This property is included only for Amazon Easy Ship orders. | [optional] 
**CbaDisplayableShippingLabel** | **string** | Custom ship label for Checkout by Amazon (CBA). | [optional] 
**OrderType** | **string** | The type of the order. | [optional] 
**EarliestShipDate** | **string** | The start of the time period within which you have committed to ship the order. In ISO 8601 date time format. Returned only for seller-fulfilled orders.  __Note__: EarliestShipDate might not be returned for orders placed before February 1, 2013. | [optional] 
**LatestShipDate** | **string** | The end of the time period within which you have committed to ship the order. In ISO 8601 date time format. Returned only for seller-fulfilled orders.  __Note__: LatestShipDate might not be returned for orders placed before February 1, 2013. | [optional] 
**EarliestDeliveryDate** | **string** | The start of the time period within which you have committed to fulfill the order. In ISO 8601 date time format. Returned only for seller-fulfilled orders. | [optional] 
**LatestDeliveryDate** | **string** | The end of the time period within which you have committed to fulfill the order. In ISO 8601 date time format. Returned only for seller-fulfilled orders that do not have a PendingAvailability, Pending, or Canceled status. | [optional] 
**IsBusinessOrder** | **bool?** | When true, the order is an Amazon Business order. An Amazon Business order is an order where the buyer is a Verified Business Buyer. | [optional] 
**IsPrime** | **bool?** | When true, the order is a seller-fulfilled Amazon Prime order. | [optional] 
**IsPremiumOrder** | **bool?** | When true, the order has a Premium Shipping Service Level Agreement. For more information about Premium Shipping orders, see \&quot;Premium Shipping Options\&quot; in the Seller Central Help for your marketplace. | [optional] 
**IsGlobalExpressEnabled** | **bool?** | When true, the order is a GlobalExpress order. | [optional] 
**ReplacedOrderId** | **string** | The order ID value for the order that is being replaced. Returned only if IsReplacementOrder &#x3D; true. | [optional] 
**IsReplacementOrder** | **bool?** | When true, this is a replacement order. | [optional] 
**PromiseResponseDueDate** | **string** | Indicates the date by which the seller must respond to the buyer with an estimated ship date. Returned only for Sourcing on Demand orders. | [optional] 
**IsEstimatedShipDateSet** | **bool?** | When true, the estimated ship date is set for the order. Returned only for Sourcing on Demand orders. | [optional] 
**IsSoldByAB** | **bool?** | When true, the item within this order was bought and re-sold by Amazon Business EU SARL (ABEU). By buying and instantly re-selling your items, ABEU becomes the seller of record, making your inventory available for sale to customers who would not otherwise purchase from a third-party seller. | [optional] 
**IsIBA** | **bool?** | When true, the item within this order was bought and re-sold by Amazon Business EU SARL (ABEU). By buying and instantly re-selling your items, ABEU becomes the seller of record, making your inventory available for sale to customers who would not otherwise purchase from a third-party seller. | [optional] 
**DefaultShipFromLocationAddress** | [**Address**](Address.md) | The recommended location for the seller to ship the items from. It is calculated at checkout. The seller may or may not choose to ship from this location. | [optional] 
**BuyerInvoicePreference** | **string** | The buyer&#39;s invoicing preference. Available only in the TR marketplace. | [optional] 
**BuyerTaxInformation** | [**BuyerTaxInformation**](BuyerTaxInformation.md) | Contains the business invoice tax information. | [optional] 
**FulfillmentInstruction** | [**FulfillmentInstruction**](FulfillmentInstruction.md) | Contains the instructions about the fulfillment like where should it be fulfilled from. | [optional] 
**IsISPU** | **bool?** | When true, this order is marked to be picked up from a store rather than delivered. | [optional] 
**IsAccessPointOrder** | **bool?** | When true, this order is marked to be delivered to an Access Point. The access location is chosen by the customer. Access Points include Amazon Hub Lockers, Amazon Hub Counters, and pickup points operated by carriers. | [optional] 
**MarketplaceTaxInfo** | [**MarketplaceTaxInfo**](MarketplaceTaxInfo.md) | Tax information about the marketplace. | [optional] 
**SellerDisplayName** | **string** | The seller’s friendly name registered in the marketplace. | [optional] 
**ShippingAddress** | [**Address**](Address.md) |  | [optional] 
**BuyerInfo** | [**BuyerInfo**](BuyerInfo.md) |  | [optional] 
**AutomatedShippingSettings** | [**AutomatedShippingSettings**](AutomatedShippingSettings.md) | Contains information regarding the Shipping Settings Automaton program, such as whether the order&#39;s shipping settings were generated automatically, and what those settings are. | [optional] 
**HasRegulatedItems** | **bool?** | Whether the order contains regulated items which may require additional approval steps before being fulfilled. | [optional] 
**ElectronicInvoiceStatus** | **ElectronicInvoiceStatus** | The status of the electronic invoice. | [optional] 
**ItemApprovalTypes** | [**List&lt;ItemApprovalType&gt;**](ItemApprovalType.md) | Set of approval types which applies to at least one order item in the order. | [optional] 
**ItemApprovalStatus** | [**List&lt;ItemApprovalStatus&gt;**](ItemApprovalStatus.md) | Subset of all ItemApprovalStatus that are set in at least one of the order items subject to approvals. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

