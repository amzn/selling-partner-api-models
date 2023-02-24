package com.amazon.SellingPartnerAPIAA;

import lombok.Builder;
import lombok.Data;

import com.amazonaws.auth.AWSCredentialsProvider;

/**
 * AWSAuthenticationCustomCredentialsProvider
 */
@Data
@Builder
public class AWSAuthenticationCustomCredentialsProvider {
    /**
     * AWS Region
     */
    private String region;

    /**
     * AWS Credentials Provider
     */
    private AWSCredentialsProvider awsCredentialsProvider;
}
