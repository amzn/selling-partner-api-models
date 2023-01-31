# Amazon.SellingPartnerAPIAA.Client.Model.Report
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**MarketplaceIds** | **List&lt;string&gt;** | A list of marketplace identifiers for the report. | [optional] 
**ReportId** | **string** | The identifier for the report. This identifier is unique only in combination with a seller ID. | 
**ReportType** | **string** | The report type. | 
**DataStartTime** | **DateTime?** | The start of a date and time range used for selecting the data to report. | [optional] 
**DataEndTime** | **DateTime?** | The end of a date and time range used for selecting the data to report. | [optional] 
**ReportScheduleId** | **string** | The identifier of the report schedule that created this report (if any). This identifier is unique only in combination with a seller ID. | [optional] 
**CreatedTime** | **DateTime?** | The date and time when the report was created. | 
**ProcessingStatus** | **string** | The processing status of the report. | 
**ProcessingStartTime** | **DateTime?** | The date and time when the report processing started, in ISO 8601 date time format. | [optional] 
**ProcessingEndTime** | **DateTime?** | The date and time when the report processing completed, in ISO 8601 date time format. | [optional] 
**ReportDocumentId** | **string** | The identifier for the report document. Pass this into the getReportDocument operation to get the information you will need to retrieve the report document&#39;s contents. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

