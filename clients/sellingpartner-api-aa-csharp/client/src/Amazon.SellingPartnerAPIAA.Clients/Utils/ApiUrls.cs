using System;

namespace Amazon.SellingPartnerAPIAA.Clients.Utils
{
    public class ApiUrl
    {
        public class OAuthUrls
        {
            public static string TokenUrl => $"identity/v1/oauth2/token";
            public static string RefreshTokenUrl => $"identity/v1/oauth2/token";
        }
        public class TaxonomyApiUrls
        {
            private readonly static string _resourceBaseUrl = "/commerce/taxonomy/v1_beta";
            public static string getDefaultCategoryTreeIdUrl => $"{_resourceBaseUrl}/get_default_category_tree_id";
            public static string CategoryTreeUrl => $"{_resourceBaseUrl}/category_tree";
            public static string GetItemAspectsForCategory => $"/get_item_aspects_for_category";
        }
        public class FBAInboundEligibilityApiUrls
        {
            private readonly static string _resourceBaseUrl = "/fba/inbound/v1";
            public static string GetItemEligibilityPreview => $"{_resourceBaseUrl}/eligibility/itemPreview";
        }
        public class FulfillmentOutboundApiUrls
        {
            private readonly static string _resourceBaseUrl = "/fba/outbound/2020-07-01";
            public static string GetFulfillmentPreview => $"{_resourceBaseUrl}/fulfillmentOrders/preview";
            public static string ListAllFulfillmentOrders => $"{_resourceBaseUrl}/fulfillmentOrders";
            public static string CreateFulfillmentOrder => $"{_resourceBaseUrl}/fulfillmentOrders";
            public static string GetPackageTrackingDetails => $"{_resourceBaseUrl}/tracking";
            public static string ListReturnReasonCodes => $"{_resourceBaseUrl}/returnReasonCodes";
            public static string CreateFulfillmentReturn(string sellerFulfillmentOrderId) => $"{_resourceBaseUrl}/fulfillmentOrders/{sellerFulfillmentOrderId}/return";
            public static string GetFulfillmentOrder(string sellerFulfillmentOrderId) => $"{_resourceBaseUrl}/fulfillmentOrders/{sellerFulfillmentOrderId}";
            public static string UpdateFulfillmentOrder(string sellerFulfillmentOrderId) => $"{_resourceBaseUrl}/fulfillmentOrders/{sellerFulfillmentOrderId}";
            public static string CancelFulfillmentOrder(string sellerFulfillmentOrderId) => $"{_resourceBaseUrl}/fulfillmentOrders/{sellerFulfillmentOrderId}/cancel";
            public static string GetFeatures => $"{_resourceBaseUrl}/features";
            public static string GetFeatureInventory(string featureName) => $"{_resourceBaseUrl}/features/inventory/{featureName}";
            public static string GetFeatureSKU(string featureName, string sellerSku) => $"{_resourceBaseUrl}/features/inventory/{featureName}/{sellerSku}";
        }
        public class FulfillmentInboundApiUrls
        {
            private readonly static string _resourceBaseUrl = "/fba/inbound/v0";
            public static string GetInboundGuidance => $"{_resourceBaseUrl}/itemsGuidance";
            public static string CreateInboundShipmentPlan => $"{_resourceBaseUrl}/plans";
            public static string UpdateInboundShipment(string shipmentId) => $"{_resourceBaseUrl}/shipments/{shipmentId}";
            public static string CreateInboundShipment(string shipmentId) => $"{_resourceBaseUrl}/shipments/{shipmentId}";
            public static string GetPreorderInfo(string shipmentId) => $"{_resourceBaseUrl}/shipments/{shipmentId}/preorder";
            public static string ConfirmPreorder(string shipmentId) => $"{_resourceBaseUrl}/shipments/{shipmentId}/preorder/confirm";
            public static string GetPrepInstructions => $"{_resourceBaseUrl}/prepInstructions";
            public static string GetTransportDetails(string shipmentId) => $"{_resourceBaseUrl}/shipments/{shipmentId}/transport";
            public static string PutTransportDetails(string shipmentId) => $"{_resourceBaseUrl}/shipments/{shipmentId}/transport";
            public static string VoidTransport(string shipmentId) => $"{_resourceBaseUrl}/shipments/{shipmentId}/transport/void";
            public static string EstimateTransport(string shipmentId) => $"{_resourceBaseUrl}/shipments/{shipmentId}/transport/estimate";
            public static string ConfirmTransport(string shipmentId) => $"{_resourceBaseUrl}/shipments/{shipmentId}/transport/confirm";
            public static string GetLabels(string shipmentId) => $"{_resourceBaseUrl}/shipments/{shipmentId}/labels";
            public static string GetBillOfLading(string shipmentId) => $"{_resourceBaseUrl}/shipments/{shipmentId}/billOfLading";
            public static string GetShipments => $"{_resourceBaseUrl}/shipments";
            public static string GetShipmentItemsByShipmentId(string shipmentId) => $"{_resourceBaseUrl}/shipments/{shipmentId}/items";
            public static string GetShipmentItems => $"{_resourceBaseUrl}/shipmentItems";
        }
        public class ShippingApiUrls
        {
            private readonly static string _resourceBaseUrl = "/shipping/v1";
            public static string CreateShipment => $"{_resourceBaseUrl}/shipments";
            public static string PurchaseShipment => $"{_resourceBaseUrl}/purchaseShipment";
            public static string GetRates => $"{_resourceBaseUrl}/rates";
            public static string GetAccount => $"{_resourceBaseUrl}/account";
            public static string GetShipment(string shipmentId) => $"{_resourceBaseUrl}/shipments/{shipmentId}";
            public static string CancelShipment(string shipmentId) => $"{_resourceBaseUrl}/shipments/{shipmentId}/cancel";
            public static string PurchaseLabels(string shipmentId) => $"{_resourceBaseUrl}/shipments/{shipmentId}/purchaseLabels";
            public static string RetrieveShippingLabel(string shipmentId, string trackingId) => $"{_resourceBaseUrl}/shipments/{shipmentId}/containers/{trackingId}/label";
            public static string GetTrackingInformation(string trackingId) => $"{_resourceBaseUrl}/tracking/{trackingId}";

