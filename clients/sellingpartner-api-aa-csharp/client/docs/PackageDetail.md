# Amazon.SellingPartnerAPIAA.Clients.Models.Orders.PackageDetail
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**PackageReferenceId** | **string** |  | 
**CarrierCode** | **string** | Identifies the carrier that will deliver the package. This field is required for all marketplaces, see [reference](https://developer-docs.amazon.com/sp-api/changelog/carriercode-value-required-in-shipment-confirmations-for-br-mx-ca-sg-au-in-jp-marketplaces). | 
**CarrierName** | **string** | Carrier Name that will deliver the package. Required when carrierCode is \&quot;Others\&quot;  | [optional] 
**ShippingMethod** | **string** | Ship method to be used for shipping the order. | [optional] 
**TrackingNumber** | **string** | The tracking number used to obtain tracking and delivery information. | 
**ShipDate** | **DateTime?** | The shipping date for the package. Must be in ISO-8601 date/time format. | 
**ShipFromSupplySourceId** | **string** | The unique identifier of the supply source. | [optional] 
**OrderItems** | [**ConfirmShipmentOrderItemsList**](ConfirmShipmentOrderItemsList.md) | The list of order items and quantities to be updated. | 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

