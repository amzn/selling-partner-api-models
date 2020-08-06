package com.amazon.spapi;

import com.amazon.SellingPartnerAPIAA.AWSAuthenticationCredentials;
import com.amazon.SellingPartnerAPIAA.LWAAuthorizationCredentials;
import com.amazon.spapi.sequencing.client.UploadDetails;
import com.amazon.spapi.sequencing.client.UploadsSequencer;
import com.amazon.spapi.sequencing.client.impl.UploadsSequencerImpl;
import io.swagger.client.ApiException;
import io.swagger.client.api.UploadsApi;
import org.junit.Ignore;
import org.junit.Test;

import java.io.File;
import java.io.FileNotFoundException;
import java.net.URL;

import static org.junit.Assert.assertNotNull;
import static org.junit.Assert.assertTrue;

/**
 * API tests for UploadsApi
 */
public class CreateDestinationAndUploadTest {

    private final String feedType = "_POST_PRODUCT_DATA_";

    private final UploadsSequencer sequencer;

    {
        sequencer = testSequencer();

        // Fill out the API sequencer with your credentials here.
    }

    static {
        System.setProperty("javax.net.ssl.trustStore",
                           getResourceFile("certs/InternalAndExternalTrustStore.jks").getPath()
        );
        System.setProperty("javax.net.ssl.trustStorePassword", "amazon");
    }

    private UploadsSequencer createSequencer(String accessKeyId, String secretKey, String clientId, String clientSecret,
                                             String refreshToken) {
        AWSAuthenticationCredentials
                awsAuthenticationCredentials = AWSAuthenticationCredentials.builder()
                                                                           .accessKeyId(accessKeyId)
                                                                           .secretKey(secretKey)
                                                                           .region("us-west-2")
                                                                           .build();


        LWAAuthorizationCredentials lwaAuthorizationCredentials =
                LWAAuthorizationCredentials.builder()
                                           .clientId(clientId)
                                           .clientSecret(clientSecret)
                                           .refreshToken(
                                                   refreshToken)
                                           .endpoint("https://api.integ.amazon.com/auth/O2/token")
                                           .build();
        UploadsApi api = new UploadsApi.Builder().awsAuthenticationCredentials(awsAuthenticationCredentials)
                                                 .lwaAuthorizationCredentials(lwaAuthorizationCredentials)
                                                 .endpoint("https://marketplaceapi-beta.amazonservices.com")
                                                 .build();

        return UploadsSequencerImpl.builder()
                                   .uploadsApi(api)
                                   .build();
    }

    private static File getResourceFile(String fileName) {
        final URL fileUrl = UploadsApi.class.getClassLoader().getResource(fileName);
        assertNotNull(fileName + " cannot be reached", fileUrl);
        final File file = new File(fileUrl.getFile());
        assertTrue(file.exists());
        return file;
    }

    @Test
    @Ignore
    public void createDestinationThenUpload() throws FileNotFoundException, ApiException {
        //file must be in "test/resources"
        final File flatFileInventoryLoader = getResourceFile("Feed_101__POST_PRODUCT_DATA_.xml");
        final UploadDetails details = sequencer.createDestinationAndUpload(feedType,
                                                                           "text/xml",
                                                                           flatFileInventoryLoader
        );
        System.out.println("SHA256Sum = " + details.getSha256Sum() + " // UploadDestinationId = " + details.getUploadDestinationId());
        assertNotNull(details);
        assertNotNull(details.getUploadDestinationId());
    }

    @Test
    @Ignore
    public void makeItPass() {
        assertTrue(true);
    }

    private UploadsSequencer testSequencer() {
        return createSequencer("",
                "",
                "",
                "",
                ""
        );
    }

}
