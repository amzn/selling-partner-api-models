package sampleCode;

// Imports from the Selling Partner API (SP-API) Auth & Auth client library.
import com.amazon.SellingPartnerAPIAA.AWSAuthenticationCredentials;
import com.amazon.SellingPartnerAPIAA.AWSAuthenticationCredentialsProvider;
import com.amazon.SellingPartnerAPIAA.AWSSigV4Signer;
import com.amazon.SellingPartnerAPIAA.LWAAuthorizationCredentials;

// Imports from the generated Tokens API client library.
import io.swagger.client.ApiException;
import io.swagger.client.api.TokensApi;
import io.swagger.client.model.CreateRestrictedDataTokenRequest;
import io.swagger.client.model.CreateRestrictedDataTokenResponse;
import io.swagger.client.model.RestrictedResource;

// Imports from the generated Tokens API client library dependencies.
import java.io.IOException;
import java.util.Arrays;
import java.util.List;
import com.google.gson.JsonObject;
import com.google.gson.JsonParser;
import com.squareup.okhttp.OkHttpClient;
import com.squareup.okhttp.Request;
import com.squareup.okhttp.RequestBody;
import com.squareup.okhttp.Response;

public class RestrictedDataTokenWorkflow {

    public static void main(String[] args) throws IOException, ApiException {
        // This method represents a workflow for a simple use case. More use cases can be defined using a similar pattern.
        callRestrictedOperationWorkflow();
    }

    // The SP-API endpoint.
    private static final String sellingPartnerAPIEndpoint = "https://sellingpartnerapi-na.amazon.com";

    // Declare a string variable for the Restricted Data Token (RDT).
    private static String restrictedDataToken;

    // Configure the LWAAuthorizationCredentials object.
    private static final LWAAuthorizationCredentials lwaAuthorizationCredentials = LWAAuthorizationCredentials.builder()
            .clientId("<application_client_id>")
            .clientSecret("<application_client_secret>")
            .refreshToken("Atzr|XXXXXXXXXXX")
            .endpoint("https://api.amazon.com/auth/O2/token")
            .build();

    // Configure the AWSAuthenticationCredentials object.
    private static final AWSAuthenticationCredentials awsAuthenticationCredentials = AWSAuthenticationCredentials.builder()
            // If application registered with Role ARN, use here the aws credentials of a user that is linked to the Role ARN via security token service.
            // Otherwise, if the application was registered using User ARN, use the access key and secret key of the User ARN, but you have to make sure that it has the policy attached.
            .accessKeyId("aws_access_key")
            .secretKey("aws_secret_key")
            .region("aws_region")
            .build();

    // Configure the AWSAuthenticationCredentialsProvider object. This is only needed for applications registered using an IAM Role.
    // If the application was registered using an IAM User, the AWSAuthenticationCredentialsProvider object should be removed.
    private static final AWSAuthenticationCredentialsProvider awsAuthenticationCredentialsProvider = AWSAuthenticationCredentialsProvider.builder()
            // The IAM Role must have the IAM policy attached as described in "Step 3. Create an IAM policy" in the SP-API Developer Guide.
            .roleArn("arn:aws:iam::XXXXXXXXX:role/XXXXXXXXX")
            .roleSessionName("session-name")
            .build();

    // The SP-API Tokens API instance used to call the createRestrictedDataToken operation.
    private static final TokensApi tokensApi = new TokensApi.Builder()
            .awsAuthenticationCredentials(awsAuthenticationCredentials)
            .awsAuthenticationCredentialsProvider(awsAuthenticationCredentialsProvider)  // If the application uses User ARN, this line is not needed. Remove it or pass a null value.
            .lwaAuthorizationCredentials(lwaAuthorizationCredentials)
            .endpoint(sellingPartnerAPIEndpoint)
            .build();

