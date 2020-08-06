package com.amazon.spapi.sequencing.crypto;

/**
 * Indicates that an error occured when generating a Signature or HMAC.
 */
public class SignatureGenerationException extends SignatureException {

    /**
     * Constructor taking both a message and cause.
     * @param message the message
     * @param cause the cause
     */
    public SignatureGenerationException(String message, Throwable cause) {
        super(message, cause);
    }

    /**
     * Constructor taking only a message.
     * @param message the message
     */
    public SignatureGenerationException(String message) {
        super(message);
    }

    /**
     * Constructor taking only a cause.
     * @param cause the cause
     */
    public SignatureGenerationException(Throwable cause) {
        super(cause);
    }

}
