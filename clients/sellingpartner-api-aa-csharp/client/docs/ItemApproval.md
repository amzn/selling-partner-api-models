# Amazon.SellingPartnerAPIAA.Client.Model.ItemApproval
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**SequenceId** | **int?** | Sequence number of the item approval. Each ItemApproval gets its sequenceId automatically from a monotonic increasing function. | 
**Timestamp** | **string** | Timestamp when the ItemApproval was recorded by Amazon&#39;s internal order approvals system. In ISO 8601 date time format. | 
**Actor** | **string** | High level actors involved in the approval process. | 
**Approver** | **string** | Person or system that triggers the approval actions on behalf of the actor. | [optional] 
**ApprovalAction** | [**ItemApprovalAction**](ItemApprovalAction.md) | Approval action that defines the behavior of the ItemApproval. | 
**ApprovalActionProcessStatus** | **string** | Status of approval action. | 
**ApprovalActionProcessStatusMessage** | **string** | Optional message to communicate optional additional context about the current status of the approval action. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

