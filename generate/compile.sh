#!/bin/bash

cd ..
rm -rf generated
mkdir -p generated/spapi/src/main/java/com/amazon/sellingpartner

basePackage="com.amazon.sellingpartner"
#models=(models/*/*)

generate () {
    java -jar generate/swagger-codegen-cli.jar generate \
          --input-spec $2 \
          --lang java \
          --template-dir clients/sellingpartner-api-aa-java/resources/swagger-codegen/templates \
          --output generated/spapi \
          --invoker-package "$basePackage" \
          --api-package "$basePackage.api.$1" \
          --model-package "$basePackage.model.$1" \
          --group-id "com.amazon" \
          --artifact-id "selling-partner-api" \
          --additional-properties dateLibrary=java8

}
generate "awd" "models/amazon-warehousing-and-distribution-model/awd_2024-05-09.json"
generate "aplus" "models/aplus-content-api-model/aplusContent_2020-11-01.json"
generate "applicaitons" "models/application-management-api-model/application_2023-11-30.json"
generate "catalogv0" "models/catalog-items-api-model/catalogItemsV0.json"
generate "catalogv20" "models/catalog-items-api-model/catalogItems_2020-12-01.json"
generate "catalogv22" "models/catalog-items-api-model/catalogItems_2022-04-01.json"
generate "queries" "models/data-kiosk-api-model/dataKiosk_2023-11-15.json"
generate "easyship" "models/easy-ship-model/easyShip_2022-03-23.json"
generate "fbainbound" "models/fba-inbound-eligibility-api-model/fbaInbound.json"
generate "fbainventory" "models/fba-inventory-api-model/fbaInventory.json"
generate "feeds" "models/feeds-api-model/feeds_2021-06-30.json"
generate "finances" "models/finances-api-model/finances_2024-06-19.json"
generate "financestransfers" "models/finances-api-model/transfers_2024-06-01.json"
generate "fulfillmentinbound" "models/fulfillment-inbound-api-model/fulfillmentInboundV0.json"
generate "fulfillmentinboundbeta" "models/fulfillment-inbound-api-model/fulfillmentInbound_2024-03-20.json"
generate "fulfillmentoutbound" "models/fulfillment-outbound-api-model/fulfillmentOutbound_2020-07-01.json"
generate "listing" "models/listings-items-api-model/listingsItems_2021-08-01.json"
generate "listingrestrictions" "models/listings-restrictions-api-model/listingsRestrictions_2021-08-01.json"
generate "merchantfulfillment" "models/merchant-fulfillment-api-model/merchantFulfillmentV0.json"
generate "messaging" "models/messaging-api-model/messaging.json"
generate "notifications" "models/notifications-api-model/notifications.json"
generate "orders" "models/orders-api-model/orders_2026-01-01.json"
generate "productfees" "models/product-fees-api-model/productFeesV0.json"
generate "productpricing" "models/product-pricing-api-model/productPricing_2022-05-01.json"
generate "definitions" "models/product-type-definitions-api-model/definitionsProductTypes_2020-09-01.json"
generate "sellingpartners" "models/replenishment-api-model/replenishment-2022-11-07.json"
generate "reports" "models/reports-api-model/reports_2021-06-30.json"
generate "sales" "models/sales-api-model/sales.json"
generate "sellers" "models/sellers-api-model/sellers.json"
generate "services" "models/services-api-model/services.json"
generate "shipmentinvoicing" "models/shipment-invoicing-api-model/shipmentInvoicingV0.json"
generate "shipping" "models/shipping-api-model/shippingV2.json"
generate "solicitations" "models/solicitations-api-model/solicitations.json"
generate "supplysources" "models/supply-sources-api-model/supplySources_2020-07-01.json"
generate "tokens" "models/tokens-api-model/tokens_2021-03-01.json"
generate "uploads" "models/uploads-api-model/uploads_2020-11-01.json"
generate "dfinventory" "models/vendor-direct-fulfillment-inventory-api-model/vendorDirectFulfillmentInventoryV1.json"
generate "dforders" "models/vendor-direct-fulfillment-orders-api-model/vendorDirectFulfillmentOrders_2021-12-28.json"
generate "dfpayments" "models/vendor-direct-fulfillment-payments-api-model/vendorDirectFulfillmentPaymentsV1.json"
generate "dfshipping" "models/vendor-direct-fulfillment-shipping-api-model/vendorDirectFulfillmentShipping_2021-12-28.json"
generate "dftransactions" "models/vendor-direct-fulfillment-transactions-api-model/vendorDirectFulfillmentTransactions_2021-12-28.json"
generate "dfsandbox" "models/vendor-direct-fulfillment-sandbox-test-data-api-model/vendorDirectFulfillmentSandboxData_2021-10-28.json"
generate "vendorinvoices" "models/vendor-invoices-api-model/vendorInvoices.json"
generate "vendororders" "models/vendor-orders-api-model/vendorOrders.json"
generate "vendorshipments" "models/vendor-shipments-api-model/vendorShipments.json"
generate "vendortransactionstatus" "models/vendor-transaction-status-api-model/vendorTransactionStatus.json"

