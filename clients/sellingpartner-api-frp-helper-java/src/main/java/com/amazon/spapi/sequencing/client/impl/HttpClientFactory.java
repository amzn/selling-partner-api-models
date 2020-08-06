package com.amazon.spapi.sequencing.client.impl;

import org.apache.http.client.HttpClient;

/**
 * Constructs purpose built HttpClient instances.
 */
public interface HttpClientFactory {
    /**
     * Constructs an HttpClient instance configured for GET requests.
     * @return an HttpClient instance.
     */
    HttpClient newGetClient();
    /**
     * Constructs an HttpClient instance configured for PUT requests.
     * @return an HttpClient instance.
     */
    HttpClient newPutClient();
}
