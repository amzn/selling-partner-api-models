package com.amazon.spapi.sequencing.client;

import java.io.IOException;

/**
 * Exception thrown when spooling size limits exceeded.
 */
public class SpoolingLimitExceededException extends IOException {

    /**
     * Constructor that takes in a message.
     * @param message
     *            the message describing the exception
     */
    public SpoolingLimitExceededException(String message) {
        super(message);
    }

}
