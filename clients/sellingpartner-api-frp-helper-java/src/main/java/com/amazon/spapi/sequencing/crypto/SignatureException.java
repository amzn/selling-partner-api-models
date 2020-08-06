package com.amazon.spapi.sequencing.crypto;

/**
 * Base class for exceptions related to Signature/HMAC problems.
 */
public class SignatureException extends CryptoException {

    /**
     * Constructor taking both a message and cause.
     * @param message the message
     * @param cause the cause
     */
    public SignatureException(String message, Throwable cause) {
        super(message, cause);
    }

    /**
     * Constructor taking only a message.
     * @param message the message
     */
    public SignatureException(String message) {
        super(message);
    }

    /**
     * Constructor taking only a cause.
     * @param cause the cause
     */
    public SignatureException(Throwable cause) {
        super(cause);
    }

}
