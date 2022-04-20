package sampleCode;

// Imports from the Selling Partner API (SP-API) Auth & Auth client library.
import com.amazon.SellingPartnerAPIAA.AWSAuthenticationCredentials;
import com.amazon.SellingPartnerAPIAA.AWSAuthenticationCredentialsProvider;
import com.amazon.SellingPartnerAPIAA.AWSSigV4Signer;
import com.squareup.okhttp.OkHttpClient;
import com.squareup.okhttp.Request;
import com.squareup.okhttp.RequestBody;
import com.squareup.okhttp.Response;

import java.io.IOException;

public class DelegatedRestrictedDataTokenWorkflowForDelegatee {

    // This represents a workflow for a simple use case. Other use cases can be implemented using similar patterns.
    public static void main(String[] args) throws IOException {
        // The values for method, path and restrictedDataToken should match the RDT request made by delegator and the response.
        String method = "GET";
        String path = "/orders/v0/orders/123-1234567-1234567";
        String restrictedDataToken = "Atz.sprdt|AYABeKCs7hKXXXXXXXXXXXXXXXXXX...";

        // Build, sign, and execute the request, specifying method, path, RDT, and RequestBody.
        // Pass a RequestBody only if required by the restricted operation. The requestBody is not required in this example.
        Response restrictedRequestResponse = buildAndExecuteDelegatedRestrictedRequest(method, path, restrictedDataToken, null);

        // Check the restricted operation response status code and headers.
        System.out.println(restrictedRequestResponse.code());
        System.out.println(restrictedRequestResponse.headers());
    }

    // The SP-API endpoint.
    private static final String sellingPartnerAPIEndpoint = "https://sellingpartnerapi-na.amazon.com";

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

    // An example of a helper method to build, sign, and execute a restricted operation, specifying RestrictedResource, (String) RDT, and RequestBody.
    // Returns the restricted operation Response object.
    private static Response buildAndExecuteDelegatedRestrictedRequest(String method, String path, String restrictedDataToken, RequestBody requestBody) throws IOException {
        // Construct a request with the specified RestrictedResource, RDT, and RequestBody.
        Request signedRequest = new Request.Builder()
                .url(sellingPartnerAPIEndpoint + path)  // Define the URL for the request, based on the endpoint and restricted resource path.
                .method(method, requestBody)  // Define the restricted resource method value, and requestBody, if required by the restricted operation.
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

        return response;
    }

}