            public static string GetShipment() => $"{_resourceBaseUrl}/shipments/{{shipmentId}}";
            public static string CancelShipment() => $"{_resourceBaseUrl}/shipments/{{shipmentId}}/cancel";
            public static string PurchaseLabels() => $"{_resourceBaseUrl}/shipments/{{shipmentId}}/purchaseLabels";
            public static string RetrieveShippingLabel() => $"{_resourceBaseUrl}/shipments/{{shipmentId}}/containers/{{trackingId}}/label";
            public static string GetTrackingInformation() => $"{_resourceBaseUrl}/tracking/{{trackingId}}";
        }

        public class ShippingApiV2Urls
        {
            private readonly static string _resourceBaseUrl = "/shipping/v2";
            public static string GetRates => $"{_resourceBaseUrl}/shipments/rates";
            public static string PurchaseShipment => $"{_resourceBaseUrl}/shipments";
            public static string GetTracking(string carrierId, string trackingId) => $"{_resourceBaseUrl}/tracking?carrierId={carrierId}&trackingId={trackingId}";
            public static string GetShipmentDocuments(string shipmentId, string packageClientReferenceId, string format) => $"{_resourceBaseUrl}/shipments/{shipmentId}/documents?packageClientReferenceId={packageClientReferenceId}&format={format}";
            public static string CancelShipment(string shipmentId) => $"{_resourceBaseUrl}/shipments/{shipmentId}/cancel";
        }
        public class MessagingApiUrls
        {
            private readonly static string _resourceBaseUrl = "/messaging/v1";

