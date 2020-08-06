package com.amazon.SellingPartnerAPIAA;

import com.amazonaws.SignableRequest;
import com.amazonaws.auth.AWS4Signer;
import com.amazonaws.auth.AWSCredentials;
import com.amazonaws.auth.BasicAWSCredentials;
import com.squareup.okhttp.Request;
import lombok.AccessLevel;
import lombok.Getter;
import lombok.Setter;

/**
 * AWS Signature Version 4 Signer
 */
public class AWSSigV4Signer {
    private static final String SERVICE_NAME = "execute-api";

    @Setter(AccessLevel.PACKAGE)
    @Getter(AccessLevel.PACKAGE)
    private AWS4Signer aws4Signer;

    private AWSCredentials awsCredentials;

    /**
     *
     * @param awsAuthenticationCredentials AWS Developer Account Credentials
     */
    public AWSSigV4Signer(AWSAuthenticationCredentials awsAuthenticationCredentials) {
        aws4Signer = new AWS4Signer();
        aws4Signer.setServiceName(SERVICE_NAME);
        aws4Signer.setRegionName(awsAuthenticationCredentials.getRegion());
        awsCredentials = new BasicAWSCredentials(awsAuthenticationCredentials.getAccessKeyId(),
                awsAuthenticationCredentials.getSecretKey());
    }

    /**
     *  Signs a Request with AWS Signature Version 4
     *
     * @param originalRequest Request to sign (treated as immutable)
     * @return Copy of originalRequest with AWS Signature
     */
    public Request sign(Request originalRequest) {
        SignableRequest<Request> signableRequest = new SignableRequestImpl(originalRequest);
        aws4Signer.sign(signableRequest, awsCredentials);

        return (Request) signableRequest.getOriginalRequestObject();
    }
}
