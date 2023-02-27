# Amazon.SellingPartnerAPIAA.Clients.Models.Orders.RegulatedOrderVerificationStatus
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Status** | **VerificationStatus** | The verification status of the order. | 
**RequiresMerchantAction** | **bool?** | When true, the regulated information provided in the order requires a review by the merchant. | 
**ValidRejectionReasons** | [**List&lt;RejectionReason&gt;**](RejectionReason.md) | A list of valid rejection reasons that may be used to reject the order&#39;s regulated information. | 
**RejectionReason** | [**RejectionReason**](RejectionReason.md) | The reason for rejecting the order&#39;s regulated information. Not present if the order isn&#39;t rejected. | [optional] 
**ReviewDate** | **string** | The date the order was reviewed. In ISO 8601 date time format. | [optional] 
**ExternalReviewerId** | **string** | The identifier for the order&#39;s regulated information reviewer. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