            public static string GetMessagingActionsForOrder(string amazonOrderId) => $"{_resourceBaseUrl}/orders/{amazonOrderId}";
            public static string ConfirmCustomizationDetails(string amazonOrderId) => $"{_resourceBaseUrl}/orders/{amazonOrderId}/messages/confirmCustomizationDetails";
            public static string CreateConfirmDeliveryDetails(string amazonOrderId) => $"{_resourceBaseUrl}/orders/{amazonOrderId}/messages/confirmDeliveryDetails";
            public static string CreateLegalDisclosure(string amazonOrderId) => $"{_resourceBaseUrl}/orders/{amazonOrderId}/messages/legalDisclosure";
            public static string CreateNegativeFeedbackRemoval(string amazonOrderId) => $"{_resourceBaseUrl}/orders/{amazonOrderId}/messages/negativeFeedbackRemoval";
            public static string CreateConfirmOrderDetails(string amazonOrderId) => $"{_resourceBaseUrl}/orders/{amazonOrderId}/messages/confirmOrderDetails";
            public static string CreateConfirmServiceDetails(string amazonOrderId) => $"{_resourceBaseUrl}/orders/{amazonOrderId}/messages/confirmServiceDetails";
            public static string CreateAmazonMotors(string amazonOrderId) => $"{_resourceBaseUrl}/orders/{amazonOrderId}/messages/amazonMotors";
            public static string CreateWarranty(string amazonOrderId) => $"{_resourceBaseUrl}/orders/{amazonOrderId}/messages/warranty";
            public static string GetAttributes(string amazonOrderId) => $"{_resourceBaseUrl}/orders/{amazonOrderId}/attributes";
            public static string CreateDigitalAccessKey(string amazonOrderId) => $"{_resourceBaseUrl}/orders/{amazonOrderId}/messages/digitalAccessKey";
            public static string CreateUnexpectedProblem(string amazonOrderId) => $"{_resourceBaseUrl}/orders/{amazonOrderId}/messages/unexpectedProblem";
        }
        public class EasyShip20220323
        {
            private readonly static string _resourceBaseUrl = "/easyShip/2022-03-23";
            public static string ListHandoverSlots => $"{_resourceBaseUrl}/timeSlot";
            public static string GetScheduledPackage => $"{_resourceBaseUrl}/package";
            public static string CreateScheduledPackage => $"{_resourceBaseUrl}/package";
            public static string UpdateScheduledPackages => $"{_resourceBaseUrl}/package";
        }
        public class FBASmallAndLightApiUrls
        {
            private readonly static string _resourceBaseUrl = "/fba/smallAndLight/v1";

            public static string GetSmallAndLightEnrollmentBySellerSKU(string sellerSKU) => $"{_resourceBaseUrl}/enrollments/{Uri.EscapeDataString(sellerSKU)}";
            public static string PutSmallAndLightEnrollmentBySellerSKU(string sellerSKU) => $"{_resourceBaseUrl}/enrollments/{Uri.EscapeDataString(sellerSKU)}";
            public static string DeleteSmallAndLightEnrollmentBySellerSKU(string sellerSKU) => $"{_resourceBaseUrl}/enrollments/{Uri.EscapeDataString(sellerSKU)}";
            public static string GetSmallAndLightEligibilityBySellerSKU(string sellerSKU) => $"{_resourceBaseUrl}/eligibilities/{Uri.EscapeDataString(sellerSKU)}";
            public static string GetSmallAndLightFeePreview => $"{_resourceBaseUrl}/feePreviews";
        }
        public class MerchantFulfillmentApiUrls
        {
            private readonly static string _resourceBaseUrl = "/mfn/v0";

            public static string GetEligibleShipmentServicesOld => $"{_resourceBaseUrl}/eligibleServices";
            public static string GetEligibleShipmentServices => $"{_resourceBaseUrl}/eligibleShippingServices";
            public static string CreateShipment => $"{_resourceBaseUrl}/shipments";
            public static string GetAdditionalSellerInputsOld => $"{_resourceBaseUrl}/sellerInputs";
            public static string GetAdditionalSellerInputs => $"{_resourceBaseUrl}/additionalSellerInputs";
            public static string GetShipment(string shipmentId) => $"{_resourceBaseUrl}/shipments/{shipmentId}";
            public static string GetShipment() => $"{_resourceBaseUrl}/shipments/{{shipmentId}}";
            public static string CancelShipment(string shipmentId) => $"{_resourceBaseUrl}/shipments/{shipmentId}";
            public static string CancelShipment() => $"{_resourceBaseUrl}/shipments/{{shipmentId}}";
            public static string CancelShipmentOld(string shipmentId) => $"{_resourceBaseUrl}/shipments/{shipmentId}/cancel";
            public static string CancelShipmentOld() => $"{_resourceBaseUrl}/shipments/{{shipmentId}}/cancel";
        }
        public class NotificationApiUrls
        {
            private readonly static string _resourceBaseUrl = "/notifications/v1";
            public static string GetSubscription(string notificationType) => $"{_resourceBaseUrl}/subscriptions/{notificationType}";
            public static string CreateSubscription(string notificationType) => $"{_resourceBaseUrl}/subscriptions/{notificationType}";
            public static string GetSubscriptionById(string notificationType, string subscriptionId) => $"{_resourceBaseUrl}/subscriptions/{notificationType}/{subscriptionId}";
            public static string DeleteSubscriptionById(string notificationType, string subscriptionId) => $"{_resourceBaseUrl}/subscriptions/{notificationType}/{subscriptionId}";
            public static string GetSubscription() => $"{_resourceBaseUrl}/subscriptions/{{notificationType}}";
            public static string CreateSubscription() => $"{_resourceBaseUrl}/subscriptions/{{notificationType}}";
            public static string GetSubscriptionById() => $"{_resourceBaseUrl}/subscriptions/{{notificationType}}/{{subscriptionId}}";
            public static string DeleteSubscriptionById() => $"{_resourceBaseUrl}/subscriptions/{{notificationType}}/{{subscriptionId}}";

