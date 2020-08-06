package com.amazon.spapi.sequencing.crypto;

/**
 * An exception that indicates an error occured during encryption or decryption of some data.
 */
public class EncryptionException extends CryptoException {

    /**
     * Constructor taking both a message and cause.
     * @param message the message
     * @param cause the cause
     */
    public EncryptionException(String message, Throwable cause) {
        super(message, cause);
    }

    /**
     * Constructor taking only a message.
     * @param message the message
     */
    public EncryptionException(String message) {
        super(message);
    }

    /**
     * Constructor taking only a cause.
     * @param cause the cause
     */
    public EncryptionException(Throwable cause) {
        super(cause);
    }
}
