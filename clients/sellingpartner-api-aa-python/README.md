---
title: "Tutorial: Automate your SP-API calls using Python SDK"
excerpt: "Tutorial to automate your SP-API calls with Python SDK with Login with Amazon (LWA) token exchange and authentication."
slug: "tutorial-automate-your-sp-api-calls-using-python-sdk"
category: 659db4609baec900382f6ebe
---

This tutorial provides you with all the required details to generate a Python SDK with Login with Amazon (LWA) token exchange and authentication, to build your application seamlessly. You will learn the prerequisites required to build the Python SDK and also view an example using the Selling Partner API for Orders and the Swagger Code Generator.

You can use this SDK to integrate Amazon marketplace features into your applications, including accessing product information, managing orders, handling shipments, and more.

## Tutorial

The following tutorial will help you set up your own Python SDK for automating SP-API calls.

### Prerequisites

To complete this tutorial, you need the following prerequisites:

* A hybrid or SP-API app in draft or published state
* Integrated development environment (IDE) software
* Python (version 3.6 or later)
* `swagger-codegen-cli-2.3` (or later) This tool is used to generate Python client code from the SP-API's Swagger definitions.

Before your application can connect to the Selling Partner API, you must register it, and it must be authorized by a selling partner. If you do not have a hybrid or SP-API app, follow the steps to [register as a developer](https://developer-docs.amazon.com/sp-api/docs/registering-as-a-developer), [register your application](https://developer-docs.amazon.com/sp-api/docs/registering-your-application), and [Authorizing Selling Partner API applications](https://developer-docs.amazon.com/sp-api/docs/authorizing-selling-partner-api-applications). Then, return to this tutorial.

Next, set up your workspace for the tutorial.

### Step 1. Set up your workspace

1. On your local drive, create a directory for this project, name it `SPAPI_Python_SDK`, and navigate to the new directory.
2. Clone the [client repo](https://github.com/amzn/selling-partner-api-models/tree/main/clients).
3. Download the following tools.
    - IDE software (this walkthrough uses [Visual Studio](https://visualstudio.microsoft.com/) IDE on Windows OS)
    - Python (version 3.6 or later). You can download this software from [python.org](https://www.python.org/downloads/).
4. Run the following command in your terminal to download the <span class="notranslate">Swagger Code Jar</span>:

```
 `wget https://repo1.maven.org/maven2/io/swagger/swagger-codegen-cli/2.4.13/swagger-codegen-cli-2.4.13.jar -O swagger-codegen-cli.jar`
```

5. Copy `swagger-codegen-cli.jar` into your local directory `C:\\SPAPI_Python_SDK`.
6. Run the following command in your terminal to install the Python `backoff` library in your environment:

```
 pip install  backoff
```
7. In GitHub, go to <span class="notranslate"><https://github.com/amzn/selling-partner-api-models/tree/main/models></span> and run the following command to clone the <span class="notranslate">selling-partner-api-models</span> repository to your local directory `C:\\SPAPI_Python_SDK`.

```
 git clone https://github.com/amzn/selling-partner-api-models
```

Now that you have completed the required setup, the next step is to generate the Python SDK with the authentication and authorization classes downloaded to your local directory `C:\\SPAPI_Python_SDK`.

### Step 2. Generate a Python client from Swagger definitions

1. Locate the Swagger JSON file for the [SP-API API model](https://github.com/amzn/selling-partner-api-models/tree/main/models) you want to use (for example, Orders API) from your local directory `C:\\SPAPI_Python_SDK`.
2. Run the following command in your terminal to generate client code. Make sure to replace the paths and API model with your settings.

```
 java -jar /[path_to_swagger_jar]/swagger-codegen-cli.jar generate -l python -t /[path_to_mustach_resources]/resources/ -D packageName=swagger_client -o /[path_to_client_folder]/client/[SP-API_NAME] -i /[path_to_model_folder]/models/[SP-API_NAME]/SP-API.json
```

Now that you have generated a Python client, you need to integrate the authentication model.

### Step 3. Integrate the authentication module

1. Locate the `auth` and `spapi` client code folders in `C:\\SPAPI_Python_SDK`, the directory where the SDK was downloaded.
2. Update the paths in the following Python files: `spapiclient.py` and `LwaRequest.py`.

With authentication set up, you're now ready to set up the Python SDK package.

### Step 4. Set up the Python SDK package

1. Navigate to `C:\\SPAPI_Python_SDK` the directory where the SDK was generated.
2. Use the following code to create a `setup.py` file. This file is required for packaging your SDK. Make sure you replace information in the example with information for your package and dependencies.

```python
  from setuptools import setup, find_packages
	 
  setup(
      name='SellingPartnerAPIAuthAndAuthPython',  # Replace with your package's name
      version='1.0.0',         # Replace with your package's version 
      package_dir={'': 'src'},  # Replace 'src' as necessary
      packages=find_packages(where='src'),
      install_requires=[line.strip() for line in open("requirements.txt", "r")],
      description='A Python SDK for Amazon Selling Partner API',
      long_description=open('README.md').read(),
      long_description_content_type='text/markdown',
      url='TBD'
  )
```

With the Python SDK set up, you're now ready to interact with the SP-API endpoints.

### Step 5. Create an instance of the Orders API and call an operation

The following is an example of how to use the Python SDK with the Orders API to make a `getOrders` request. Update the code with your information and then run the code in your terminal.

```python
  if __name__ == "__main__":
	 
      from auth.credentials import SPAPIConfig
      config = SPAPIConfig(
          client_id="Your client-id",
          client_secret="Your client-secret",
          refresh_token="Your refresh-token",
          region="NA",  # Possible values NA, EU, FE, and SANDBOX
          scope = None # Required for grant_type='client_credentials' ; Possible values "sellingpartnerapi::notifications" and "sellingpartnerapi::migration"
      )
	 
      from spapi.spapiclient import SPAPIClient
	 
      # Create the API Client
      print("Config and client initialized...")
      api_client = SPAPIClient(config)
	 
      marketplace_ids = ["ATVPDKIKX0DER"]
      created_after = "2024-01-19T00:00:00"
	 
      orders_api = api_client.get_api_client('OrdersV0Api')
      orders_response = orders_api.get_orders(marketplace_ids=marketplace_ids, created_after=created_after)
      print("Orders API Response:")
      print(orders_response)
```

> 🚧 Caution
>
> Never commit this file to your version control system as it contains sensitive information. Please ensure these LWA credentials are stored securely in an encrypted format.

A status code of 200 means the API call was successful.

## Step 6. Connect to the Selling Partner API using the generated the Python SDK

Run the following commands in your terminal to build and install your SDK locally:

     ```bash
     python3 setup.py sdist bdist_wheel
     ```
     ```bash
     pip install dist/{YourPackageName}-1.0.0-py3-none-any.whl
     ```

Run the following test script in your terminal to test the Python SDK:

      ```bash
      python test.py
      ```

A status code of 200 means the API call was successful.

## Conclusion

In this tutorial, you learned how to automate your SP-API calls using an SP-API SDK for Python. In the walkthrough, you learned how to set up your workspace, generate a Python SDK for Selling Partner API, connect to the Orders API, and make your first API call.