            public static string GetDestinations => $"{_resourceBaseUrl}/destinations";
            public static string CreateDestination => $"{_resourceBaseUrl}/destinations";
            public static string GetDestination(string destinationId) => $"{_resourceBaseUrl}/destinations/{destinationId}";
            public static string GetDestination() => $"{_resourceBaseUrl}/destinations/{{destinationId}}";
            public static string DeleteDestination(string destinationId) => $"{_resourceBaseUrl}/destinations/{destinationId}";
            public static string DeleteDestination() => $"{_resourceBaseUrl}/destinations/{{destinationId}}";
        }

        public class SalesApiUrls
        {
            private readonly static string _resourceBaseUrl = "/sales/v1";
            public static string GetOrderMetrics => $"{_resourceBaseUrl}/orderMetrics";
        }
        public class RestrictionsApiUrls
        {
            private readonly static string _resourceBaseUrl = "/listings/2021-08-01";
            public static string GetListingsRestrictions => $"{_resourceBaseUrl}/restrictions";
        }
        public class AuthorizationsApiUrls
        {
            private readonly static string _resourceBaseUrl = "/authorization/v1";
            public static string GetAuthorizationCode => $"{_resourceBaseUrl}/authorizationCode";
        }
        public class SellersApiUrls
        {
            private readonly static string _resourceBaseUrl = "/sellers/v1";
            public static string GetMarketplaceParticipations => $"{_resourceBaseUrl}/marketplaceParticipations";
        }
        public class ProductPricingApiUrls
        {
            private readonly static string _resourceBaseUrl = "/products/pricing/v0";
            public static string GetPricing => $"{_resourceBaseUrl}/price";
            public static string GetCompetitivePricing => $"{_resourceBaseUrl}/competitivePrice";

            public static string GetListingOffersBySellerSku(string SellerSKU) => $"{_resourceBaseUrl}/listings/{Uri.EscapeDataString(SellerSKU)}/offers";
            public static string GetItemOffers(string Asin) => $"{_resourceBaseUrl}/items/{Asin}/offers";
            public static string GetListingOffers(string sellerSKU) => $"{_resourceBaseUrl}/listings/{Uri.EscapeDataString(sellerSKU)}/offers";

            public static string GetBatchItemOffers => $"/batches{_resourceBaseUrl}/itemOffers";

            public static string GetBatchListingOffers => $"/batches{_resourceBaseUrl}/listingOffers";

        }
        public class ProductTypeApiUrls
        {
            private readonly static string _resourceBaseUrl = "/definitions/2020-09-01";
            public static string SearchDefinitionsProductTypes => $"{_resourceBaseUrl}/productTypes";

            public static string GetDefinitionsProductType(string productType) => $"{_resourceBaseUrl}/productTypes/{productType}";
        }

