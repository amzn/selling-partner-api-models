package com.amazon.spapi;

import com.amazon.spapi.sequencing.client.DownloadsSequencer;
import com.amazon.spapi.sequencing.client.impl.DownloadsSequencerImpl;
import io.swagger.client.model.EncryptionDetails;
import io.swagger.client.model.UploadDestination;
import org.junit.Before;
import org.junit.Ignore;
import org.junit.Test;

import java.io.File;
import java.io.IOException;
import java.net.URL;

import static org.junit.Assert.assertNotNull;
import static org.junit.Assert.assertTrue;

public class ReportDownloadTest {

    private final DownloadsSequencer sequencer;

    private UploadDestination destinationToDownload;

    {
        sequencer = DownloadsSequencerImpl.builder().build();
        // Fill out the API sequencer with your credentials here.
    }


    static {
        System.setProperty("javax.net.ssl.trustStore",
                           getResourceFile("certs/InternalAndExternalTrustStore.jks").getPath()
        );
        System.setProperty("javax.net.ssl.trustStorePassword", "amazon");
    }


    @Before
    public void before() {
        // TODO: fill this out based on payload
        EncryptionDetails encryptionDetails = new EncryptionDetails().standard(EncryptionDetails.StandardEnum.AES)
                                                                     .initializationVector("")
                                                                     .key("");
        destinationToDownload = new UploadDestination().encryptionDetails(encryptionDetails)
                                                       .url("");
    }

    @Test
    // TODO: remove @Ignore when ready to test
    @Ignore
    public void downloadAndOutputDecryptedReport() throws IOException {
        File fileToOutputTo = new File("DecryptedReport.xml");
        sequencer.downloadDecryptThenWriteFile(destinationToDownload, false, fileToOutputTo);
    }

    @Test
    public void makeItPass() {
        assertTrue(true);
    }


    private static File getResourceFile(String fileName) {
        final URL fileUrl = DownloadsSequencerImpl.class.getClassLoader().getResource(fileName);
        assertNotNull(fileName + " cannot be reached", fileUrl);
        final File file = new File(fileUrl.getFile());
        assertTrue(file.exists());
        return file;
    }

}
