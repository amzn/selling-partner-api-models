# Amazon.SellingPartnerAPIAA.Client.Model.CreateReportSpecification
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**ReportOptions** | [**ReportOptions**](ReportOptions.md) |  | [optional] 
**ReportType** | **string** | The report type. | 
**DataStartTime** | **DateTime?** | The start of a date and time range, in ISO 8601 date time format, used for selecting the data to report. The default is now. The value must be prior to or equal to the current date and time. Not all report types make use of this. | [optional] 
**DataEndTime** | **DateTime?** | The end of a date and time range, in ISO 8601 date time format, used for selecting the data to report. The default is now. The value must be prior to or equal to the current date and time. Not all report types make use of this. | [optional] 
**MarketplaceIds** | **List&lt;string&gt;** | A list of marketplace identifiers. The report document&#39;s contents will contain data for all of the specified marketplaces, unless the report type indicates otherwise. | 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

