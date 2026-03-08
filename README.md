# Selling Partner API - Java Client Library (Jazva Fork)

Fork of [amzn/selling-partner-api-models](https://github.com/amzn/selling-partner-api-models) with a custom build pipeline that generates a single Java JAR from all SP-API OpenAPI models.

## Repository Structure

```
selling-partner-api-models/
├── models/                     # OpenAPI/Swagger JSON model files (from upstream)
├── schemas/                    # JSON schemas for SP-API notifications
│   ├── notifications/          # Notification event schemas
│   ├── feeds/                  # Feed document schemas
│   ├── reports/                # Report document schemas
│   └── data-kiosk/             # Data Kiosk query schemas
├── clients/
│   └── sellingpartner-api-aa-java/   # Auth library (LWA + AWS SigV4)
│       ├── pom.xml                   # AA library Maven config (v3.0)
│       ├── src/com/amazon/SellingPartnerAPIAA/  # Auth source code
│       └── resources/swagger-codegen/templates/ # Mustache templates for codegen
│           ├── ApiClient.mustache    # Custom ApiClient (RDT support, SigV4 disabled)
│           ├── api.mustache          # API class template
│           ├── api_test.mustache     # API test template
│           ├── build.gradle.mustache # Gradle build template
│           └── StringUtil.mustache   # String utility template
├── generate/                   # Build pipeline (entirely custom, not from upstream)
│   ├── compile.sh              # Main build script - generates code + compiles JAR
│   ├── pom.xml                 # Maven config for generated code (v2.0.1)
│   ├── swagger-codegen-cli.jar # Swagger codegen binary
│   ├── JSON.java               # Custom JSON utility (replaces codegen's version)
│   ├── ProgressResponseBody.java   # okhttp3-compatible (replaces codegen's okhttp2)
│   ├── ProgressRequestBody.java    # okhttp3-compatible (replaces codegen's okhttp2)
│   ├── GzipRequestInterceptor.java # okhttp3-compatible gzip interceptor
│   └── HttpBasicAuth.java          # okhttp3-compatible auth
├── generated/                  # Build output (gitignored, created by compile.sh)
│   └── spapi/                  # Generated Java source + compiled JAR
├── lib/                        # Local Maven repository for AA library
├── gen/                        # Empty (unused)
└── ~/ (home dir symlink)       # Present but not relevant
```

## Git Remotes and Branching

- **origin**: `git@github.com:jazva/selling-partner-api-models.git` (our fork)
- **upstream**: `git@github.com:amzn/selling-partner-api-models.git` (Amazon's repo)
- **main branch**: `main`
- **Active branches**: `main`, `fbainbound2`, `newversion`

## Build Pipeline

### How It Works

The build system in `generate/` takes Amazon's raw OpenAPI JSON model files and produces two JARs:

1. **`sellingpartnerapi-aa-java-3.0.jar`** - Authentication library (LWA token management)
2. **`selling-partner-api-2.0.1.jar`** - All SP-API client code in a single JAR

### Build Steps (what `compile.sh` does)

1. Cleans `generated/` directory
2. Runs `swagger-codegen-cli.jar` for each API model, generating Java code into `generated/spapi/` with:
   - Custom mustache templates from `clients/sellingpartner-api-aa-java/resources/swagger-codegen/templates/`
   - Each API gets its own sub-package under `com.amazon.sellingpartner.api.<name>` and `com.amazon.sellingpartner.model.<name>`
3. Builds the AA (auth) library from `clients/sellingpartner-api-aa-java/`
4. Installs `sellingpartnerapi-aa-java-3.0.jar` to three local Maven repos:
   - `selling-partner-api-models/lib/`
   - `jazva/lib/`
   - `jazva8/lib/`
5. Copies okhttp3-compatible Java files over swagger-codegen's okhttp2 defaults
6. Compiles all generated code into `selling-partner-api-2.0.1.jar`
7. Installs the JAR (with sources + javadoc) to `jazva/lib/` and `jazva8/lib/`

### Build Instructions

```bash
cd generate
bash compile.sh
```

**Prerequisites**: Java 8+, Maven

### Key Build Configuration

| File | Purpose |
|------|---------|
| `generate/pom.xml` line 8 | JAR version (`2.0.1`) |
| `generate/compile.sh` lines 120-121 | Install paths and version for final JAR |
| `clients/sellingpartner-api-aa-java/pom.xml` line 9 | AA library version (`3.0`) |

### Dependencies (generate/pom.xml)

| Dependency | Version |
|------------|---------|
| okhttp3 | 4.12.0 |
| gson | 2.8.1 |
| gson-fire | 1.8.3 |
| guava | 31.1-jre |
| swagger-annotations | 1.6.9 |
| Java target | 1.8 |

### Dependencies (AA library pom.xml)

| Dependency | Version |
|------------|---------|
| okhttp3 | 4.12.0 |
| aws-java-sdk-signer | 1.12.777 |
| aws-java-sdk-sts | 1.12.777 |
| httpclient5 | 5.2.1 |
| lombok | 1.18.34 |
| gson | 2.11.0 |

## Our Enhancements vs Upstream

We maintain a small set of changes on top of Amazon's upstream repo. All are in `generate/`, `clients/`, and mustache templates — never in `models/`.

### 1. Build Pipeline (`generate/` - entirely new)

Upstream provides only raw OpenAPI model files. We added a complete build system that generates a single Java JAR from all ~55 API models using swagger-codegen.

### 2. RDT (Restricted Data Token) Support (`ApiClient.mustache`)

Added `setRdt(String rdt)` method to the generated ApiClient. When an RDT is set, the client skips LWA signing and directly adds the `x-amz-access-token` header. This is used for PII data access (buyer info, shipping addresses).

**Location**: `clients/sellingpartner-api-aa-java/resources/swagger-codegen/templates/ApiClient.mustache`

**How it works**: In the `buildRequest` method (around line 969-975), if `rdt` is null, normal LWA signing occurs. If `rdt` is set, it skips LWA and sets the access token header directly.

**Status**: The new Orders API v2026-01-01 uses a "simplified PII access model" that may reduce the need for RDT. However, other APIs (Merchant Fulfillment, etc.) may still require RDT for PII access, so this enhancement remains useful.

### 3. HTTP Client Upgrade (`sellingpartner-api-aa-java/pom.xml`)

Upgraded `org.apache.httpcomponents:httpclient:4.5.13` (v4, EOL) to `org.apache.httpcomponents.client5:httpclient5:5.2.1` (v5). Upstream still uses v4.

### 4. AWS SigV4 Signing Removed (`ApiClient.mustache`)

Commented out `awsSigV4Signer.sign(request)` call at line 975 of ApiClient.mustache. The current SP-API authentication flow uses LWA (Login with Amazon) tokens only; AWS SigV4 signing is no longer required for most operations.

### 5. okhttp2 to okhttp3 Migration

Swagger-codegen generates code for okhttp2 by default. We replace several generated files with okhttp3-compatible versions stored in `generate/`:
- `JSON.java` - JSON serialization utilities
- `ProgressResponseBody.java` - Download progress tracking
- `ProgressRequestBody.java` - Upload progress tracking
- `GzipRequestInterceptor.java` - Request compression
- `HttpBasicAuth.java` - Basic auth handler

These are copied over the codegen output during `compile.sh` (lines 109-114).

## Downstream Consumer

The generated JARs are consumed by **jazva8** (and legacy **jazva**), which are separate repositories at:
- `/Users/levon/Projects/jazva8/` - Main consumer, uses both JARs as Maven dependencies
- `/Users/levon/Projects/jazva/` - Legacy, also receives JARs

In jazva8's `pom.xml`, two dependencies reference these JARs:
- `com.amazon.sellingpartnerapi:sellingpartnerapi-aa-java:3.0`
- `com.amazon.sellingpartnerapi:selling-partner-api:2.0.1`

## Compiled APIs (v2.0.1)

### Core APIs

| Package | Model File | Version |
|---------|-----------|---------|
| `awd` | awd_2024-05-09.json | 2024-05-09 |
| `aplus` | aplusContent_2020-11-01.json | 2020-11-01 |
| `applicaitons` | application_2023-11-30.json | 2023-11-30 |
| `catalogv0` | catalogItemsV0.json | v0 |
| `catalogv20` | catalogItems_2020-12-01.json | 2020-12-01 |
| `catalogv22` | catalogItems_2022-04-01.json | 2022-04-01 |
| `queries` | dataKiosk_2023-11-15.json | 2023-11-15 |
| `easyship` | easyShip_2022-03-23.json | 2022-03-23 |
| `fbainbound` | fbaInbound.json | - |
| `fbainventory` | fbaInventory.json | - |
| `feeds` | feeds_2021-06-30.json | 2021-06-30 |
| `finances` | finances_2024-06-19.json | 2024-06-19 |
| `financestransfers` | transfers_2024-06-01.json | 2024-06-01 |
| `fulfillmentinbound` | fulfillmentInboundV0.json | v0 |
| `fulfillmentinboundbeta` | fulfillmentInbound_2024-03-20.json | 2024-03-20 |
| `fulfillmentoutbound` | fulfillmentOutbound_2020-07-01.json | 2020-07-01 |
| `listing` | listingsItems_2021-08-01.json | 2021-08-01 |
| `listingrestrictions` | listingsRestrictions_2021-08-01.json | 2021-08-01 |
| `merchantfulfillment` | merchantFulfillmentV0.json | v0 |
| `messaging` | messaging.json | - |
| `notifications` | notifications.json | - |
| `orders` | orders_2026-01-01.json | 2026-01-01 |
| `productfees` | productFeesV0.json | v0 |
| `productpricing` | productPricing_2022-05-01.json | 2022-05-01 |
| `definitions` | definitionsProductTypes_2020-09-01.json | 2020-09-01 |
| `sellingpartners` | replenishment-2022-11-07.json | 2022-11-07 |
| `reports` | reports_2021-06-30.json | 2021-06-30 |
| `sales` | sales.json | - |
| `sellers` | sellers.json | - |
| `services` | services.json | - |
| `shipmentinvoicing` | shipmentInvoicingV0.json | v0 |
| `shipping` | shippingV2.json | v2 |
| `solicitations` | solicitations.json | - |
| `supplysources` | supplySources_2020-07-01.json | 2020-07-01 |
| `tokens` | tokens_2021-03-01.json | 2021-03-01 |
| `uploads` | uploads_2020-11-01.json | 2020-11-01 |

### Vendor / Direct Fulfillment APIs

| Package | Model File | Version |
|---------|-----------|---------|
| `dfinventory` | vendorDirectFulfillmentInventoryV1.json | v1 |
| `dforders` | vendorDirectFulfillmentOrders_2021-12-28.json | 2021-12-28 |
| `dfpayments` | vendorDirectFulfillmentPaymentsV1.json | v1 |
| `dfshipping` | vendorDirectFulfillmentShipping_2021-12-28.json | 2021-12-28 |
| `dftransactions` | vendorDirectFulfillmentTransactions_2021-12-28.json | 2021-12-28 |
| `dfsandbox` | vendorDirectFulfillmentSandboxData_2021-10-28.json | 2021-10-28 |
| `vendorinvoices` | vendorInvoices.json | - |
| `vendororders` | vendorOrders.json | - |
| `vendorshipments` | vendorShipments.json | - |
| `vendortransactionstatus` | vendorTransactionStatus.json | - |

### New APIs (added in 2.0.1)

| Package | Model File | Version |
|---------|-----------|---------|
| `appintegrations` | appIntegrations-2024-04-01.json | 2024-04-01 |
| `customerfeedback` | customerFeedback_2024-06-01.json | 2024-06-01 |
| `deliverybyamazon` | deliveryShipmentInvoiceV2022-07-01.json | 2022-07-01 |
| `externalfulfillmentinventory` | externalFulfillmentInventory_2024-09-11.json | 2024-09-11 |
| `externalfulfillmentreturns` | externalFulfillmentReturns_2024-09-11.json | 2024-09-11 |
| `externalfulfillmentshipments` | externalFulfillmentShipments_2024-09-11.json | 2024-09-11 |
| `invoices` | InvoicesApiModel_2024-06-19.json | 2024-06-19 |
| `sellerwallet` | sellerWallet_2024-03-01.json | 2024-03-01 |
| `vehicles` | vehicles_2024-11-01.json | 2024-11-01 |

## Maintenance

### Upgrading from upstream

1. `git fetch upstream && git merge upstream/main`
2. Update version in `generate/pom.xml` (line 8) and `generate/compile.sh` (bottom install lines)
3. Run `cd generate && bash compile.sh`
4. In jazva8: update both dependencies in `pom.xml` to new versions
5. Git add the new lib version dirs in jazva8

### Adding a new API

1. Add a `generate "<package>" "<model-path>"` line to `generate/compile.sh`
2. Run `bash compile.sh` to regenerate
3. Update this README's API table

### Modifying model files

Model files in `models/` come from upstream. Occasionally we make local modifications to fix issues (e.g., PascalCase schema names in notifications, pruning unused definitions). These modifications should be documented in commit messages. The goal is to keep models as close to upstream as possible.

## Deprecation Notes

### Orders API v0 - Deprecated, deadline 3/27/2027
Amazon is removing 6 operations: `getOrders`, `getOrder`, `getOrderBuyerInfo`, `getOrderAddress`, `getOrderItems`, `getOrderItemsBuyerInfo`. We compile the new **Orders API v2026-01-01** which consolidates these into fewer endpoints with `includedData` parameters and simplified PII access.

### Catalog Items V0 - Deprecated
Upstream removed `listCatalogItems` and `getCatalogItem` operations from V0. The generated `CatalogV0Api` now only has `listCatalogCategories`. Use `catalogv22` (CatalogApi) for item lookups.

### FBA Small and Light - Removed
Model file still exists in upstream but is deprecated. Not included in `compile.sh`. Use Product Fees API instead.

### Authorization API, Feeds v2020-09-04, Reports v2020-09-04 - Removed
All deprecated by Amazon. We use their replacements (Feeds/Reports 2021-06-30).

## Known Issues

- **`applicaitons` typo**: Package name for Application Management API is misspelled. Kept for backward compatibility with jazva8. Fix requires coordinated rename.
- **Hardcoded paths in compile.sh**: Install paths reference `/Users/levon/Projects/` absolute paths. Must be run from that machine or paths updated.

## Version History

### 2.0.1 (current)
**Breaking changes** - requires code updates in jazva8.

- **Upgraded:** `sellingpartnerapi-aa-java` 2.0.1 -> 3.0 (okhttp2 -> okhttp3)
- **Upgraded:** okhttp 2.7.5 -> okhttp3 4.12.0
- **Upgraded:** `finances` v0 -> 2024-06-19
- **Upgraded:** `productpricing` v0 -> 2022-05-01
- **Upgraded:** `dforders` V1 -> 2021-12-28
- **Upgraded:** `dfshipping` V1 -> 2021-12-28
- **Upgraded:** `dftransactions` V1 -> 2021-12-28
- **Removed:** `fbaSmallandLight` (deprecated by Amazon)
- **Removed:** Custom model tag renames (using upstream models as-is now)
- **Added:** 12 new APIs (appintegrations, customerfeedback, deliverybyamazon, externalfulfillment x3, financestransfers, invoices, sellerwallet, vehicles, dfsandbox)
- **Fixed:** JAR installs to both jazva/lib and jazva8/lib
- **Fixed:** AA library auto-built during compile

### 1.0.18
- Added Orders API v2026-01-01
- Added fulfillment inbound beta (2024-03-20)

### 1.0.17 and earlier
- Legacy versions with older API models and okhttp2

## jazva8 Migration Checklist (v1.0.18 -> v2.0.1)

### Dependencies
1. Update `pom.xml`: `selling-partner-api` version `1.0.18` -> `2.0.1`
2. Update `pom.xml`: `sellingpartnerapi-aa-java` version `2.0.1` -> `3.0`

### Generated Class Name Changes (models now use upstream tags)
These classes are renamed because we stopped customizing the model tags:
- `Catalog2020Api` -> `CatalogApi` (in package `api.catalogv20`)
- `FbaInboundEligibilityApi` -> `FbaInboundApi` (in package `api.fbainbound`)
- `VendorOrdersDfApi` -> `VendorOrdersApi` (in package `api.dforders`)
- `VendorShippingLabelsDfApi` -> `VendorShippingLabelsApi` (in package `api.dfshipping`)
- `VendorShippingDfApi` -> `VendorShippingApi` (in package `api.dfshipping`)
- `CustomerInvoicesDfApi` -> `CustomerInvoicesApi` (in package `api.dfshipping`)
- `VendorTransactionDfApi` -> `VendorTransactionApi` (in package `api.dftransactions`)

### Removed/Changed APIs
3. Remove `CatalogV0Api.listCatalogItems()` / `.getCatalogItem()` calls (deprecated, removed by Amazon)
4. Remove `fbaSmallandLight` references (no longer compiled)

### Upgraded APIs (new class structures, update imports/usage)
5. `finances` - v0 -> 2024-06-19
6. `productpricing` - v0 -> 2022-05-01
7. `dforders` - V1 -> 2021-12-28
8. `dfshipping` - V1 -> 2021-12-28
9. `dftransactions` - V1 -> 2021-12-28

### New APIs (optional, no changes needed unless adopting)
10. appintegrations, customerfeedback, deliverybyamazon, externalfulfillment x3, financestransfers, invoices, sellerwallet, vehicles
