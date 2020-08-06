package com.amazon.spapi.sequencing.crypto;

import javax.annotation.Nonnull;
import javax.crypto.SecretKey;
import java.security.KeyPair;
import java.security.PrivateKey;
import java.security.PublicKey;

/**
 * A class of objects that converts the raw bytes of an encoded key into a Key.
 */
public interface KeyConverter {

    /**
     * Convert a byte[] into a SecretKey.
     * @param material the byte[] to covert
     * @return the SecretKey
     */
    @Nonnull
    SecretKey convertToSymmetricKey(@Nonnull byte[] material);

    /**
     * Convert a PKCS8 encoded byte[] into a PrivateKey.
     * @param material the byte[] to covert
     * @return the PrivateKey
     * @throws InvalidKeyException if the byte[] is not a properly encoded key
     */
    @Nonnull
    PrivateKey convertToPrivateKey(@Nonnull byte[] material) throws InvalidKeyException;

    /**
     * Convert a X509 encoded byte[] into a PublicKey.
     * @param material the byte[] to covert
     * @return the PublicKey
     * @throws InvalidKeyException if the byte[] is not a properly encoded key
     */
    @Nonnull
    PublicKey convertToPublicKey(@Nonnull byte[] material) throws InvalidKeyException;

    /**
     * Convert a X509 encoded byte[] and a PKCS8 encoded byte[] into a KeyPair.
     * @param publicMaterial the X509 encoded byte[] to covert into the public key
     * @param privateMaterial the PKCS8 encoded byte[] to covert into the private key
     * @return the SecretKey
     * @throws InvalidKeyException if either the public or private material is invalid
     */
    @Nonnull
    KeyPair convertToKeyPair(@Nonnull byte[] publicMaterial, @Nonnull byte[] privateMaterial)
            throws InvalidKeyException;
}
