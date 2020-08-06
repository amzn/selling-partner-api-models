package com.amazon.spapi.sequencing.client.impl;

import com.amazon.spapi.sequencing.crypto.CryptoProvider;
import com.amazon.spapi.sequencing.crypto.EncryptionException;
import com.amazon.spapi.sequencing.crypto.EncryptionMaterials;
import com.amazon.spapi.sequencing.crypto.SymmetricCryptoProvider;
import com.google.common.base.Preconditions;
import com.google.common.base.Strings;
import io.swagger.client.model.EncryptionDetails;
import lombok.AccessLevel;
import lombok.Builder;
import lombok.Getter;
import lombok.experimental.FieldDefaults;
import lombok.extern.apachecommons.CommonsLog;
import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.HttpStatus;
import org.apache.http.client.HttpResponseException;
import org.apache.http.util.EntityUtils;

import javax.crypto.CipherInputStream;
import javax.crypto.spec.SecretKeySpec;
import java.io.IOException;
import java.io.InputStream;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.Base64;
import java.util.Objects;

@Builder
@Getter
@FieldDefaults(makeFinal = true, level = AccessLevel.PRIVATE)
@CommonsLog
public class SequencerHelper {

    static final String SHA_256 = "SHA-256";
    static final String MD5 = "MD5";
    static final String S3_IAD_ENDPOINT = "s3-external-1.amazonaws.com";
    static final String S3_SEA_ENDPOINT = "s3-external-2.amazonaws.com";
    static final String S3_GET_TIME_METRIC = "S3GetTime";
    static final String S3_PUT_TIME_METRIC = "S3PutTime";
    static final String REQUIRED_KEY_ALGORITHM = "AES";
    static final int AES_BLOCK_SIZE = 16;
    static final Base64.Encoder BASE64_ENCODER = Base64.getEncoder();
    static final Base64.Decoder BASE64_DECODER = Base64.getDecoder();

    private CryptoProvider cryptoProvider;

    private HttpClientFactory httpClientFactory;

    public static SequencerHelper defaultSequencerHelper() {
        return SequencerHelper.builder()
                              .cryptoProvider(new SymmetricCryptoProvider())
                              .httpClientFactory(new SPAPIUploadDestinationsHttpClientFactory())
                              .build();
    }

    /**
     * Remove the query string from a signed S3 URL to hide access key, signature, expiration info when logging.
     *
     * @param signedS3Url Signed S3 URL to truncate
     * @return truncated URL
     */
    String removeQueryFromUrl(String signedS3Url) {
        if (signedS3Url == null) {
            return null;
        }

        try {
            URL url = new URL(signedS3Url);

            if (url.getQuery() == null) {
                return signedS3Url;
            } else {
                return signedS3Url.replace(url.getQuery(), "");
            }
        } catch (MalformedURLException e) {
            return null;
        }
    }


    /**
     * Wraps the provided InputStream in a CipherInputStream in DECRYPT mode.
     *
     * @param encryptionDetails The EncryptionInfo to use in constructing the Cipher.
     * @param stream            The InputStream to wrap.
     * @param mode              The cipher mode.
     * @return The wrapped InputStream
     * @throws EncryptionException if initializing the Cipher fails.
     */
    InputStream buildCipherInputStream(EncryptionDetails encryptionDetails, InputStream stream, int mode)
            throws EncryptionException {
        Preconditions.checkArgument(!Objects.isNull(encryptionDetails), "Encryption details missing");
        Preconditions.checkArgument(!Strings.isNullOrEmpty(encryptionDetails.getInitializationVector()),
                                    "iv is null or empty."
        );
        Preconditions.checkArgument(!Strings.isNullOrEmpty(encryptionDetails.getKey()), "key is null or empty.");
        Preconditions.checkState(cryptoProvider != null, "The CryptoProvider must be non-null.");

        String key = encryptionDetails.getKey();
        String iv = encryptionDetails.getInitializationVector();

        EncryptionMaterials materials = new EncryptionMaterials(new SecretKeySpec(BASE64_DECODER.decode(key),
                                                                                  REQUIRED_KEY_ALGORITHM
        ), BASE64_DECODER.decode(iv));
        return new CipherInputStream(stream, cryptoProvider.getInitializedCipher(mode, materials));
    }


    /**
     * Examine the HttpResponse object and throw appropriate exceptions for non-success status
     * codes.
     *
     * @param httpResponse the HttpResponse object to examine.
     * @throws RuntimeException if the status code is not 200 level or 400 level (except 403 errors).
     * @throws RuntimeException if the HttpResponse, or its nested StatusLine object are null. Or if the status
     *                          code is a 400 level HTTP response code (except 403 errors, which are retriable).
     */
    void handleErrorResponseCodes(HttpResponse httpResponse, AccessMechanism accessMechanism) throws RuntimeException {
        // Handle error responses
        if (httpResponse == null || httpResponse.getStatusLine() == null) {
            throw new RuntimeException("Unable to " + accessMechanism.getDescription() + " the document.");
        }
        int status = httpResponse.getStatusLine().getStatusCode();
        log.info(String.format("Document " + accessMechanism.getDescription() + " returned status code %d", status));
        int statusRange = status / 100;
        if (statusRange != 2) { // Not 2XX
            HttpEntity entity = httpResponse.getEntity();
            if (entity != null) {
                try {
                    log.warn(EntityUtils.toString(entity, "UTF-8"));
                } catch (IOException e) {
                    log.warn("Got IOException when trying to decode response body into a String", e);
                }
            }
            String reason = httpResponse.getStatusLine().getReasonPhrase();
            HttpResponseException error = new HttpResponseException(status, reason);
            // A 403 response is typically due to the signed URL expiring before use.
            // The root cause of the expiration is usually clock skew, so treat this as retriable.
            if (status == HttpStatus.SC_FORBIDDEN) {
                throw new RuntimeException(
                        String.format("A 403 response was returned. This typically indicates that the signed URL "
                                      + "expired before use (usually due to clock skew). %s %s", status, reason),
                        error
                );
            } else if (statusRange == 4) { // 4XX
                throw new RuntimeException(
                        String.format("A 400 level response was returned: %s %s", status, reason), error);
            } else { // 1XX, 3XX, 5XX
                throw new RuntimeException(
                        String.format("A non 200/400 level response was returned: %s %s", status, reason), error);
            }
        }
    }
}