# New APIs added in 2.0.0
generate "appintegrations" "models/application-integrations-api-model/appIntegrations-2024-04-01.json"
generate "customerfeedback" "models/customer-feedback-api-model/customerFeedback_2024-06-01.json"
generate "deliverybyamazon" "models/delivery-by-amazon/deliveryShipmentInvoiceV2022-07-01.json"
generate "externalfulfillmentinventory" "models/external-fulfillment/externalFulfillmentInventory_2024-09-11.json"
generate "externalfulfillmentreturns" "models/external-fulfillment/externalFulfillmentReturns_2024-09-11.json"
generate "externalfulfillmentshipments" "models/external-fulfillment/externalFulfillmentShipments_2024-09-11.json"
generate "invoices" "models/invoices-api-model/InvoicesApiModel_2024-06-19.json"
generate "sellerwallet" "models/seller-wallet-api-model/sellerWallet_2024-03-01.json"
generate "vehicles" "models/vehicles-api-model/vehicles_2024-11-01.json"
#
## For every model in the models/ directory, and all subdirectories:
#for model in "${models[@]}"
#do
#    # Generate a client library
#    # --input-spec $model 			:: use the current model file
#    # --lang java 				:: generate a Java library
#    # --template-dir .../ 			:: use Amazon's premade Java template files
#    # --output ../spapi 			:: put the generated library in ../spapi
#    # --invoker-package "..." 			:: put the generated code in the given package
#    # --api-package "..." 			:: put the generated api code in the given package
#    # --model-package "..." 			:: put the generated model code in the given package
#    # --group-id "..." 				:: package metadata
#    # --artifact-id "..." 			:: package metadata
#    # --additional-properties dateLibrary=java8 :: Use Java 8 date libraries
#done
#


# Build the AA (auth) library - must be done before the main API build
cd clients/sellingpartner-api-aa-java
mvn clean package -DskipTests
mvn install:install-file -Dfile=target/sellingpartnerapi-aa-java-3.0.1.jar -DgroupId=com.amazon.sellingpartnerapi -DartifactId=sellingpartnerapi-aa-java -Dversion=3.0.1 -Dpackaging=jar -DlocalRepositoryPath=/Users/levon/Projects/selling-partner-api-models/lib
mvn install:install-file -Dfile=target/sellingpartnerapi-aa-java-3.0.1.jar -DgroupId=com.amazon.sellingpartnerapi -DartifactId=sellingpartnerapi-aa-java -Dversion=3.0.1 -Dpackaging=jar -DlocalRepositoryPath=/Users/levon/Projects/jazva/lib
mvn install:install-file -Dfile=target/sellingpartnerapi-aa-java-3.0.1.jar -DgroupId=com.amazon.sellingpartnerapi -DartifactId=sellingpartnerapi-aa-java -Dversion=3.0.1 -Dpackaging=jar -DlocalRepositoryPath=/Users/levon/Projects/jazva8/lib
cd ../..

# Copy okhttp3-compatible utility files over swagger-codegen's okhttp2 versions
cp -a generate/JSON.java generated/spapi/src/main/java/com/amazon/sellingpartner/
cp -a generate/ProgressResponseBody.java generated/spapi/src/main/java/com/amazon/sellingpartner/
cp -a generate/ProgressRequestBody.java generated/spapi/src/main/java/com/amazon/sellingpartner/
cp -a generate/GzipRequestInterceptor.java generated/spapi/src/main/java/com/amazon/sellingpartner/
cp -a generate/HttpBasicAuth.java generated/spapi/src/main/java/com/amazon/sellingpartner/auth/
cp -r generate/pom.xml generated/spapi
cd generated/spapi
mvn clean package

# install into jazva/lib and jazva8/lib
cd ../..
mvn install:install-file -Dfile=generated/spapi/target/selling-partner-api-2.0.1.jar -Dsources=generated/spapi/target/selling-partner-api-2.0.1-sources.jar -Djavadoc=generated/spapi/target/selling-partner-api-2.0.1-javadoc.jar -DgroupId=com.amazon.sellingpartnerapi -DartifactId=selling-partner-api -Dversion=2.0.1 -Dpackaging=jar -DlocalRepositoryPath=/Users/levon/Projects/jazva/lib
mvn install:install-file -Dfile=generated/spapi/target/selling-partner-api-2.0.1.jar -Dsources=generated/spapi/target/selling-partner-api-2.0.1-sources.jar -Djavadoc=generated/spapi/target/selling-partner-api-2.0.1-javadoc.jar -DgroupId=com.amazon.sellingpartnerapi -DartifactId=selling-partner-api -Dversion=2.0.1 -Dpackaging=jar -DlocalRepositoryPath=/Users/levon/Projects/jazva8/lib
