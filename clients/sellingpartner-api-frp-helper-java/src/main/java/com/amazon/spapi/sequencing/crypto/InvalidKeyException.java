package com.amazon.spapi.sequencing.crypto;

/**
 * InvalidKeyException indicates that a key was invalid.
 *
 */
public class InvalidKeyException extends CryptoException {

    /**
     * Constructor taking both a message and cause.
     * @param message the message
     * @param cause the cause
     */
    public InvalidKeyException(String message, Throwable cause) {
        super(message, cause);
    }

    /**
     * Constructor taking only a message.
     * @param message the message
     */
    public InvalidKeyException(String message) {
        super(message);
    }

    /**
     * Constructor taking only a cause.
     * @param cause the cause
     */
    public InvalidKeyException(Throwable cause) {
        super(cause);
    }

}
