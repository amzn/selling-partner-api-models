# Amazon.SellingPartnerAPIAA.Client.Api.ReportsApi

All URIs are relative to *https://sellingpartnerapi-na.amazon.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**CancelReport**](ReportsApi.md#cancelreport) | **DELETE** /reports/2021-06-30/reports/{reportId} | 
[**CancelReportSchedule**](ReportsApi.md#cancelreportschedule) | **DELETE** /reports/2021-06-30/schedules/{reportScheduleId} | 
[**CreateReport**](ReportsApi.md#createreport) | **POST** /reports/2021-06-30/reports | 
[**CreateReportSchedule**](ReportsApi.md#createreportschedule) | **POST** /reports/2021-06-30/schedules | 
[**GetReport**](ReportsApi.md#getreport) | **GET** /reports/2021-06-30/reports/{reportId} | 
[**GetReportDocument**](ReportsApi.md#getreportdocument) | **GET** /reports/2021-06-30/documents/{reportDocumentId} | 
[**GetReportSchedule**](ReportsApi.md#getreportschedule) | **GET** /reports/2021-06-30/schedules/{reportScheduleId} | 
[**GetReportSchedules**](ReportsApi.md#getreportschedules) | **GET** /reports/2021-06-30/schedules | 
[**GetReports**](ReportsApi.md#getreports) | **GET** /reports/2021-06-30/reports | 


<a name="cancelreport"></a>
# **CancelReport**
> void CancelReport (string reportId)



Cancels the report that you specify. Only reports with processingStatus=IN_QUEUE can be cancelled. Cancelled reports are returned in subsequent calls to the getReport and getReports operations.  **Usage Plan:**  | Rate (requests per second) | Burst | | - -- - | - -- - | | 0.0222 | 10 |  For more information, see \"Usage Plans and Rate Limits\" in the Selling Partner API documentation.

### Example
```csharp
using System;
using System.Diagnostics;
using Amazon.SellingPartnerAPIAA.Client.Api;
using Amazon.SellingPartnerAPIAA.Client.Client;
using Amazon.SellingPartnerAPIAA.Client.Model;

namespace Example
{
    public class CancelReportExample
    {
        public void main()
        {
            var apiInstance = new ReportsApi();
            var reportId = reportId_example;  // string | The identifier for the report. This identifier is unique only in combination with a seller ID.

            try
            {
                apiInstance.CancelReport(reportId);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ReportsApi.CancelReport: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **reportId** | **string**| The identifier for the report. This identifier is unique only in combination with a seller ID. | 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="cancelreportschedule"></a>
# **CancelReportSchedule**
> void CancelReportSchedule (string reportScheduleId)



Cancels the report schedule that you specify.  **Usage Plan:**  | Rate (requests per second) | Burst | | - -- - | - -- - | | 0.0222 | 10 |  For more information, see [Usage Plans and Rate Limits in the Selling Partner API](doc:usage-plans-and-rate-limits-in-the-sp-api) in the Selling Partner API documentation.

### Example
```csharp
using System;
using System.Diagnostics;
using Amazon.SellingPartnerAPIAA.Client.Api;
using Amazon.SellingPartnerAPIAA.Client.Client;
using Amazon.SellingPartnerAPIAA.Client.Model;

namespace Example
{
    public class CancelReportScheduleExample
    {
        public void main()
        {
            var apiInstance = new ReportsApi();
            var reportScheduleId = reportScheduleId_example;  // string | The identifier for the report schedule. This identifier is unique only in combination with a seller ID.

            try
            {
                apiInstance.CancelReportSchedule(reportScheduleId);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ReportsApi.CancelReportSchedule: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **reportScheduleId** | **string**| The identifier for the report schedule. This identifier is unique only in combination with a seller ID. | 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="createreport"></a>
# **CreateReport**
> CreateReportResponse CreateReport (CreateReportSpecification body)



Creates a report.  **Usage Plan:**  | Rate (requests per second) | Burst | | - -- - | - -- - | | 0.0167 | 15 |  For more information, see \"Usage Plans and Rate Limits\" in the Selling Partner API documentation.

### Example
```csharp
using System;
using System.Diagnostics;
using Amazon.SellingPartnerAPIAA.Client.Api;
using Amazon.SellingPartnerAPIAA.Client.Client;
using Amazon.SellingPartnerAPIAA.Client.Model;

namespace Example
{
    public class CreateReportExample
    {
        public void main()
        {
            var apiInstance = new ReportsApi();
            var body = new CreateReportSpecification(); // CreateReportSpecification | 

            try
            {
                CreateReportResponse result = apiInstance.CreateReport(body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ReportsApi.CreateReport: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**CreateReportSpecification**](CreateReportSpecification.md)|  | 

### Return type

[**CreateReportResponse**](CreateReportResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="createreportschedule"></a>
# **CreateReportSchedule**
> CreateReportScheduleResponse CreateReportSchedule (CreateReportScheduleSpecification body)



Creates a report schedule. If a report schedule with the same report type and marketplace IDs already exists, it will be cancelled and replaced with this one.  **Usage Plan:**  | Rate (requests per second) | Burst | | - -- - | - -- - | | 0.0222 | 10 |  For more information, see [Usage Plans and Rate Limits in the Selling Partner API](doc:usage-plans-and-rate-limits-in-the-sp-api) in the Selling Partner API documentation.

### Example
```csharp
using System;
using System.Diagnostics;
using Amazon.SellingPartnerAPIAA.Client.Api;
using Amazon.SellingPartnerAPIAA.Client.Client;
using Amazon.SellingPartnerAPIAA.Client.Model;

namespace Example
{
    public class CreateReportScheduleExample
    {
        public void main()
        {
            var apiInstance = new ReportsApi();
            var body = new CreateReportScheduleSpecification(); // CreateReportScheduleSpecification | 

            try
            {
                CreateReportScheduleResponse result = apiInstance.CreateReportSchedule(body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ReportsApi.CreateReportSchedule: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**CreateReportScheduleSpecification**](CreateReportScheduleSpecification.md)|  | 

### Return type

[**CreateReportScheduleResponse**](CreateReportScheduleResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getreport"></a>
# **GetReport**
> Report GetReport (string reportId)



Returns report details (including the reportDocumentId, if available) for the report that you specify.  **Usage Plan:**  | Rate (requests per second) | Burst | | - -- - | - -- - | | 2.0 | 15 |  For more information, see [Usage Plans and Rate Limits in the Selling Partner API](doc:usage-plans-and-rate-limits-in-the-sp-api) in the Selling Partner API documentation.

### Example
```csharp
using System;
using System.Diagnostics;
using Amazon.SellingPartnerAPIAA.Client.Api;
using Amazon.SellingPartnerAPIAA.Client.Client;
using Amazon.SellingPartnerAPIAA.Client.Model;

namespace Example
{
    public class GetReportExample
    {
        public void main()
        {
            var apiInstance = new ReportsApi();
            var reportId = reportId_example;  // string | The identifier for the report. This identifier is unique only in combination with a seller ID.

            try
            {
                Report result = apiInstance.GetReport(reportId);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ReportsApi.GetReport: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **reportId** | **string**| The identifier for the report. This identifier is unique only in combination with a seller ID. | 

### Return type

[**Report**](Report.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getreportdocument"></a>
# **GetReportDocument**
> ReportDocument GetReportDocument (string reportDocumentId)



Returns the information required for retrieving a report document's contents.  **Usage Plan:**  | Rate (requests per second) | Burst | | - -- - | - -- - | | 0.0167 | 15 |  For more information, see [Usage Plans and Rate Limits in the Selling Partner API](doc:usage-plans-and-rate-limits-in-the-sp-api)in the Selling Partner API documentation.

### Example
```csharp
using System;
using System.Diagnostics;
using Amazon.SellingPartnerAPIAA.Client.Api;
using Amazon.SellingPartnerAPIAA.Client.Client;
using Amazon.SellingPartnerAPIAA.Client.Model;

namespace Example
{
    public class GetReportDocumentExample
    {
        public void main()
        {
            var apiInstance = new ReportsApi();
            var reportDocumentId = reportDocumentId_example;  // string | The identifier for the report document.

            try
            {
                ReportDocument result = apiInstance.GetReportDocument(reportDocumentId);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ReportsApi.GetReportDocument: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **reportDocumentId** | **string**| The identifier for the report document. | 

### Return type

[**ReportDocument**](ReportDocument.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getreportschedule"></a>
# **GetReportSchedule**
> ReportSchedule GetReportSchedule (string reportScheduleId)



Returns report schedule details for the report schedule that you specify.  **Usage Plan:**  | Rate (requests per second) | Burst | | - -- - | - -- - | | 0.0222 | 10 |  For more information, see [Usage Plans and Rate Limits in the Selling Partner API](doc:usage-plans-and-rate-limits-in-the-sp-api) in the Selling Partner API documentation.

### Example
```csharp
using System;
using System.Diagnostics;
using Amazon.SellingPartnerAPIAA.Client.Api;
using Amazon.SellingPartnerAPIAA.Client.Client;
using Amazon.SellingPartnerAPIAA.Client.Model;

namespace Example
{
    public class GetReportScheduleExample
    {
        public void main()
        {
            var apiInstance = new ReportsApi();
            var reportScheduleId = reportScheduleId_example;  // string | The identifier for the report schedule. This identifier is unique only in combination with a seller ID.

            try
            {
                ReportSchedule result = apiInstance.GetReportSchedule(reportScheduleId);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ReportsApi.GetReportSchedule: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **reportScheduleId** | **string**| The identifier for the report schedule. This identifier is unique only in combination with a seller ID. | 

### Return type

[**ReportSchedule**](ReportSchedule.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getreportschedules"></a>
# **GetReportSchedules**
> ReportScheduleList GetReportSchedules (List<string> reportTypes)



Returns report schedule details that match the filters that you specify.  **Usage Plan:**  | Rate (requests per second) | Burst | | - -- - | - -- - | | 0.0222 | 10 |  For more information, see [Usage Plans and Rate Limits in the Selling Partner API](doc:usage-plans-and-rate-limits-in-the-sp-api) in the Selling Partner API documentation.

### Example
```csharp
using System;
using System.Diagnostics;
using Amazon.SellingPartnerAPIAA.Client.Api;
using Amazon.SellingPartnerAPIAA.Client.Client;
using Amazon.SellingPartnerAPIAA.Client.Model;

namespace Example
{
    public class GetReportSchedulesExample
    {
        public void main()
        {
            var apiInstance = new ReportsApi();
            var reportTypes = new List<string>(); // List<string> | A list of report types used to filter report schedules.

            try
            {
                ReportScheduleList result = apiInstance.GetReportSchedules(reportTypes);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ReportsApi.GetReportSchedules: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **reportTypes** | [**List&lt;string&gt;**](string.md)| A list of report types used to filter report schedules. | 

### Return type

[**ReportScheduleList**](ReportScheduleList.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getreports"></a>
# **GetReports**
> GetReportsResponse GetReports (List<string> reportTypes = null, List<string> processingStatuses = null, List<string> marketplaceIds = null, int? pageSize = null, DateTime? createdSince = null, DateTime? createdUntil = null, string nextToken = null)



Returns report details for the reports that match the filters that you specify.  **Usage Plan:**  | Rate (requests per second) | Burst | | - -- - | - -- - | | 0.0222 | 10 |  For more information, see \"Usage Plans and Rate Limits\" in the Selling Partner API documentation.

### Example
```csharp
using System;
using System.Diagnostics;
using Amazon.SellingPartnerAPIAA.Client.Api;
using Amazon.SellingPartnerAPIAA.Client.Client;
using Amazon.SellingPartnerAPIAA.Client.Model;

namespace Example
{
    public class GetReportsExample
    {
        public void main()
        {
            var apiInstance = new ReportsApi();
            var reportTypes = new List<string>(); // List<string> | A list of report types used to filter reports. When reportTypes is provided, the other filter parameters (processingStatuses, marketplaceIds, createdSince, createdUntil) and pageSize may also be provided. Either reportTypes or nextToken is required. (optional) 
            var processingStatuses = processingStatuses_example;  // List<string> | A list of processing statuses used to filter reports. (optional) 
            var marketplaceIds = new List<string>(); // List<string> | A list of marketplace identifiers used to filter reports. The reports returned will match at least one of the marketplaces that you specify. (optional) 
            var pageSize = 56;  // int? | The maximum number of reports to return in a single call. (optional)  (default to 10)
            var createdSince = 2013-10-20T19:20:30+01:00;  // DateTime? | The earliest report creation date and time for reports to include in the response, in ISO 8601 date time format. The default is 90 days ago. Reports are retained for a maximum of 90 days. (optional) 
            var createdUntil = 2013-10-20T19:20:30+01:00;  // DateTime? | The latest report creation date and time for reports to include in the response, in ISO 8601 date time format. The default is now. (optional) 
            var nextToken = nextToken_example;  // string | A string token returned in the response to your previous request. nextToken is returned when the number of results exceeds the specified pageSize value. To get the next page of results, call the getReports operation and include this token as the only parameter. Specifying nextToken with any other parameters will cause the request to fail. (optional) 

            try
            {
                GetReportsResponse result = apiInstance.GetReports(reportTypes, processingStatuses, marketplaceIds, pageSize, createdSince, createdUntil, nextToken);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ReportsApi.GetReports: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **reportTypes** | [**List&lt;string&gt;**](string.md)| A list of report types used to filter reports. When reportTypes is provided, the other filter parameters (processingStatuses, marketplaceIds, createdSince, createdUntil) and pageSize may also be provided. Either reportTypes or nextToken is required. | [optional] 
 **processingStatuses** | **List&lt;string&gt;**| A list of processing statuses used to filter reports. | [optional] 
 **marketplaceIds** | [**List&lt;string&gt;**](string.md)| A list of marketplace identifiers used to filter reports. The reports returned will match at least one of the marketplaces that you specify. | [optional] 
 **pageSize** | **int?**| The maximum number of reports to return in a single call. | [optional] [default to 10]
 **createdSince** | **DateTime?**| The earliest report creation date and time for reports to include in the response, in ISO 8601 date time format. The default is 90 days ago. Reports are retained for a maximum of 90 days. | [optional] 
 **createdUntil** | **DateTime?**| The latest report creation date and time for reports to include in the response, in ISO 8601 date time format. The default is now. | [optional] 
 **nextToken** | **string**| A string token returned in the response to your previous request. nextToken is returned when the number of results exceeds the specified pageSize value. To get the next page of results, call the getReports operation and include this token as the only parameter. Specifying nextToken with any other parameters will cause the request to fail. | [optional] 

### Return type

[**GetReportsResponse**](GetReportsResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

