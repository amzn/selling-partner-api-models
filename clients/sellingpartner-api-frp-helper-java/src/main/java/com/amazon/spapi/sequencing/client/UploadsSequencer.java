package com.amazon.spapi.sequencing.client;

import io.swagger.client.ApiException;
import io.swagger.client.model.UploadDestination;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.InputStream;

/**
 * A thick client used to simplify the use of SP-API Uploads.
 */
public interface UploadsSequencer {
    /**
     * Retrieve a document and related metadata from SP-API Uploads.
     * @param obfuscatedCustomerId the obfuscated customer ID of the document owner.
     * @param documentId the document ID of the document being retrieved.
     * @return A DownloadBundle to be used to retrieve the requested document.
     * @throws RuntimeException if an exceptional case was encountered but the action should be retried.
     * @throws RuntimeException if an exceptional case was encountered and the action should not be retried.
     * @throws RuntimeException if an exceptional case was encountered and the cause was unexpected.
     */
   // DownloadBundle get(String obfuscatedCustomerId, String documentId) throws RuntimeException;

    UploadDestination createDestination(String feedType, String contentType, long documentLength)
            throws ApiException;
    /**
     * Delivers a document and related metadata to SP-API Uploads.
     * @param uploadDestination metadata for the document being uploaded.
     * @param contentType the content type
     * @param documentLength the length in bytes of the document.
     * @param inputStream the InputStream from which the document should be read for upload.
     * @return A String document ID.
     * @throws RuntimeException if an exceptional case was encountered but the action should be retried.
     * @throws RuntimeException if an exceptional case was encountered and the action should not be retried.
     * @throws RuntimeException if an exceptional case was encountered and the cause was unexpected.
     */
    UploadDetails uploadToDestination(UploadDestination uploadDestination, String contentType, long documentLength,
                                      InputStream inputStream) throws RuntimeException;

    UploadDetails createDestinationAndUpload(String feedType, String contentType, File fileName)
            throws ApiException, FileNotFoundException;

    UploadDetails createDestinationAndUpload(String feedType, String contentType, long documentLength,
                                             InputStream stream) throws ApiException;
}