    // This method wraps the workflow to request an RDT and make a call to a restricted operation.
    private static void callRestrictedOperationWorkflow() throws IOException, ApiException {
        // Define the path for the restricted operation that requires an RDT.
        final String resourcePath = "/orders/v0/orders/123-1234567-1234567/address";

        // Build the RestrictedResource object with the respective Method (MethodEnum from RestrictedResource class) and Path.
        RestrictedResource resource = buildRestrictedResource(RestrictedResource.MethodEnum.GET, resourcePath);

        // Make a list of the RestrictedResource objects that will be included in the request to create the RDT.
        List<RestrictedResource> resourceList = Arrays.asList(resource);

        // Get an RDT for the list of restricted resources.
        restrictedDataToken = getRestrictedDataToken(resourceList);

        // Build, sign, and execute the request, specifying RestrictedResource, RDT, and RequestBody.
        // Pass a RequestBody only if required by the restricted operation. The requestBody is not required in this example.
        Response restrictedRequestResponse = buildAndExecuteRestrictedRequest(resource, restrictedDataToken, null);

        // You can handle the response as a JSON object, as shown in this example.
        JsonObject responseBodyJson = new JsonParser().parse(restrictedRequestResponse.body().string()).getAsJsonObject();
        System.out.println(responseBodyJson);

        // Below is a example of how the workflow would look when requesting an RDT for multiple restricted resources.
        // You can specify a maximum of 50 restricted resources.
        /*
        // Define a path for each restricted operation that requires an RDT.
        final String resourcePath1 = "/orders/v0/orders/123-1234567-1234567/address";
        final String resourcePath2 = "/orders/v0/orders/123-7654321-1234567/address";
        final String resourcePath3 = "/orders/v0/orders/123-1234567-7654321/address";

        // Build each RestrictedResource object with the respective Method (MethodEnum from RestrictedResource class) and Path.
        RestrictedResource resource1 = buildRestrictedResource(RestrictedResource.MethodEnum.GET, resourcePath1);
        RestrictedResource resource2 = buildRestrictedResource(RestrictedResource.MethodEnum.GET, resourcePath2);
        RestrictedResource resource3 = buildRestrictedResource(RestrictedResource.MethodEnum.GET, resourcePath3);

        // Make a list of the RestrictedResource objects that will be included in the request to create the RDT.
        List<RestrictedResource> resourceList = Arrays.asList(resource1,resource2,resource3);

        // Get an RDT for the list of restricted resources.
        restrictedDataToken = getRestrictedDataToken(resourceList);

        // If you request an RDT for multiple restricted resources; build, sign and execute each restricted operation separately.
        // Pass the same RDT when building each restricted operation request. An RDT expires after 60 minutes.

        // Build, sign, and execute each request, specifying RestrictedResource, RDT, and RequestBody.
        // Pass a RequestBody only if required by the restricted operation. The requestBody is not required in this example.
        Response restrictedRequestResponse1 = buildAndExecuteRestrictedRequest(resource1, restrictedDataToken, null);
        Response restrictedRequestResponse2 = buildAndExecuteRestrictedRequest(resource2, restrictedDataToken, null);
        Response restrictedRequestResponse3 = buildAndExecuteRestrictedRequest(resource3, restrictedDataToken, null);
         */
    }


    // An example of a helper method to build, sign, and execute a restricted operation, specifying RestrictedResource, (String) RDT, and RequestBody.
    // Returns the restricted operation Response object.
    private static Response buildAndExecuteRestrictedRequest(RestrictedResource resource, String restrictedDataToken, RequestBody requestBody) throws IOException {
        // Construct a request with the specified RestrictedResource, RDT, and RequestBody.
        Request signedRequest = new Request.Builder()
                .url(sellingPartnerAPIEndpoint + resource.getPath())  // Define the URL for the request, based on the endpoint and restricted resource path.
                .method(resource.getMethod().getValue(), requestBody)  // Define the restricted resource method value, and requestBody, if required by the restricted operation.
                .addHeader("x-amz-access-token", restrictedDataToken) // Sign the request with the RDT by adding it to the "x-amz-access-token" header.
                .build(); // Build the request.

        // Initiate an AWSSigV4Signer instance using your AWS credentials. This example is for an application registered using an AIM Role.
        AWSSigV4Signer awsSigV4Signer = new AWSSigV4Signer(awsAuthenticationCredentials, awsAuthenticationCredentialsProvider);

        /*
        // Or, if the application was registered using an IAM User, use the following example:
        AWSSigV4Signer awsSigV4Signer = new AWSSigV4Signer(awsAuthenticationCredentials);
        */

        // Sign the request with the AWSSigV4Signer.
        signedRequest = awsSigV4Signer.sign(signedRequest);

        // Execute the signed request.
        OkHttpClient okHttpClient = new OkHttpClient();
        Response response = okHttpClient.newCall(signedRequest).execute();

        // Check the restricted operation response status code and headers.
        System.out.println(response.code());
        System.out.println(response.headers());
        return response;
    }

    // An example of a helper method for building RestrictedResource objects, specifying the method (MethodEnum from RestrictedResource class) and the path.
    private static RestrictedResource buildRestrictedResource(RestrictedResource.MethodEnum method, String path){
        RestrictedResource resource = new RestrictedResource();
        resource.setMethod(method);
        resource.setPath(path);
        return resource;
    }

    // An example of a helper method for creating an RDT for a list of RestrictedResource objects.
    private static String getRestrictedDataToken(List<RestrictedResource> resourceList) throws ApiException {
        // Initialize a CreateRestrictedDataTokenRequest object that represents the Restricted Data Token request body.
        CreateRestrictedDataTokenRequest restrictedDataTokenRequest = new CreateRestrictedDataTokenRequest();

        // Add a resource list to the CreateRestrictedDataTokenRequest object.
        restrictedDataTokenRequest.setRestrictedResources(resourceList);

        try {
            CreateRestrictedDataTokenResponse response = tokensApi.createRestrictedDataToken(restrictedDataTokenRequest);
            restrictedDataToken = response.getRestrictedDataToken();
            return restrictedDataToken;
        } catch (ApiException e) {
            System.out.println(e.getResponseHeaders());  // Capture the response headers when a exception is thrown.
            throw e;
        }
    }
}
