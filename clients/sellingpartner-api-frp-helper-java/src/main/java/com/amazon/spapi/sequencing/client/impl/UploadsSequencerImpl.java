package com.amazon.spapi.sequencing.client.impl;

import com.amazon.spapi.sequencing.client.UploadDetails;
import com.amazon.spapi.sequencing.client.UploadsSequencer;
import com.amazon.spapi.sequencing.crypto.EncryptionException;
import com.google.common.base.Preconditions;
import com.google.common.base.Strings;
import io.swagger.client.ApiException;
import io.swagger.client.api.UploadsApi;
import io.swagger.client.model.CreateUploadDestinationResponse;
import io.swagger.client.model.EncryptionDetails;
import io.swagger.client.model.UploadDestination;
import lombok.AccessLevel;
import lombok.Builder;
import lombok.experimental.FieldDefaults;
import lombok.extern.apachecommons.CommonsLog;
import org.apache.commons.io.IOUtils;
import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.HttpClient;
import org.apache.http.client.methods.HttpPut;
import org.apache.http.entity.InputStreamEntity;

import javax.crypto.Cipher;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.InputStream;
import java.security.DigestInputStream;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;

/**
 * This implementation performs the expected crypto and compression work on the provided streams.
 */
@Builder
@FieldDefaults(makeFinal = true, level = AccessLevel.PRIVATE)
@CommonsLog
public class UploadsSequencerImpl implements UploadsSequencer {

    @Builder.Default
    private final SequencerHelper helper = SequencerHelper.defaultSequencerHelper();

    private UploadsApi uploadsApi;

    /**
     * {@inheritDoc}
     */
    @Override
    public UploadDetails uploadToDestination(UploadDestination uploadDestination, String contentType,
                                             long documentLength,
                                             InputStream inputStream) throws RuntimeException {
        Preconditions.checkArgument(uploadDestination != null, "documentMetadata is null.");
        Preconditions.checkArgument(inputStream != null, "inputStream is null.");
        Preconditions.checkArgument(documentLength >= 0, "documentLength must be at least zero.");
        Preconditions.checkState(helper.getHttpClientFactory() != null, "The httpClientFactory is null.");

        String truncatedUrl = null;
        try {

            final String uploadDestinationId = uploadDestination.getUploadDestinationId();
            if (Strings.isNullOrEmpty(uploadDestinationId)) {
                throw new RuntimeException("The returned uploadDestinationId is null or empty.");
            }
            EncryptionDetails encryptionDetails = uploadDestination.getEncryptionDetails();
            if (encryptionDetails == null) {
                throw new RuntimeException("The returned encryptionDetails is null.");
            }
            final String url = uploadDestination.getUrl();
            if (Strings.isNullOrEmpty(url)) {
                throw new RuntimeException("The returned upload URL is null or empty.");
            }

            // Wrap the stream with a CipherStream
            inputStream = helper.buildCipherInputStream(encryptionDetails, inputStream, Cipher.ENCRYPT_MODE);

            // Wrap the cipher stream with a sha256 digest stream.
            DigestInputStream sha256sumStream = new DigestInputStream(inputStream, MessageDigest.getInstance(
                    SequencerHelper.SHA_256));
            inputStream = sha256sumStream;

            truncatedUrl = helper.removeQueryFromUrl(url);

            // Put the file
            HttpClient httpClient = helper.getHttpClientFactory().newPutClient();
            HttpPut httpPut = new HttpPut(url);
            // This content length calculation is specific to AES
            long contentLength = (documentLength / SequencerHelper.AES_BLOCK_SIZE + 1) * SequencerHelper.AES_BLOCK_SIZE;
            // This sets the Content-Length header since we specified the contentLength
            HttpEntity document = new InputStreamEntity(inputStream, contentLength);
            httpPut.setHeader("Content-Type", contentType);
            httpPut.setEntity(document);

            log.info(String.format("Uploading document with ID %s using URL %s (signing info redacted)",
                                   uploadDestinationId, truncatedUrl
            ));
            HttpResponse httpResponse = httpClient.execute(httpPut);

            // Handle error responses
            helper.handleErrorResponseCodes(httpResponse, AccessMechanism.UPLOAD);
            log.info(String.format("Successfully uploaded document with ID %s", uploadDestinationId));

            byte[] sha256sumDigest = sha256sumStream.getMessageDigest().digest();
            String sha256sum = SequencerHelper.BASE64_ENCODER.encodeToString(sha256sumDigest);


            return UploadDetails.builder()
                                .sha256Sum(sha256sum)
                                .uploadDestinationId(uploadDestinationId)
                                .build();
        } catch (ClientProtocolException e) {
            // Indicates an error in the HTTP protocol
            throw new RuntimeException("Unable to upload the file.", e);
        } catch (EncryptionException e) {
            // Unable to perform cipher operations
            throw new RuntimeException("Unable to perform cipher operations.", e);
        } catch (IOException e) {
            // Unable to upload the file or operate on the stream
            throw new RuntimeException(
                    String.format("Unable to upload the file using URL %s (signing info redacted) "
                                  + "or manipulate the InputStream.", truncatedUrl),
                    e
            );
        } catch (NoSuchAlgorithmException e) {
            // Unable to get a MAC on the encrypted doc
            throw new IllegalStateException("Unable to create MessageDigest. Required algorithm not present.", e);
        } finally {
            IOUtils.closeQuietly(inputStream);
        }
    }

    @Override
    public UploadDetails createDestinationAndUpload(String feedType, String contentType, File fileName)
            throws FileNotFoundException, ApiException {
        long documentLength = fileName.length();

        return createDestinationAndUpload(feedType, contentType, documentLength, new FileInputStream(fileName));
    }

    @Override
    public UploadDetails createDestinationAndUpload(String feedType, String contentType, long documentLength,
                                                    InputStream stream) throws ApiException {

        UploadDestination destination = createDestination(feedType, contentType, documentLength);

        return uploadToDestination(destination, contentType, documentLength, stream);
    }

    public UploadDestination createDestination(String feedType, String contentType, long documentLength)
            throws ApiException {
        Preconditions.checkArgument(documentLength < (long) Integer.MAX_VALUE,
                                    "Document length cannot exceed " + Integer.MAX_VALUE + " bytes."
        );
        CreateUploadDestinationResponse response = uploadsApi.createUploadDestinationForFeed(feedType, contentType,
                                                                                             (int) documentLength
        );
        return response.getPayload();
    }
/*
 TODO

    public void submitFeed(UploadDetails uploadDetails) {

        CompleteDocumentUploadInput completeUploadInput = new CompleteDocumentUploadInput();
        completeUploadInput.setDocumentId(uploadDestinationId);
        completeUploadInput.setObfuscatedCustomerId(uploadDestination.getObfuscatedCustomerId());
        completeUploadInput.setCompleteUploadToken(getUploadUrlOutput.getCompleteUploadToken());
        completeUploadInput.setSha256sum(sha256sum);
        completeUploadInput.setContentMd5(contentMd5);
        CompleteDocumentUploadSyncJobBuilder.build(completeUploadInput, metricsStrategy, client).runOnce();

    }
*/


}
