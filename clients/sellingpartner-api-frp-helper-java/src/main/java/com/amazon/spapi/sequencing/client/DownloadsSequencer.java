package com.amazon.spapi.sequencing.client;

import com.amazon.spapi.sequencing.crypto.EncryptionException;
import io.swagger.client.model.EncryptionDetails;
import io.swagger.client.model.UploadDestination;

import java.io.File;
import java.io.IOException;
import java.io.InputStream;

public interface DownloadsSequencer {

    InputStream downloadAndDecryptReportContent(UploadDestination destination, boolean isGzipped) throws IOException;

    InputStream decryptReportContent(File reportFile, EncryptionDetails details, boolean isGzipped)
            throws IOException, EncryptionException;

    void downloadDecryptThenWriteFile(UploadDestination destination, boolean isGzipped, File fileToWriteTo)
            throws IOException;

    void decryptThenWriteFile(File reportFile, EncryptionDetails details, boolean isGzipped, File fileToWriteTo)
            throws IOException, EncryptionException;
}
