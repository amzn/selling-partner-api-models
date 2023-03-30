#!/bin/bash

cd ..
rm -rf generated
mkdir -p generated/spapi/src/main/java/com/amazon/sellingpartner
cp -a generate/JSON.java generated/spapi/src/main/java/com/amazon/sellingpartner/

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
          --additional-properties dateLibrary=java11

}
generate "aplus" "models/aplus-content-api-model/aplusContent_2020-11-01.json"
generate "authorization" "models/authorization-api-model/authorization.json"
generate "catalogv0" "models/catalog-items-api-model/catalogItemsV0.json"
generate "catalogv20" "models/catalog-items-api-model/catalogItems_2020-12-01.json"
generate "catalogv22" "models/catalog-items-api-model/catalogItems_2022-04-01.json"
generate "easyship" "models/easy-ship-model/easyShip_2022-03-23.json"
generate "fbainbound" "models/fba-inbound-eligibility-api-model/fbaInbound.json"
generate "fbainventory" "models/fba-inventory-api-model/fbaInventory.json"
generate "fbaSmallandLight" "models/fba-small-and-light-api-model/fbaSmallandLight.json"
generate "feeds" "models/feeds-api-model/feeds_2021-06-30.json"
generate "finances" "models/finances-api-model/financesV0.json"
generate "fulfillmentinbound" "models/fulfillment-inbound-api-model/fulfillmentInboundV0.json"
generate "fulfillmentoutbound" "models/fulfillment-outbound-api-model/fulfillmentOutbound_2020-07-01.json"
generate "listing" "models/listings-items-api-model/listingsItems_2021-08-01.json"
generate "listingrestrictions" "models/listings-restrictions-api-model/listingsRestrictions_2021-08-01.json"
generate "merchantfulfillment" "models/merchant-fulfillment-api-model/merchantFulfillmentV0.json"
generate "messaging" "models/messaging-api-model/messaging.json"
generate "notifications" "models/notifications-api-model/notifications.json"
generate "orders" "models/orders-api-model/ordersV0.json"
generate "productfees" "models/product-fees-api-model/productFeesV0.json"
generate "productpricing" "models/product-pricing-api-model/productPricingV0.json"
generate "definitions" "models/product-type-definitions-api-model/definitionsProductTypes_2020-09-01.json"
generate "reports" "models/reports-api-model/reports_2021-06-30.json"
generate "sales" "models/sales-api-model/sales.json"
generate "sellers" "models/sellers-api-model/sellers.json"
generate "services" "models/services-api-model/services.json"
generate "shipmentinvoicing" "models/shipment-invoicing-api-model/shipmentInvoicingV0.json"
generate "shipping" "models/shipping-api-model/shipping.json"
generate "solicitations" "models/solicitations-api-model/solicitations.json"
generate "tokens" "models/tokens-api-model/tokens_2021-03-01.json"
generate "uploads" "models/uploads-api-model/uploads_2020-11-01.json"
generate "dfinventory" "models/vendor-direct-fulfillment-inventory-api-model/vendorDirectFulfillmentInventoryV1.json"
generate "dforders" "models/vendor-direct-fulfillment-orders-api-model/vendorDirectFulfillmentOrdersV1.json"
generate "dfpayments" "models/vendor-direct-fulfillment-payments-api-model/vendorDirectFulfillmentPaymentsV1.json"
generate "dfshipping" "models/vendor-direct-fulfillment-shipping-api-model/vendorDirectFulfillmentShippingV1.json"
generate "dftransactions" "models/vendor-direct-fulfillment-transactions-api-model/vendorDirectFulfillmentTransactionsV1.json"
generate "vendorinvoices" "models/vendor-invoices-api-model/vendorInvoices.json"
generate "vendororders" "models/vendor-orders-api-model/vendorOrders.json"
generate "vendorshipments" "models/vendor-shipments-api-model/vendorShipments.json"
generate "vendortransactionstatus" "models/vendor-transaction-status-api-model/vendorTransactionStatus.json"
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
#cd clients/sellingpartner-api-aa-java
#mvn clean package
#mvn install:install-file -Dfile=target/sellingpartnerapi-aa-java-1.0.3.jar -DgroupId=com.amazon.sellingpartnerapi -DartifactId=sellingpartnerapi-aa-java -Dversion=1.0.3 -Dpackaging=jar -DlocalRepositoryPath=/Users/levon/Projects/jazva/lib
#cd ../..

cp -r generate/pom.xml generated/spapi
cd generated/spapi
mvn clean package

# install into jazva/lib  check path
# fix path and run manually from root folder
cd ../..
#mvn install:install-file -Dfile=generated/spapi/target/selling-partner-api-1.0.11.jar -Dsources=generated/spapi/target/selling-partner-api-1.0.11-sources.jar -Djavadoc=generated/spapi/target/selling-partner-api-1.0.11-javadoc.jar -DgroupId=com.amazon.sellingpartnerapi -DartifactId=selling-partner-api -Dversion=1.0.10 -Dpackaging=jar -DlocalRepositoryPath=/Users/levon/Projects/jazva/lib
