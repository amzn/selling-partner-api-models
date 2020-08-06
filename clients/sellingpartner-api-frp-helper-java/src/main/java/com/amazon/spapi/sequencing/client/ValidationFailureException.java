package com.amazon.spapi.sequencing.client;


/**
 * Exception thrown if document integrity has been compromised during handling and is not considered safe to consume.
 */
public class ValidationFailureException extends RuntimeException {

    /**
     * Constructor taking both a message and cause.
     * @param message
     *            the message
     * @param cause
     *            the cause
     */
    public ValidationFailureException(String message, Throwable cause) {
        super(message, cause);
    }

    /**
     * Constructor taking only a message.
     * @param message
     *            the message
     */
    public ValidationFailureException(String message) {
        super(message);
    }

    /**
     * Constructor taking only a cause.
     * @param cause
     *            the cause
     */
    public ValidationFailureException(Throwable cause) {
        super(cause);
    }

}
