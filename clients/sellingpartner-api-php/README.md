# SP API PHP Sandbox
This is example PHP Client will show you all initial steps you need to do, to launch Selling Partner API in your project.
It's perfect for the quick start, but not enough to be a working solution. 
On top of it you can build the integration you like. 
You don't wanna use the GazzelHttp? Not a problem write you own ApiTransport which implements ApiTransportInterface and you good to go!
You would like to map responses to your domain models? Easy! Write you mapper and inject it to any client you want to map response and return response as model.

## Requirements
* PHP >=7.4.0 
* [Composer](https://getcomposer.org/)
* Complete this guide -> [Registering as a developer](https://github.com/amzn/selling-partner-api-docs/blob/main/guides/developer-guide/SellingPartnerApiDeveloperGuide.md#registering-as-a-developer)
* Complete this guide -> [Registering your Selling Partner API application](https://github.com/amzn/selling-partner-api-docs/blob/main/guides/developer-guide/SellingPartnerApiDeveloperGuide.md#registering-your-selling-partner-api-application)

## Building
* Run `composer install`

## Example usage 
* See [index.php](index.php) file

## How to run  
* Fill out [.env](.env) file
* Follow [Self Authorization](https://github.com/amzn/selling-partner-api-docs/blob/main/guides/developer-guide/SellingPartnerApiDeveloperGuide.md#self-authorization) and receive `refresh_token`
* Add `refresh_token` to [.env](.env) file
* Run `index.php` and you should see a `var_dump` of orders data

## License
Licensed under the Apache License, Version 2.0 (the "License");
you may not use this library except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

Copyright (c) 2020 Pavlo Cherniavsyi