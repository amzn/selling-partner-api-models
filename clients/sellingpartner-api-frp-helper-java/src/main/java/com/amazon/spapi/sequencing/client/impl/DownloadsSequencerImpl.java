package com.amazon.spapi.sequencing.client.impl;

import com.amazon.spapi.sequencing.client.DownloadsSequencer;
import com.amazon.spapi.sequencing.crypto.EncryptionException;
import com.google.common.base.Strings;

import io.swagger.client.model.EncryptionDetails;
import io.swagger.client.model.UploadDestination;
import lombok.AccessLevel;
import lombok.Builder;
import lombok.experimental.FieldDefaults;
import lombok.extern.apachecommons.CommonsLog;
import org.apache.commons.io.IOUtils;
import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.HttpStatus;
import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.HttpClient;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.util.EntityUtils;

import javax.crypto.Cipher;
import java.io.*;
import java.util.regex.Pattern;
import java.util.zip.GZIPInputStream;

import static com.amazon.spapi.sequencing.client.impl.SequencerHelper.S3_IAD_ENDPOINT;
import static com.amazon.spapi.sequencing.client.impl.SequencerHelper.S3_SEA_ENDPOINT;

@Builder
@FieldDefaults(makeFinal = true, level = AccessLevel.PRIVATE)
@CommonsLog
public class DownloadsSequencerImpl implements DownloadsSequencer {
    @Builder.Default
    private final SequencerHelper helper = SequencerHelper.defaultSequencerHelper();

    @Override
    public InputStream downloadAndDecryptReportContent(UploadDestination destination, boolean isGzipped) {
        boolean hasError = false;
        InputStream result = null;
        String truncatedUrl = null;
        try {

            String url = destination.getUrl();

            if (Strings.isNullOrEmpty(url)) {
                throw new RuntimeException("The returned download URL is null or empty.");
            }

            // Acquire the file
            HttpClient httpClient = helper.getHttpClientFactory().newGetClient();
            HttpGet httpGet = new HttpGet(url);

            truncatedUrl = helper.removeQueryFromUrl(url);
            log.info(String.format("Downloading document with destination ID %s using URL %s (signing info redacted)",
                                   destination.getUploadDestinationId(), truncatedUrl
            ));
            HttpResponse httpResponse = httpClient.execute(httpGet);
            if (httpResponse != null && httpResponse.getStatusLine().getStatusCode() == HttpStatus.SC_NOT_FOUND) {
                boolean isS3UsStandardRegion = false;
                if (url.contains(S3_IAD_ENDPOINT)) {
                    isS3UsStandardRegion = true;
                    url = url.replaceFirst(Pattern.quote(S3_IAD_ENDPOINT), S3_SEA_ENDPOINT);
                } else if (url.contains(S3_SEA_ENDPOINT)) {
                    isS3UsStandardRegion = true;
                    url = url.replaceFirst(Pattern.quote(S3_SEA_ENDPOINT), S3_IAD_ENDPOINT);
                }

                if (isS3UsStandardRegion) {
                    log.info("S3 Returned a 404 in the US Standard Region."
                             + "  This likely indicates issues with eventual consistency and should be rare."
                             + "  Attempting to find object in peer region."
                             + "  See: https://w.amazon.com/index.php/S3#What_About_Eventual_Consistency.3F");

                    // Consume the previous response so that the HTTP client connection can be reused
                    EntityUtils.consumeQuietly(httpResponse.getEntity());

                    httpGet = new HttpGet(url);
                    httpResponse = httpClient.execute(httpGet);
                }
            }

            // Handle error responses
            helper.handleErrorResponseCodes(httpResponse, AccessMechanism.DOWNLOAD);

            HttpEntity entity = httpResponse.getEntity();
            if (entity == null) {
                throw new RuntimeException("The HTTP store returned success but no document.");
            }
            result = entity.getContent();
            result = decryptReportContent(entity.getContent(), destination.getEncryptionDetails(), isGzipped);
            return result;
        } catch (ClientProtocolException e) {
            // Indicates an error in the HTTP protocol
            hasError = true;
            throw new RuntimeException(
                    "Unable to download the file.", e);
        } catch (EncryptionException e) {
            // Unable to perform cipher operations
            hasError = true;
            throw new RuntimeException(
                    "Unable to perform cipher operations.", e);
        } catch (IOException e) {
            hasError = true;
            // Unable to download the file or operate on the stream
            throw new RuntimeException(
                    String.format("Unable to download the file using URL %s (signing info redacted) "
                                  + "or manipulate the InputStream.", truncatedUrl),
                    e
            );
        } finally {
            // kill the stream if we are throwing an exception
            if (hasError) {
                IOUtils.closeQuietly(result);
            }
        }
    }

