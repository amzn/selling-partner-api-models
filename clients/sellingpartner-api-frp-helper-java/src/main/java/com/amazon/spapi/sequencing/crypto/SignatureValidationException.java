package com.amazon.spapi.sequencing.crypto;

/**
 * Indicates that the signature/hmac did not verify and that there was a mismatch.
 * This indicates either data corruption or a possible tamper attack.
 */
public class SignatureValidationException extends SignatureException {

    /**
     * Constructor taking both a message and cause.
     * @param message the message
     * @param cause the cause
     */
    public SignatureValidationException(String message, Throwable cause) {
        super(message, cause);
    }

    /**
     * Constructor taking only a message.
     * @param message the message
     */
    public SignatureValidationException(String message) {
        super(message);
    }

    /**
     * Constructor taking only a cause.
     * @param cause the cause
     */
    public SignatureValidationException(Throwable cause) {
        super(cause);
    }

}