        public class ReportApiUrls
        {
            private readonly static string _resourceBaseUrl = "/reports/2021-06-30";
            public static string CreateReport => $"{_resourceBaseUrl}/reports";
            public static string GetReports => $"{_resourceBaseUrl}/reports";
            public static string GetReport(string reportId) => $"{_resourceBaseUrl}/reports/{reportId}";
            public static string GetReport() => $"{_resourceBaseUrl}/reports/{{reportId}}";
            public static string CancelReport(string reportId) => $"{_resourceBaseUrl}/reports/{reportId}";
            public static string CancelReport() => $"{_resourceBaseUrl}/reports/{{reportId}}";
            public static string GetReportSchedules => $"{_resourceBaseUrl}/schedules";
            public static string CreateReportSchedule => $"{_resourceBaseUrl}/schedules";

            public static string GetReportSchedule(string reportScheduleId) => $"{_resourceBaseUrl}/schedules/{reportScheduleId}";
            public static string GetReportSchedule() => $"{_resourceBaseUrl}/schedules/{{reportScheduleId}}";
            public static string CancelReportSchedule(string reportScheduleId) => $"{_resourceBaseUrl}/schedules/{reportScheduleId}";
            public static string CancelReportSchedule() => $"{_resourceBaseUrl}/schedules/{{reportScheduleId}}";
            public static string GetReportDocument(string reportDocumentId) => $"{_resourceBaseUrl}/documents/{reportDocumentId}";
            public static string GetReportDocument() => $"{_resourceBaseUrl}/documents/{{reportDocumentId}}";
        }
        public class VendorDirectFulfillmentOrdersApiUrls
        {
            private readonly static string _resourceBaseUrl = "/vendor/directFulfillment/orders/v1";
            public static string GetOrders => $"{_resourceBaseUrl}/purchaseOrders";
            public static string SubmitAcknowledgement => $"{_resourceBaseUrl}/acknowledgements";
            public static string GetOrder(string purchaseOrderNumber) => $"{_resourceBaseUrl}/purchaseOrders/{purchaseOrderNumber}";
        }
        public class VendorOrdersApiUrls
        {
            private readonly static string _resourceBaseUrl = "/vendor/orders/v1";
            public static string GetPurchaseOrders => $"{_resourceBaseUrl}/purchaseOrders";
            public static string GetPurchaseOrder(string purchaseOrderNumber) => $"{_resourceBaseUrl}/purchaseOrders/{purchaseOrderNumber}";
            public static string SubmitAcknowledgement => $"{_resourceBaseUrl}/acknowledgements";
            public static string GetPurchaseOrdersStatus => $"{_resourceBaseUrl}/purchaseOrdersStatus";
        }
        public class UploadApiUrls
        {
            private readonly static string _resourceBaseUrl = "/uploads/2020-11-01";
            public static string CreateUploadDestinationForResource(string resource) => $"{_resourceBaseUrl}/uploadDestinations/{resource}";
        }
        public class InventoryApiUrls
        {
            private readonly static string _resourceBaseUrl = "/sell/inventory/v1";
            public static string bulkCreateOrReplaceInventoryItemUrl => $"{_resourceBaseUrl}/bulk_create_or_replace_inventory_item";
            public static string InventoryItemUrl => $"{_resourceBaseUrl}/inventory_item";
            public static string InventoryItemGroupUrl => $"{_resourceBaseUrl}/inventory_item_group";
            public static string Offer => $"{_resourceBaseUrl}/offer";
            public static string BulkCreateOffer => $"{_resourceBaseUrl}/bulk_create_offer";
            public static string BulkPublishOffer => $"{_resourceBaseUrl}/bulk_publish_offer";
            public static string PublishByInventoryItemGroup => $"{_resourceBaseUrl}/publish_by_inventory_item_group";
            public static string WithdrawByInventoryItemGroup => $"{_resourceBaseUrl}/withdraw_by_inventory_item_group";
            public static string Location => $"{_resourceBaseUrl}/location";
        }
        public class FinancialApiUrls
        {
            private readonly static string _resourceBaseUrl = "/finances/v0";
            public static string ListFinancialEventGroups => $"{_resourceBaseUrl}/financialEventGroups";
            public static string ListFinancialEventsByGroupId(string eventGroupId) => $"{_resourceBaseUrl}/financialEventGroups/{eventGroupId}/financialEvents";
            public static string ListFinancialEventsByOrderId(string orderId) => $"{_resourceBaseUrl}/orders/{orderId}/financialEvents";
            public static string ListFinancialEvents => $"{_resourceBaseUrl}/financialEvents";
        }