    @Override
    public InputStream decryptReportContent(File reportFile, EncryptionDetails details, boolean isGzipped)
            throws IOException, EncryptionException {
        return decryptReportContent(new FileInputStream(reportFile), details, isGzipped);
    }

    private InputStream decryptReportContent(InputStream input, EncryptionDetails details, boolean isGzipped)
            throws EncryptionException, IOException {
        InputStream resultStream = input;
        // If encrypted, decipher the stream
        if (details != null && EncryptionDetails.StandardEnum.AES.equals(details.getStandard())) {
            try {
                resultStream = helper.buildCipherInputStream(details, resultStream, Cipher.DECRYPT_MODE);

            } catch (IllegalArgumentException | IllegalStateException e) {
                throw new EncryptionException(e);
            }
        }

        // Determine if the stream should be unzipped as well
        if (isGzipped) {
            resultStream = new GZIPInputStream(resultStream);
        }

        return resultStream;
    }

    @Override
    public void downloadDecryptThenWriteFile(UploadDestination destination, boolean isGzipped, File fileToWriteTo)
            throws IOException {
        try (
                InputStream result = downloadAndDecryptReportContent(destination, isGzipped);
                FileOutputStream output = new FileOutputStream(fileToWriteTo);
        ) {
            IOUtils.copyLarge(result, output);
        }
    }

    @Override
    public void decryptThenWriteFile(File reportFile, EncryptionDetails details, boolean isGzipped,
                                     File fileToWriteTo) throws IOException, EncryptionException {
        try (
                InputStream result = decryptReportContent(reportFile,details, isGzipped);
                FileOutputStream output = new FileOutputStream(fileToWriteTo);
        ) {
            IOUtils.copyLarge(result, output);
        }

    }

