## Jazva Notes

We modified the code base, but still should periodically merge upstream/main into our branch and push
To build:

1. change version in [generate/pom.xml]
2. global replace the new version in generate/compile.sh
3. run generate/compile.sh (which has mvn install:install-file commented out)
4. fix the absolute path in mvn install:install-file 
5. manually run mvn install:install-file line to make sure it pushes to the lib folder in main jazva project
6. add the new version to git in jazva project ./lib/com/amazon/sellingpartnerapi/...
7. update jazvas main pom.xml to use the new version

Note: sometimes we need to update the authorization-authentication module as well, which is a dependency in the above pom. The lines to build it are in the same compile.sh file


## Selling Partner API Models
This repository contains Swagger models for developers to use to create software to call Selling Partner APIs. Developers can use [swagger codegen](https://github.com/swagger-api/swagger-codegen) to generate client libraries from these models. Please refer to [selling-partner-api-docs](https://github.com/amzn/selling-partner-api-docs) for additional documentation and read the [Selling Partner API Developer Guide](https://github.com/amzn/selling-partner-api-docs/blob/main/guides/en-US/developer-guide/SellingPartnerApiDeveloperGuide.md) for instructions to get started.

The [models directory](https://github.com/amzn/selling-partner-api-models/tree/main/models) contains all of the currently available Selling Partner API models.

The [clients directory](https://github.com/amzn/selling-partner-api-models/tree/main/clients) contains a [Java library](https://github.com/amzn/selling-partner-api-models/tree/main/clients/sellingpartner-api-aa-java) and a [C# library](https://github.com/amzn/selling-partner-api-models/tree/main/clients/sellingpartner-api-aa-csharp) with mustache templates for use with [swagger-codegen](https://swagger.io/tools/swagger-codegen/) to generate client libraries with authentication and authorization functionality included. The templates are located in *resources/swagger-codegen*.

The [schemas directory](https://github.com/amzn/selling-partner-api-models/tree/main/schemas) contains all of the currently available Selling Partner Api schemas.

## Security

See [CONTRIBUTING](CONTRIBUTING.md#security-issue-notifications) for more information.

## License

This project is licensed under the Apache-2.0 License.

