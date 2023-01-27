# Amazon.SellingPartnerAPIAA.Client.Model.FeedDocument
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**FeedDocumentId** | **string** | The identifier for the feed document. This identifier is unique only in combination with a seller ID. | 
**Url** | **string** | A presigned URL for the feed document. If &#x60;compressionAlgorithm&#x60; is not returned, you can download the feed directly from this URL. This URL expires after 5 minutes. | 
**CompressionAlgorithm** | **string** | If the feed document contents have been compressed, the compression algorithm used is returned in this property and you must decompress the feed when you download. Otherwise, you can download the feed directly. Refer to [Step 7. Download the feed processing report](doc:feeds-api-v2021-06-30-use-case-guide#step-7-download-the-feed-processing-report) in the use case guide, where sample code is provided. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