    /*



     */
    /** {@inheritDoc} *//*

    @Override
    public DownloadBundle get(String obfuscatedCustomerId, String documentId)
            throws RuntimeException, RuntimeException, RuntimeException {
        Preconditions.checkArgument(!Strings.isNullOrEmpty(obfuscatedCustomerId),
                                    "obfuscatedMerchantCustomerId is null or empty.");
        Preconditions.checkArgument(!Strings.isNullOrEmpty(documentId), "documentId is null or empty.");
        Preconditions.checkState(httpClientFactory != null, "The httpClientFactory is null.");

        // We need the expanded scope so that we can close the stream in the case of an exception.
        InputStream resultStream = null;
        DownloadBundle result = null;
        String truncatedUrl = null;
        try {
            // Acquire the document metadata, document receipt URL and encryption details from Tortuga.
            GetDownloadUrlInput getUrlInput = new GetDownloadUrlInput();
            getUrlInput.setDocumentId(documentId);
            getUrlInput.setObfuscatedCustomerId(obfuscatedCustomerId);
            GetDownloadUrlOutput getUrlOutput =
                    GetDownloadUrlSyncJobBuilder.build(getUrlInput, metricsStrategy, client).run();
            DocumentMetadata metadata = getUrlOutput.getDocumentMetadata();
            if (metadata == null) {
                throw new RuntimeException("The returned document metadata is null.");
            }
            String url = getUrlOutput.getUrl();
            if (Strings.isNullOrEmpty(url)) {
                throw new RuntimeException("The returned download URL is null or empty.");
            }
            Date creationTimestamp = getUrlOutput.getCreationTimestamp();
            if (creationTimestamp == null) {
                throw new RuntimeException("The returned document creationDate is null.");
            }

            getUrlOutput.setDocumentMetadata(null);

            // Acquire the file
            HttpClient httpClient = httpClientFactory.newGetClient();
            HttpGet httpGet = new HttpGet(url);

            truncatedUrl = removeQueryFromUrl(url);
            log.info(String.format("Downloading document with ID %s using URL %s (signing info redacted)",
                    documentId, truncatedUrl));
            long startTimeMillis = System.nanoTime() / 1000000L;
            HttpResponse httpResponse = httpClient.execute(httpGet);
            if (httpResponse != null && httpResponse.getStatusLine().getStatusCode() == HttpStatus.SC_NOT_FOUND) {
                boolean isS3UsStandardRegion = false;
                if (url.contains(S3_IAD_ENDPOINT)) {
                    isS3UsStandardRegion = true;
                    url = url.replaceFirst(Pattern.quote(S3_IAD_ENDPOINT), S3_SEA_ENDPOINT);
                } else if (url.contains(S3_SEA_ENDPOINT)) {
                    isS3UsStandardRegion = true;
                    url = url.replaceFirst(Pattern.quote(S3_SEA_ENDPOINT), S3_IAD_ENDPOINT);
                }

                if (isS3UsStandardRegion) {
                    log.info("S3 Returned a 404 in the US Standard Region."
                            + "  This likely indicates issues with eventual consistency and should be rare."
                            + "  Attempting to find object in peer region."
                            + "  See: https://w.amazon.com/index.php/S3#What_About_Eventual_Consistency.3F");

                    // Consume the previous response so that the HTTP client connection can be reused
                    EntityUtils.consumeQuietly(httpResponse.getEntity());

                    httpGet = new HttpGet(url);
                    httpResponse = httpClient.execute(httpGet);
                }
            }
            long endTimeMillis = System.nanoTime() / 1000000L;
            serviceMetrics.addTime(S3_GET_TIME_METRIC, endTimeMillis - startTimeMillis, SI.MILLI(SI.SECOND));

            // Handle error responses
            handleErrorResponseCodes(httpResponse);

            HttpEntity entity = httpResponse.getEntity();
            if (entity == null) {
                throw new RuntimeException("The HTTP store returned success but no document.");
            }
            resultStream = entity.getContent();

            // If this document has a sha256sum in the metadata, wrap with DigestInputStream to calculate the sha256 sum
            // for validation of document integrity
            String sha256sumStr = getUrlOutput.getSha256sum();
            if (sha256sumStr != null) {
                resultStream = new DocumentValidatorInputStream(resultStream, BASE64_DECODER.decode(sha256sumStr));
            }

            // If encrypted, decipher the stream
            if (getUrlOutput.isEncrypted()) {
                try {
                    EncryptionInfo encryptionInfo = getUrlOutput.getEncryptionInfo();
                    resultStream = buildCipherInputStream(encryptionInfo, resultStream, Cipher.DECRYPT_MODE);

                } catch (IllegalArgumentException e) {
                    throw new RuntimeException(e);
                } catch (IllegalStateException e) {
                    throw new RuntimeException(e);
                }
            }

            // Determine if the stream should be unzipped as well
            if (metadata.isDocumentGzipped()) {
                resultStream = new GZIPInputStream(resultStream);
            }

            // build the result
            result = new DownloadBundle(metadata, resultStream, creationTimestamp.getTime());
        } catch (ClientProtocolException e) {
            // Indicates an error in the HTTP protocol
            throw new RuntimeException(
                    "Unable to download the file.", e);
        } catch (EncryptionException e) {
            // Unable to perform cipher operations
            throw new RuntimeException(
                    "Unable to perform cipher operations.", e);
        } catch (NoSuchAlgorithmException e) {
            throw new IllegalStateException("Couldn't get a Sha256 Message Digest instance", e);
        } catch (IOException e) {
            // Unable to download the file or operate on the stream
            throw new RuntimeException(
                    String.format("Unable to download the file using URL %s (signing info redacted) "
                            + "or manipulate the InputStream.", truncatedUrl),
                    e);
        } finally {
            // kill the stream if we are throwing an exception
            if (result == null) {
                IOUtils.closeQuietly(resultStream);
            }
        }
        return result;
    }
*/

}
