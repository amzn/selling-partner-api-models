package com.amazon.spapi.sequencing.client;

import com.google.common.base.Preconditions;
import lombok.extern.apachecommons.CommonsLog;

import java.io.FilterInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.security.DigestInputStream;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.util.Arrays;

/**
 * An input stream that validates the integrity of the document when you close the stream.
 */
@CommonsLog
public class DocumentValidatorInputStream extends FilterInputStream {

    private final byte[] expectedSha256sum;
    private final MessageDigest sha256digest;

    /**
     * Creates an InputStream that will calculate the sha256digest and validate it upon closing the stream.
     * @param inputStream
     *            the inputStream to calculate the sha256sum of.
     * @param expectedSha256sum
     *            the expected sha256sum of the document being streamed if one exists. Leave null if document does not
     *            have a sha256sum to validate against.
     * @throws NoSuchAlgorithmException
     *             when unable to create a SHA-256 instance of MessageDigest
     */
    public DocumentValidatorInputStream(InputStream inputStream, byte[] expectedSha256sum)
            throws NoSuchAlgorithmException {
        super(null);
        Preconditions.checkArgument(inputStream != null, "inputStream must not be null");
        Preconditions.checkArgument(expectedSha256sum != null, "expectedSha256sum must not be null");

        this.expectedSha256sum = Arrays.copyOf(expectedSha256sum, expectedSha256sum.length);
        sha256digest = MessageDigest.getInstance("SHA-256");
        this.in = new DigestInputStream(inputStream, sha256digest);
    }

    /**
     * Validates that the streamed document was not tampered with.
     * @throws IOException
     *             if the streamed document was completely read and is not valid or an IOException occurs while closing
     *             the stream.
     */
    @Override
    public void close() throws IOException {
        try {
            if (!MessageDigest.isEqual(expectedSha256sum, sha256digest.digest()) && super.read() == -1) {
                throw new ValidationFailureException("The streamed document was not valid.");
            }
        } finally {
            try {
                super.close();
            } catch (IOException e) {
                log.warn(String.format("Exception while closing the validation string: %s", e.getMessage()), e);
            }
        }
    }
}