        public class AccountApiUrls
        {
            private readonly static string _resourceBaseUrl = "/sell/account/v1";
            public static string returnPolicy => $"{_resourceBaseUrl}/return_policy";
            public static string FulfillmentPolicy => $"{_resourceBaseUrl}/fulfillment_policy";
            public static string PaymentPolicy => $"{_resourceBaseUrl}/payment_policy";
            public static string Privilege => $"{_resourceBaseUrl}/privilege";
        }
        public class ShipmentInvoicingApiUrls
        {
            private readonly static string _resourceBaseUrl = "/fba/outbound/brazil/v0";
            public static string GetShipmentDetails(string shipmentId) => $"{_resourceBaseUrl}/shipments/{shipmentId}";
            public static string SubmitInvoice(string shipmentId) => $"{_resourceBaseUrl}/shipments/{shipmentId}/invoice";
            public static string GetInvoiceStatus(string shipmentId) => $"{_resourceBaseUrl}/shipments/{shipmentId}/invoice/status";
            public static string GetShipmentDetails() => $"{_resourceBaseUrl}/shipments/{{shipmentId}}";
            public static string SubmitInvoice() => $"{_resourceBaseUrl}/shipments/{{shipmentId}}/invoice";
            public static string GetInvoiceStatus() => $"{_resourceBaseUrl}/shipments/{{shipmentId}}/invoice/status";
        }
        public class ProductFeesApiUrls
        {
            private readonly static string _resourceBaseUrl = "/products/fees/v0";
            public static string GetMyFeesEstimateForSKU(string SellerSKU) => $"{_resourceBaseUrl}/listings/{Uri.EscapeDataString(SellerSKU)}/feesEstimate";
            public static string GetMyFeesEstimateForASIN(string Asin) => $"{_resourceBaseUrl}/items/{Asin}/feesEstimate";

            public static string GetMyFeesEstimateForSKU() => $"{_resourceBaseUrl}/listings/{{SellerSKU}}/feesEstimate";
            public static string GetMyFeesEstimateForASIN() => $"{_resourceBaseUrl}/items/{{Asin}}/feesEstimate";

            public static string GetMyFeesEstimate => $"{_resourceBaseUrl}/feesEstimate";
        }
        public class TokenApiUrls
        {
            private readonly static string _resourceBaseUrl = "/tokens/2021-03-01";
            public static string CreateRestrictedDataToken => $"{_resourceBaseUrl}/restrictedDataToken";
        }
        public class SolicitationsApiUrls
        {
            private readonly static string _resourceBaseUrl = "/solicitations/v1";
            public static string GetSolicitationActionsForOrder(string amazonOrderId) => $"{_resourceBaseUrl}/orders/{amazonOrderId}";
            public static string CreateProductReviewAndSellerFeedbackSolicitation(string amazonOrderId) => $"{_resourceBaseUrl}/orders/{amazonOrderId}/solicitations/productReviewAndSellerFeedback";
        }
        public class FeedsApiUrls
        {
            private readonly static string _resourceBaseUrl = "/feeds/2021-06-30";
            public static string GetFeeds => $"{_resourceBaseUrl}/feeds";
            public static string CreateFeed => $"{_resourceBaseUrl}/feeds";
            public static string CreateFeedDocument => $"{_resourceBaseUrl}/documents";
            public static string GetFeedDocument(string feedDocumentId) => $"{_resourceBaseUrl}/documents/{feedDocumentId}";
            public static string GetFeedDocument() => $"{_resourceBaseUrl}/documents/{{feedDocumentId}}";
            public static string GetFeed(string feedId) => $"{_resourceBaseUrl}/feeds/{feedId}";
            public static string GetFeed() => $"{_resourceBaseUrl}/feeds/{{feedId}}";
            public static string CancelFeed(string feedId) => $"{_resourceBaseUrl}/feeds/{feedId}";
            public static string CancelFeed() => $"{_resourceBaseUrl}/feeds/{{feedId}}";
        }
        public class FBAInventoriesApiUrls
        {
            private readonly static string _resourceBaseUrl = "/fba/inventory/v1";
            public static string GetInventorySummaries => $"{_resourceBaseUrl}/summaries";
        }

