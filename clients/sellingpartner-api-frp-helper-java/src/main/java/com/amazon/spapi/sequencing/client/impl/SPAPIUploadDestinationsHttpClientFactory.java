package com.amazon.spapi.sequencing.client.impl;

import com.google.common.collect.ImmutableMap;
import lombok.Setter;
import org.apache.http.client.HttpClient;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.params.CoreConnectionPNames;
import org.apache.http.params.CoreProtocolPNames;

import java.util.Map;

/**
 * HttpClientFactory implementation for SP-API Upload Destinations.
 */
@Setter
public class SPAPIUploadDestinationsHttpClientFactory implements HttpClientFactory {
    private static final Map<String, Object> DEFAULT_CONNECTION_PARAMETERS;

    static {
        ImmutableMap.Builder<String, Object> builder = ImmutableMap.builder();
        builder.put(CoreConnectionPNames.CONNECTION_TIMEOUT, 10000);
        builder.put(CoreConnectionPNames.SO_TIMEOUT, 10000);
        builder.put(CoreConnectionPNames.SO_KEEPALIVE, Boolean.TRUE);

        DEFAULT_CONNECTION_PARAMETERS = builder.build();
    }

    /**
     *http://hc.apache.org/httpcomponents-core-4.2.x/httpcore/apidocs/org/apache/http/params/CoreConnectionPNames.html.
     */
    private Map<String, Object> getClientConnectionParametersOverrides = DEFAULT_CONNECTION_PARAMETERS;

    /**
     *http://hc.apache.org/httpcomponents-core-4.2.x/httpcore/apidocs/org/apache/http/params/CoreConnectionPNames.html.
     */
    private Map<String, Object> putClientConnectionParametersOverrides = DEFAULT_CONNECTION_PARAMETERS;

    private String userAgent;

    /** {@inheritDoc} */
    @Override
    public HttpClient newGetClient() {
        return newHttpClient(getClientConnectionParametersOverrides);
    }

    /** {@inheritDoc} */
    @Override
    public HttpClient newPutClient() {
        return newHttpClient(putClientConnectionParametersOverrides);
    }

    /**
     * Helper function to create a new DefaultHttpClient with the given connectionParameter Overrides.
     * @param connectionParameters used to override the default properties of the HttpClient.
     * @return a new, configured HttpClient.
     */
    private HttpClient newHttpClient(Map<String, Object> connectionParameters) {
        // TODO: evaluate necessity
        // Preconditions.checkState(!Strings.isNullOrEmpty(userAgent), "You must supply a User-Agent.");

        DefaultHttpClient client = new DefaultHttpClient();
        for (Map.Entry<String, Object> param : connectionParameters.entrySet()) {
            client.getParams().setParameter(param.getKey(), param.getValue());
        }

        if (userAgent != null) {
            client.getParams().setParameter(CoreProtocolPNames.USER_AGENT, userAgent);
        }
        return client;
    }
}
