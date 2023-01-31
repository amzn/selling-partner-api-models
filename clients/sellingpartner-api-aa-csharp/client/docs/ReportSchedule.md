# Amazon.SellingPartnerAPIAA.Client.Model.ReportSchedule
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**ReportScheduleId** | **string** | The identifier for the report schedule. This identifier is unique only in combination with a seller ID. | 
**ReportType** | **string** | The report type. | 
**MarketplaceIds** | **List&lt;string&gt;** | A list of marketplace identifiers. The report document&#39;s contents will contain data for all of the specified marketplaces, unless the report type indicates otherwise. | [optional] 
**ReportOptions** | [**ReportOptions**](ReportOptions.md) |  | [optional] 
**Period** | **string** | An ISO 8601 period value that indicates how often a report should be created. | 
**NextReportCreationTime** | **DateTime?** | The date and time when the schedule will create its next report, in ISO 8601 date time format. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

