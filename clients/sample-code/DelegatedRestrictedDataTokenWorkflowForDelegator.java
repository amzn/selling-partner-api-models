package sampleCode;

// Imports from the Selling Partner API (SP-API) Auth & Auth client library.
import com.amazon.SellingPartnerAPIAA.AWSAuthenticationCredentials;
import com.amazon.SellingPartnerAPIAA.AWSAuthenticationCredentialsProvider;
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


public class DelegatedRestrictedDataTokenWorkflowForDelegator {

    public static void main(String[] args) throws IOException, ApiException {
        // This method represents a workflow for a simple use case. More use cases can be defined using a similar pattern.
        callDelegatedRestrictedOperationWorkflow();
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
    // If you registered your application using an IAM Role ARN, the AWSAuthenticationCredentials and AWSAuthenticationCredentialsProvider objects are required.
    private static final AWSAuthenticationCredentials awsAuthenticationCredentials = AWSAuthenticationCredentials.builder()
            // If you registered your application using an IAM Role ARN, use the AWS credentials of an IAM User that is linked to the IAM Role through the AWS Security Token Service policy.
            // Or, if you registered your application using an IAM User ARN, use the AWS credentials of that IAM User. Be sure that the IAM User has the correct IAM policy attached.
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
    private static void callDelegatedRestrictedOperationWorkflow() throws IOException, ApiException {

        // Define the target application to which access is being delegated.
        final String targetApplication = "amzn1.sellerapps.app.target-application";

        // Define the path for the restricted operation that requires an RDT.
        final String resourcePath = "/orders/v0/orders/123-1234567-1234567";

        // Define the dataElements to indicates the type of Personally Identifiable Information requested.
        // This parameter is required only when getting an RDT for use with the getOrder, getOrders, or getOrderItems operation of the Orders API
        final List<String> dataElements = Arrays.asList("buyerInfo","shippingAddress");

        // Build the RestrictedResource object with the respective Method (MethodEnum from RestrictedResource class), Path and dataElements.
        RestrictedResource resource = buildRestrictedResource(RestrictedResource.MethodEnum.GET, resourcePath, dataElements);

        // Make a list of the RestrictedResource objects that will be included in the request to create the RDT.
        List<RestrictedResource> resourceList = Arrays.asList(resource);

        // Get a delegated RDT for a list of RestrictedResource objects for a target application.
        restrictedDataToken = getDelegatedRestrictedDataToken(targetApplication, resourceList);

        // Pass the delegated RDT to the targetApplication owner.
        // Delegated party should build, sign, and execute the request.
        // An RDT expires after 60 minutes.

        // Below is a example of how the workflow would look when requesting an RDT for multiple restricted resources.
        // You can specify a maximum of 50 restricted resources.
        /*
        // Define a path for each restricted operation that requires an RDT.
        final String resourcePath1 = "/orders/v0/orders";
        final String resourcePath2 = "/orders/v0/orders/123-7654321-1234567";
        final String resourcePath3 = "/orders/v0/orders/123-1234567-7654321/items";
        final String resourcePath4 = "/mfn/v0/shipments/FBA1234ABC5D";
        // Define the dataElements to indicate the type of Personally Identifiable Information to be requested.
        // This parameter is required only when getting an RDT for use with the getOrder, getOrders, or getOrderItems operation of the Orders API
        final List<String> dataElements = Arrays.asList("buyerInfo","shippingAddress");
        // Build each RestrictedResource object with the respective Method (MethodEnum from RestrictedResource class) and Path.
        RestrictedResource resource1 = buildRestrictedResource(RestrictedResource.MethodEnum.GET, resourcePath1, dataElements);
        RestrictedResource resource2 = buildRestrictedResource(RestrictedResource.MethodEnum.GET, resourcePath2, dataElements);
        RestrictedResource resource3 = buildRestrictedResource(RestrictedResource.MethodEnum.GET, resourcePath3, dataElements);
        RestrictedResource resource4 = buildRestrictedResource(RestrictedResource.MethodEnum.GET, resourcePath4);
        // Make a list of the RestrictedResource objects that will be included in the request to create the RDT.
        List<RestrictedResource> resourceList = Arrays.asList(resource1,resource2,resource3,resource4);
        // Get a delegated RDT for a list of RestrictedResource objects for a target application.
        restrictedDataToken = getDelegatedRestrictedDataToken(targetApplication, resourceList);
        // Pass the delegated RDT to the targetApplication owner.
        // If you request an RDT for multiple restricted resources; the delegated party should build, sign and execute each restricted operation separately.
        // An RDT expires after 60 minutes.
         */
    }

    // An example of a helper method for building RestrictedResource objects with dataElements parameters.
    private static RestrictedResource buildRestrictedResource(RestrictedResource.MethodEnum method, String path, List<String> dataElements){
        RestrictedResource resource = buildRestrictedResource(method,path);
        resource.dataElements(dataElements);
        return resource;
    }

    // An example of a helper method for building RestrictedResource objects, specifying the method (MethodEnum from RestrictedResource class) and path parameters.
    private static RestrictedResource buildRestrictedResource(RestrictedResource.MethodEnum method, String path){
        RestrictedResource resource = new RestrictedResource();
        resource.setMethod(method);
        resource.setPath(path);
        return resource;
    }

    // An example of a helper method for creating a delegated RDT for a list of RestrictedResource objects for a target application.
    private static String getDelegatedRestrictedDataToken(String targetApplication, List<RestrictedResource> resourceList) throws ApiException {
        // Initialize a CreateRestrictedDataTokenRequest object that represents the Restricted Data Token request body.
        CreateRestrictedDataTokenRequest restrictedDataTokenRequest = new CreateRestrictedDataTokenRequest();

        // Set target application in the CreateRestrictedDataTokenRequest object
        restrictedDataTokenRequest.setTargetApplication(targetApplication);

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