        public class OrdersApiUrls
        {
            private readonly static string _resourceBaseUrl = "/orders/v0";
            public static string Orders => $"{_resourceBaseUrl}/orders";
            public static string Order(string orderId) => $"{_resourceBaseUrl}/orders/{orderId}";
            public static string OrderItems(string orderId) => $"{_resourceBaseUrl}/orders/{orderId}/orderItems";
            public static string OrderBuyerInfo(string orderId) => $"{_resourceBaseUrl}/orders/{orderId}/buyerInfo";
            public static string OrderItemsBuyerInfo(string orderId) => $"{_resourceBaseUrl}/orders/{orderId}/orderItems/buyerInfo";
            public static string OrderShipmentInfo(string orderId) => $"{_resourceBaseUrl}/orders/{orderId}/address";
            public static string UpdateShipmentStatus(string orderId) => $"{_resourceBaseUrl}/orders/{orderId}/shipment";
            public static string Order() => $"{_resourceBaseUrl}/orders/{{orderId}}";
            public static string OrderItems() => $"{_resourceBaseUrl}/orders/{{orderId}}/orderItems";
            public static string OrderBuyerInfo() => $"{_resourceBaseUrl}/orders/{{orderId}}/buyerInfo";
            public static string OrderItemsBuyerInfo() => $"{_resourceBaseUrl}/orders/{{orderId}}/orderItems/buyerInfo";
            public static string OrderShipmentInfo() => $"{_resourceBaseUrl}/orders/{{orderId}}/address";
            public static string UpdateShipmentStatus() => $"{_resourceBaseUrl}/orders/{{orderId}}/shipment";
        }

        public class CategoryApiUrls
        {
            private readonly static string _resourceBaseUrl = "/catalog/v0";

            private readonly static string _202012resourceBaseUrl = "/catalog/2020-12-01";

            private readonly static string _202204resourceBaseUrl = "/catalog/2022-04-01";


            public static string ListCatalogItems => $"{_resourceBaseUrl}/items";
            public static string ListCatalogCategories => $"{_resourceBaseUrl}/categories";
            public static string GetCatalogItem(string asin) => $"{_resourceBaseUrl}/items/{asin}";
            public static string GetCatalogItem202012(string asin) => $"{_202012resourceBaseUrl}/items/{asin}";

            public static string GetCatalogItem() => $"{_resourceBaseUrl}/items/{{asin}}";
            public static string GetCatalogItem202012() => $"{_202012resourceBaseUrl}/items/{{asin}}";
            public static string SearchCatalogItems => $"{_202012resourceBaseUrl}/items";

            public static string SearchCatalogItems202204 => $"{_202204resourceBaseUrl}/items";
            public static string GetCatalogItem202204(string asin) => $"{_202204resourceBaseUrl}/items/{asin}";
            public static string GetCatalogItem202204() => $"{_202204resourceBaseUrl}/items/{{asin}}";
        }

        public class ListingsApiUrls
        {
            private readonly static string _resourceBaseUrl = "/listings/2021-08-01";

            // https://stackoverflow.com/questions/575440/url-encoding-using-c-sharp/21771206#21771206
            public static string GetListingItem(string seller, string sku) => $"{_resourceBaseUrl}/items/{seller}/{Uri.EscapeDataString(sku)}";

            public static string PutListingItem(string seller, string sku) => $"{_resourceBaseUrl}/items/{seller}/{Uri.EscapeDataString(sku)}";

            public static string DeleteListingItem(string seller, string sku) => $"{_resourceBaseUrl}/items/{seller}/{Uri.EscapeDataString(sku)}";

            public static string PatchListingItem(string seller, string sku) => $"{_resourceBaseUrl}/items/{seller}/{Uri.EscapeDataString(sku)}";
        }

        public class ListingsRestrictionsApi
        {
            private readonly static string _resourceBaseUrl = "/listings/2021-08-01";
            public static string GetListingsRestrictions() => $"{_resourceBaseUrl}/restrictions";
        }

        public class ProductTypeDefinitionsApi
        {
            private readonly static string _resourceBaseUrl = "/definitions/2020-09-01/productTypes";
            public static string GetDefinitionsProductType(string productType) => $"{_resourceBaseUrl}/{productType}";
            public static string SearchDefinitionsProductTypes => $"{_resourceBaseUrl}";
        }

    }

}
