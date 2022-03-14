#!/bin/bash

cd ..
rm -rf generated
basePackage="com.amazon.sellingpartner"
models=(models/*/*)
#models=(models/*/listingsIte*)

# For every model in the models/ directory, and all subdirectories:
for model in "${models[@]}"
do
    # Generate a client library
    # --input-spec $model 			:: use the current model file
    # --lang java 				:: generate a Java library
    # --template-dir .../ 			:: use Amazon's premade Java template files
    # --output ../spapi 			:: put the generated library in ../spapi
    # --invoker-package "..." 			:: put the generated code in the given package
    # --api-package "..." 			:: put the generated api code in the given package
    # --model-package "..." 			:: put the generated model code in the given package
    # --group-id "..." 				:: package metadata
    # --artifact-id "..." 			:: package metadata
    # --additional-properties dateLibrary=java8 :: Use Java 8 date libraries
    java -jar generate/swagger-codegen-cli.jar generate \
        --input-spec $model \
        --lang java \
        --template-dir clients/sellingpartner-api-aa-java/resources/swagger-codegen/templates \
        --output generated/spapi \
        --invoker-package "$basePackage" \
        --api-package "$basePackage.api.$model" \
        --model-package "$basePackage.model.$model" \
        --group-id "com.amazon" \
        --artifact-id "selling-partner-api" \
        --additional-properties dateLibrary=java8
done

cd clients/sellingpartner-api-aa-java
mvn clean package
mvn install:install-file -Dfile=target/sellingpartnerapi-aa-java-1.0.jar -DgroupId=com.amazon.sellingpartnerapi -DartifactId=sellingpartnerapi-aa-java -Dversion=1.0 -Dpackaging=jar

cd ../..
cp -r generate/pom.xml generated/spapi
cd generated/spapi
mvn clean package

# install into jazva/lib  check path
# fix path and run manually from root folder
# sudo mvn install:install-file -Dfile=generated/spapi/target/selling-partner-api-1.0.1.jar -Dsources=generated/spapi/target/selling-partner-api-1.0.1-sources.jar -Djavadoc=generated/spapi/target/selling-partner-api-1.0.1-javadoc.jar -DgroupId=com.amazon.sellingpartnerapi -DartifactId=selling-partner-api -Dversion=1.0.1 -Dpackaging=jar -DlocalRepositoryPath=/Users/levon/Projects/jazva/lib
