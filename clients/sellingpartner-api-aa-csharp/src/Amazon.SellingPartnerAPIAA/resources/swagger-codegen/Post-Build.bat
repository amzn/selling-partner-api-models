java ^
-jar C:\swagger-codegen\swagger-codegen-cli.jar generate ^
-l csharp ^
-i ..\..\..\..\models\orders-api-model\ordersV0.json ^
-t ..\..\..\..\clients\sellingpartner-api-aa-csharp\src\Amazon.SellingPartnerAPIAA\resources\swagger-codegen\templates ^
-o ..\..\..\..\clients\sellingpartner-api-aa-csharp\client ^
-c ..\..\..\..\clients\sellingpartner-api-aa-csharp\src\Amazon.SellingPartnerAPIAA\resources\swagger-codegen\config.json ^
--ignore-file-override=..\..\..\..\clients\sellingpartner-api-aa-csharp\src\Amazon.SellingPartnerAPIAA\resources\swagger-codegen\.swagger-codegen-ignore