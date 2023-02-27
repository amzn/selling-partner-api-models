using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Amazon.SellingPartnerAPIAA.Clients.Utils.ApiUrl;

namespace Amazon.SellingPartnerAPIAA.Clients.Utils
{
#pragma warning disable CS0618 // Type or member is obsolete
    public class RateLimit
    {
        public RateLimit() { }

        public RateLimit(string aPIPath, double rateLimitValue, int burstValue)
        {
            RateLimitValue = rateLimitValue;
            BurstValue = burstValue;
            APIPath = aPIPath;
        }

        public double RateLimitValue { get; set; }

        public int BurstValue { get; set; }

        public string APIPath { get; set; }
    }

    public class RateLimitDefs
    {

        public static RateLimit GetRateLimit(string apiPath)
        {
            return GetRateLimit(apiPath, null);
        }

        public static RateLimit GetRateLimit(string apiPath, List<Parameter> parameters = null)
        {
            if (parameters == null)
                parameters = new List<Parameter>();
            InitRateLimits(parameters);
            return RateLimits.FirstOrDefault(r => r.APIPath == apiPath);
        }

        private static void InitRateLimits(List<Parameter> parameters)
        {
            string identifier1 = "";
            if (parameters.Count > 0)
                identifier1 = parameters[0].Value.ToString() ?? "";
            RateLimits = new List<RateLimit>()
            {
                new RateLimit(OAuthUrls.TokenUrl, 1.0, 5),
                new RateLimit(OAuthUrls.RefreshTokenUrl, 1.0, 5),
                new RateLimit(CategoryApiUrls.GetCatalogItem(identifier1), 2.0, 2),
                new RateLimit(CategoryApiUrls.GetCatalogItem(), 2.0, 2),
                new RateLimit(CategoryApiUrls.ListCatalogCategories, 1.0, 40),
                new RateLimit(CategoryApiUrls.ListCatalogItems, 6.0, 40),
                new RateLimit(CategoryApiUrls.GetCatalogItem202012(identifier1), 2.0, 2),
                new RateLimit(CategoryApiUrls.GetCatalogItem202204(identifier1), 2.0, 2),
                new RateLimit(CategoryApiUrls.GetCatalogItem202012(), 2.0, 2),
                new RateLimit(CategoryApiUrls.GetCatalogItem202204(), 2.0, 2),
                new RateLimit(CategoryApiUrls.SearchCatalogItems, 2.0, 2),
                new RateLimit(CategoryApiUrls.SearchCatalogItems202204, 2.0, 2),
                //new RateLimit(1.0, 5, EasyShipApiUrls.CreateScheduledPackage(identifier1)),
                //new RateLimit(1.0, 5, EasyShipApiUrls.GetScheduledPackage(identifier1)),
                //new RateLimit(1.0, 5, EasyShipApiUrls.ListHandoverSlots(identifier1)),
                //new RateLimit(1.0, 5, EasyShipApiUrls.UpdateScheduledPackages(identifier1)),
                new RateLimit(FBAInboundEligibilityApiUrls.GetItemEligibilityPreview, 1.0, 1),
                new RateLimit(FBAInventoriesApiUrls.GetInventorySummaries, 2.0, 2),
                new RateLimit(FBASmallAndLightApiUrls.DeleteSmallAndLightEnrollmentBySellerSKU(identifier1), 2.0, 5),
                new RateLimit(FBASmallAndLightApiUrls.GetSmallAndLightEligibilityBySellerSKU(identifier1), 2.0, 10),
                new RateLimit(FBASmallAndLightApiUrls.GetSmallAndLightEnrollmentBySellerSKU(identifier1), 2.0, 10),
                new RateLimit(FBASmallAndLightApiUrls.GetSmallAndLightFeePreview, 1.0, 3),
                new RateLimit(FBASmallAndLightApiUrls.PutSmallAndLightEnrollmentBySellerSKU(identifier1), 2.0, 5),
                new RateLimit(FeedsApiUrls.CancelFeed(identifier1), 2.0, 15),
                new RateLimit(FeedsApiUrls.CancelFeed(), 2.0, 15),
                new RateLimit(FeedsApiUrls.CreateFeed, 0.5, 15),
                new RateLimit(FeedsApiUrls.CreateFeedDocument, 0.0083, 15),
                new RateLimit(FeedsApiUrls.GetFeed(identifier1), 2.0, 15),
                new RateLimit(FeedsApiUrls.GetFeed(), 2.0, 15),
                new RateLimit(FeedsApiUrls.GetFeedDocument(identifier1), 0.0222, 10),
                new RateLimit(FeedsApiUrls.GetFeedDocument(), 0.0222, 10),
                new RateLimit(FeedsApiUrls.GetFeeds, 0.0222, 10),
                new RateLimit(FinancialApiUrls.ListFinancialEventGroups, 0.5, 30),
                new RateLimit(FinancialApiUrls.ListFinancialEvents, 0.5, 30),
                new RateLimit(FinancialApiUrls.ListFinancialEventsByGroupId(identifier1), 0.5, 30),
                new RateLimit(FinancialApiUrls.ListFinancialEventsByOrderId(identifier1), 0.5, 30),
                new RateLimit(FulfillmentInboundApiUrls.ConfirmPreorder(identifier1), 2.0, 30),
                new RateLimit(FulfillmentInboundApiUrls.ConfirmTransport(identifier1), 2.0, 30),
                new RateLimit(FulfillmentInboundApiUrls.CreateInboundShipment(identifier1), 2.0, 30),
                new RateLimit(FulfillmentInboundApiUrls.CreateInboundShipmentPlan, 2.0, 30),
                new RateLimit(FulfillmentInboundApiUrls.EstimateTransport(identifier1), 2.0, 30),
                new RateLimit(FulfillmentInboundApiUrls.GetBillOfLading(identifier1), 2.0, 30),
                new RateLimit(FulfillmentInboundApiUrls.GetInboundGuidance, 2.0, 30),
                new RateLimit(FulfillmentInboundApiUrls.GetLabels(identifier1), 2.0, 30),
                new RateLimit(FulfillmentInboundApiUrls.GetPreorderInfo(identifier1), 2.0, 30),
                new RateLimit(FulfillmentInboundApiUrls.GetPrepInstructions, 2.0, 30),
                new RateLimit(FulfillmentInboundApiUrls.GetShipmentItems, 2.0, 30),
                new RateLimit(FulfillmentInboundApiUrls.GetShipmentItemsByShipmentId(identifier1), 2.0, 30),
                new RateLimit(FulfillmentInboundApiUrls.GetShipments, 2.0, 30),
                new RateLimit(FulfillmentInboundApiUrls.GetTransportDetails(identifier1), 2.0, 30),
                new RateLimit(FulfillmentInboundApiUrls.PutTransportDetails(identifier1), 2.0, 30),
                new RateLimit(FulfillmentInboundApiUrls.UpdateInboundShipment(identifier1), 2.0, 30),
                new RateLimit(FulfillmentInboundApiUrls.VoidTransport(identifier1), 2.0, 30),
                new RateLimit(FulfillmentOutboundApiUrls.CancelFulfillmentOrder(identifier1), 2.0, 30),
                new RateLimit(FulfillmentOutboundApiUrls.CreateFulfillmentOrder, 2.0, 30),
                new RateLimit(FulfillmentOutboundApiUrls.CreateFulfillmentReturn(identifier1), 2.0, 30),
                new RateLimit(FulfillmentOutboundApiUrls.GetFeatureInventory(identifier1), 2.0, 30),
                new RateLimit(FulfillmentOutboundApiUrls.GetFeatures, 2.0, 30),
                new RateLimit(FulfillmentOutboundApiUrls.GetFeatureSKU(
                    parameters.FirstOrDefault(p => p.Name == "featureName")?.Value.ToString() ?? "", 
                    parameters.FirstOrDefault(p => p.Name == "sellerSku")?.Value.ToString() ?? ""), 2.0, 30),
                new RateLimit(FulfillmentOutboundApiUrls.GetFulfillmentOrder(identifier1), 2.0, 30),
                new RateLimit(FulfillmentOutboundApiUrls.GetFulfillmentPreview, 2.0, 30),
                new RateLimit(FulfillmentOutboundApiUrls.GetPackageTrackingDetails, 2.0, 30),
                new RateLimit(FulfillmentOutboundApiUrls.ListAllFulfillmentOrders, 2.0, 30),
                new RateLimit(FulfillmentOutboundApiUrls.ListReturnReasonCodes, 2.0, 30),
                new RateLimit(FulfillmentOutboundApiUrls.UpdateFulfillmentOrder(identifier1), 2.0, 30),
                new RateLimit(ListingsApiUrls.DeleteListingItem(
                    parameters.FirstOrDefault(p => p.Name == "seller")?.Value.ToString() ?? "",
                    parameters.FirstOrDefault(p => p.Name == "sku")?.Value.ToString() ?? ""), 5.0, 10),
                new RateLimit(ListingsApiUrls.GetListingItem(
                    parameters.FirstOrDefault(p => p.Name == "seller")?.Value.ToString() ?? "",
                    parameters.FirstOrDefault(p => p.Name == "sku")?.Value.ToString() ?? ""), 5.0, 10),
                new RateLimit(ListingsApiUrls.PatchListingItem(
                    parameters.FirstOrDefault(p => p.Name == "seller")?.Value.ToString() ?? "",
                    parameters.FirstOrDefault(p => p.Name == "sku")?.Value.ToString() ?? ""), 5.0, 10),
                new RateLimit(ListingsApiUrls.PutListingItem(
                    parameters.FirstOrDefault(p => p.Name == "seller")?.Value.ToString() ?? "",
                    parameters.FirstOrDefault(p => p.Name == "sku")?.Value.ToString() ?? ""), 5.0, 10),
                new RateLimit(MerchantFulfillmentApiUrls.CancelShipment(identifier1), 1.0, 1),
                new RateLimit(MerchantFulfillmentApiUrls.CancelShipmentOld(identifier1), 1.0, 1),
                new RateLimit(MerchantFulfillmentApiUrls.CreateShipment, 1.0, 1),
                new RateLimit(MerchantFulfillmentApiUrls.GetAdditionalSellerInputs, 1.0, 1),
                new RateLimit(MerchantFulfillmentApiUrls.GetAdditionalSellerInputsOld, 1.0, 1),
                new RateLimit(MerchantFulfillmentApiUrls.GetEligibleShipmentServices, 5.0, 10),
                new RateLimit(MerchantFulfillmentApiUrls.GetEligibleShipmentServicesOld, 1.0, 1),
                new RateLimit(MerchantFulfillmentApiUrls.GetShipment(identifier1), 1.0, 1),
                new RateLimit(MessagingApiUrls.ConfirmCustomizationDetails(identifier1), 1.0, 5),
                new RateLimit(MessagingApiUrls.CreateAmazonMotors(identifier1), 1.0, 5),
                new RateLimit(MessagingApiUrls.CreateConfirmDeliveryDetails(identifier1), 1.0, 5),
                new RateLimit(MessagingApiUrls.CreateConfirmOrderDetails(identifier1), 1.0, 5),
                new RateLimit(MessagingApiUrls.CreateConfirmServiceDetails(identifier1), 1.0, 5),
                new RateLimit(MessagingApiUrls.CreateDigitalAccessKey(identifier1), 1.0, 5),
                new RateLimit(MessagingApiUrls.CreateLegalDisclosure(identifier1), 1.0, 5),
                new RateLimit(MessagingApiUrls.CreateNegativeFeedbackRemoval(identifier1), 1.0, 5),
                new RateLimit(MessagingApiUrls.CreateUnexpectedProblem(identifier1), 1.0, 5),
                new RateLimit(MessagingApiUrls.CreateWarranty(identifier1), 1.0, 5),
                new RateLimit(MessagingApiUrls.GetAttributes(identifier1), 1.0, 5),
                new RateLimit(MessagingApiUrls.GetMessagingActionsForOrder(identifier1), 1.0, 5),
                new RateLimit(NotificationApiUrls.CreateDestination, 1.0, 5),
                new RateLimit(NotificationApiUrls.CreateSubscription(identifier1), 1.0, 5),
                new RateLimit(NotificationApiUrls.CreateSubscription(), 1.0, 5),
                new RateLimit(NotificationApiUrls.DeleteDestination(identifier1), 1.0, 5),
                new RateLimit(NotificationApiUrls.DeleteDestination(), 1.0, 5),
                new RateLimit(NotificationApiUrls.DeleteSubscriptionById(
                    parameters.FirstOrDefault(p => p.Name == "notificationType")?.Value.ToString() ?? "",
                    parameters.FirstOrDefault(p => p.Name == "subscriptionId")?.Value.ToString() ?? ""), 1.0, 5),
                new RateLimit(NotificationApiUrls.DeleteSubscriptionById(), 1.0, 5),
                new RateLimit(NotificationApiUrls.GetDestination(identifier1), 1.0, 5),
                new RateLimit(NotificationApiUrls.GetDestination(), 1.0, 5),
                new RateLimit(NotificationApiUrls.GetDestinations, 1.0, 5),
                new RateLimit(NotificationApiUrls.GetSubscription(identifier1), 1.0, 5),
                new RateLimit(NotificationApiUrls.GetSubscription(), 1.0, 5),
                new RateLimit(NotificationApiUrls.GetSubscriptionById(
                    parameters.FirstOrDefault(p => p.Name == "notificationType")?.Value.ToString() ?? "",
                    parameters.FirstOrDefault(p => p.Name == "subscriptionId")?.Value.ToString() ?? ""), 1.0, 5),
                new RateLimit(NotificationApiUrls.GetSubscriptionById(), 1.0, 5),
                new RateLimit(OrdersApiUrls.Orders, 0.0167, 20),
                new RateLimit(OrdersApiUrls.Order(identifier1), 0.0167, 20),
                new RateLimit(OrdersApiUrls.OrderBuyerInfo(identifier1), 0.0167, 20),
                new RateLimit(OrdersApiUrls.OrderShipmentInfo(identifier1), 0.0167, 20),
                new RateLimit(OrdersApiUrls.OrderItems(identifier1), 0.0167, 20),
                new RateLimit(OrdersApiUrls.OrderItemsBuyerInfo(identifier1), 0.0167, 20),
                new RateLimit(OrdersApiUrls.UpdateShipmentStatus(identifier1), 0.0167, 20),
                new RateLimit(OrdersApiUrls.Order(), 0.0167, 20),
                new RateLimit(OrdersApiUrls.OrderBuyerInfo(), 0.0167, 20),
                new RateLimit(OrdersApiUrls.OrderShipmentInfo(), 0.0167, 20),
                new RateLimit(OrdersApiUrls.OrderItems(), 0.0167, 20),
                new RateLimit(OrdersApiUrls.OrderItemsBuyerInfo(), 0.0167, 20),
                new RateLimit(OrdersApiUrls.UpdateShipmentStatus(), 0.0167, 20),
                new RateLimit(ProductFeesApiUrls.GetMyFeesEstimate, 0.5, 1),
                new RateLimit(ProductFeesApiUrls.GetMyFeesEstimateForASIN(identifier1), 1, 2),
                new RateLimit(ProductFeesApiUrls.GetMyFeesEstimateForSKU(identifier1), 1, 2),
                new RateLimit(ProductPricingApiUrls.GetCompetitivePricing, 0.5, 1),
                new RateLimit(ProductPricingApiUrls.GetItemOffers(identifier1), 0.5, 1),
                //new RateLimit(ProductPricingApiUrls.GetItemOffersBatch, 0.5, 1),
                new RateLimit(ProductPricingApiUrls.GetListingOffers(identifier1), 1, 2),
                //new RateLimit(ProductPricingApiUrls.GetListingOffersBatch, 0.5, 1),
                new RateLimit(ProductPricingApiUrls.GetPricing, 0.5, 1),
                new RateLimit(ProductTypeDefinitionsApi.GetDefinitionsProductType(identifier1), 5.0, 10),
                new RateLimit(ProductTypeDefinitionsApi.SearchDefinitionsProductTypes, 5.0, 10),
                new RateLimit(ReportApiUrls.CancelReport(identifier1), 0.0222, 10),
                new RateLimit(ReportApiUrls.CancelReport(), 0.0222, 10),
                new RateLimit(ReportApiUrls.CancelReportSchedule(identifier1), 0.0222, 10),
                new RateLimit(ReportApiUrls.CancelReportSchedule(), 0.0222, 10),
                new RateLimit(ReportApiUrls.CreateReport, 0.0167, 15),
                new RateLimit(ReportApiUrls.CreateReportSchedule, 0.0222, 10),
                new RateLimit(ReportApiUrls.GetReport(identifier1), 2.0, 15),
                new RateLimit(ReportApiUrls.GetReport(), 2.0, 15),
                new RateLimit(ReportApiUrls.GetReportDocument(identifier1), 0.0167, 15),
                new RateLimit(ReportApiUrls.GetReportDocument(), 0.0167, 15),
                new RateLimit(ReportApiUrls.GetReports, 0.0222, 10),
                new RateLimit(ReportApiUrls.GetReportSchedule(identifier1), 0.0222, 10),
                new RateLimit(ReportApiUrls.GetReportSchedule(), 0.0222, 10),
                new RateLimit(ReportApiUrls.GetReportSchedules, 0.0222, 10),
                new RateLimit(RestrictionsApiUrls.GetListingsRestrictions, 5.0, 10),
                new RateLimit(SalesApiUrls.GetOrderMetrics, 0.5, 15),
                new RateLimit(SellersApiUrls.GetMarketplaceParticipations, 0.016, 15),
                new RateLimit(ShipmentInvoicingApiUrls.GetInvoiceStatus(identifier1), 1.133, 25),
                new RateLimit(ShipmentInvoicingApiUrls.GetShipmentDetails(identifier1), 1.133, 25),
                new RateLimit(ShipmentInvoicingApiUrls.SubmitInvoice(identifier1), 1.133, 25),
                new RateLimit(ShippingApiUrls.CancelShipment(identifier1), 5.0, 15),
                new RateLimit(ShippingApiUrls.CreateShipment, 5.0, 15),
                new RateLimit(ShippingApiUrls.GetAccount, 5.0, 15),
                new RateLimit(ShippingApiUrls.GetRates, 5.0, 15),
                new RateLimit(ShippingApiUrls.GetShipment(identifier1), 5.0, 15),
                new RateLimit(ShippingApiUrls.GetTrackingInformation(identifier1), 1.0, 1),
                new RateLimit(ShippingApiUrls.PurchaseLabels(identifier1), 5.0, 15),
                new RateLimit(ShippingApiUrls.PurchaseShipment, 5.0, 15),
                new RateLimit(ShippingApiUrls.RetrieveShippingLabel(
                    parameters.FirstOrDefault(p => p.Name == "shipmentId")?.Value.ToString() ?? "",
                    parameters.FirstOrDefault(p => p.Name == "trackingId")?.Value.ToString() ?? ""), 5.0, 15),
                new RateLimit(ShippingApiV2Urls.CancelShipment(identifier1), 80.0, 100),
                //new RateLimit(ShippingApiV2Urls.DirectPurchaseShipment(identifier1, identifier2), 80.0, 100),
                //new RateLimit(ShippingApiV2Urls.GetAdditionalInputs(identifier1, identifier2), 80.0, 100),
                new RateLimit(ShippingApiV2Urls.GetRates, 80.0, 100),
                //new RateLimit(ShippingApiV2Urls.GetShipmentDocument(identifier1, identifier2), 80.0, 100),
                new RateLimit(ShippingApiV2Urls.GetTracking(
                    parameters.FirstOrDefault(p => p.Name == "carrierId")?.Value.ToString() ?? "",
                    parameters.FirstOrDefault(p => p.Name == "trackingId")?.Value.ToString() ?? ""), 80.0, 100),
                new RateLimit(ShippingApiV2Urls.PurchaseShipment, 80.0, 100),
                new RateLimit(SolicitationsApiUrls.CreateProductReviewAndSellerFeedbackSolicitation(identifier1), 1.0, 5),
                new RateLimit(SolicitationsApiUrls.GetSolicitationActionsForOrder(identifier1), 1.0, 5),
                new RateLimit(TokenApiUrls.CreateRestrictedDataToken, 1.0, 10),
                new RateLimit(UploadApiUrls.CreateUploadDestinationForResource(identifier1), 10, 10),
                new RateLimit(VendorDirectFulfillmentOrdersApiUrls.GetOrder(identifier1), 10.0, 10),
                new RateLimit(VendorDirectFulfillmentOrdersApiUrls.GetOrders, 10.0, 10),
                new RateLimit(VendorDirectFulfillmentOrdersApiUrls.SubmitAcknowledgement, 10.0, 10),
                new RateLimit(VendorOrdersApiUrls.GetPurchaseOrder(identifier1), 10.0, 10),
                new RateLimit(VendorOrdersApiUrls.GetPurchaseOrders, 10.0, 10),
                new RateLimit(VendorOrdersApiUrls.GetPurchaseOrdersStatus, 10.0, 10),
                new RateLimit(VendorOrdersApiUrls.SubmitAcknowledgement, 10.0, 10),
            };
        }

        public static List<RateLimit> RateLimits { get; set; } 
    }
#pragma warning restore CS0618 // Type or member is obsolete
}
