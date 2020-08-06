package com.amazon.spapi.sequencing.crypto;


/**
 * Base class for any Cryptographic exception that occurs.
 */
public class CryptoException extends Exception {

    /**
     * Constructor taking both a message and cause.
     * @param message the message
     * @param cause the cause
     */
    public CryptoException(String message, Throwable cause) {
        super(message, cause);
    }

    /**
     * Constructor taking only a message.
     * @param message the message
     */
    public CryptoException(String message) {
        super(message);
    }

    /**
     * Constructor taking only a cause.
     * @param cause the cause
     */
    public CryptoException(Throwable cause) {
        super(cause);
    }

}
