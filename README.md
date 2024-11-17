# `sellingpartnerapi-aa-java` Fork

## Modifications

All modifications apply to the `sellingpartnerapi-aa-java` client.

* Dependency updates (not okhttp and apache httpcomponents - sadly)
* Attach sources to jar
* Define ItemAttributes of Catalog API as hashmap containing arrays of jsonObjects
* bugfix for stackoverflow error caused by networkInterceptors in async methods

## Update instructions

to update this repository with changes by Amazon:

* add a git remote for the amazon repo: `git remote add amazon git@github.com:amzn/selling-partner-api-models.git`
* create a new branch and perform a git merge: `git merge amazon/main`

# Amazon README

## Selling Partner API Models
This repository contains Swagger models for developers to use to create software that calls Selling Partner APIs. Developers can use [swagger codegen](https://github.com/swagger-api/swagger-codegen) to generate client libraries from these models. Please refer to [selling-partner-api-docs](https://github.com/amzn/selling-partner-api-docs) for additional documentation and read the [Selling Partner API Developer Guide](https://github.com/amzn/selling-partner-api-docs/blob/main/guides/en-US/developer-guide/SellingPartnerApiDeveloperGuide.md) for instructions to get started.

The [models directory](https://github.com/amzn/selling-partner-api-models/tree/main/models) contains all of the currently available Selling Partner API models.

The [clients directory](https://github.com/amzn/selling-partner-api-models/tree/main/clients) contains a [Java library](https://github.com/amzn/selling-partner-api-models/tree/main/clients/sellingpartner-api-aa-java) and a [C# library](https://github.com/amzn/selling-partner-api-models/tree/main/clients/sellingpartner-api-aa-csharp) with mustache templates for use with [swagger-codegen](https://swagger.io/tools/swagger-codegen/) to generate client libraries with authentication and authorization functionality included. The templates are located in *resources/swagger-codegen*.

The [schemas directory](https://github.com/amzn/selling-partner-api-models/tree/main/schemas) contains all of the currently available Selling Partner Api schemas.

**Note:** For any issues related to SP API like bugs or troubleshooting, please reach out to [Developer Support](https://developer.amazonservices.com/support) so that the right team can help / channel your inputs (in case of bugs / feature requests) to the development team.

## Security

See [CONTRIBUTING](CONTRIBUTING.md#security-issue-notifications) for more information.

## License

This project is licensed under the Apache-2.0 License.

