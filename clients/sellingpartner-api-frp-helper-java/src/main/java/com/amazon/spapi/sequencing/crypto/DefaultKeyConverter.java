package com.amazon.spapi.sequencing.crypto;

import com.google.common.base.Preconditions;

import javax.annotation.Nonnull;
import javax.crypto.SecretKey;
import javax.crypto.spec.SecretKeySpec;
import java.security.*;
import java.security.spec.InvalidKeySpecException;
import java.security.spec.PKCS8EncodedKeySpec;
import java.security.spec.X509EncodedKeySpec;

/**
 * Default implementation of the {@link KeyConverter} interface.
 */
public class DefaultKeyConverter implements KeyConverter {
    private static final String SECRET_KEY_ALGORITHM = "AES";
    private static final String KEYPAIR_ALGORITHM = "RSA";

    /**
     * {@inheritDoc}
     */
    @Nonnull
    @Override
    public SecretKey convertToSymmetricKey(@Nonnull byte[] material) {
        Preconditions.checkArgument(material != null, "material must be non-null.");
        return new SecretKeySpec(material, SECRET_KEY_ALGORITHM);
    }

    /**
     * {@inheritDoc}
     */
    @Nonnull
    @Override
    public PrivateKey convertToPrivateKey(@Nonnull byte[] material) throws com.amazon.spapi.sequencing.crypto.InvalidKeyException {
        Preconditions.checkArgument(material != null, "material must be non-null.");

        PKCS8EncodedKeySpec keySpec = new PKCS8EncodedKeySpec(material);
        try {
            KeyFactory factory = KeyFactory.getInstance(KEYPAIR_ALGORITHM);
            return factory.generatePrivate(keySpec);
        } catch (NoSuchAlgorithmException e) {
            throw new IllegalStateException("RSA Algorithm is invalid.  This indicates a badly configured Provider.");
        } catch (InvalidKeySpecException e) {
            throw new com.amazon.spapi.sequencing.crypto.InvalidKeyException("Invalid key.", e);
        }
    }

    /**
     * {@inheritDoc}
     */
    @Nonnull
    @Override
    public PublicKey convertToPublicKey(@Nonnull byte[] material) throws com.amazon.spapi.sequencing.crypto.InvalidKeyException {
        Preconditions.checkArgument(material != null, "material must be non-null.");

        X509EncodedKeySpec keySpec = new X509EncodedKeySpec(material);
        try {
            KeyFactory factory = KeyFactory.getInstance(KEYPAIR_ALGORITHM);
            return factory.generatePublic(keySpec);
        } catch (NoSuchAlgorithmException e) {
            throw new IllegalStateException("RSA Algorithm is invalid.  This indicates a badly configured Provider.");
        } catch (InvalidKeySpecException e) {
            throw new com.amazon.spapi.sequencing.crypto.InvalidKeyException("Invalid key.", e);
        }
    }

    /**
     * {@inheritDoc}
     */
    @Nonnull
    @Override
    public KeyPair convertToKeyPair(@Nonnull byte[] publicMaterial, @Nonnull byte[] privateMaterial)
            throws InvalidKeyException {
        return new KeyPair(convertToPublicKey(publicMaterial), convertToPrivateKey(privateMaterial));
    }
}
