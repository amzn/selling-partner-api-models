package com.amazon.spapi.sequencing.crypto;

import javax.annotation.Nonnull;
import javax.crypto.Cipher;

/**
 * This class of objects performs encryption, decryption, signing (or hmacing), and signature/hmac validation.
 */
public interface CryptoProvider {

    /**
     * Encrypt plain text into cipher text.
     * @param plainText the plain text to encrypt.
     * @param materials the EncryptionMaterials to use to perform the encryption
     * @return the cipherText
     * @throws EncryptionException if any problems occur during encryption.
     */
    @Nonnull
    byte[] encrypt(@Nonnull byte[] plainText, @Nonnull EncryptionMaterials materials) throws EncryptionException;

    /**
     * Decrypts the cipher text into plain text.
     * @param cipherText the cipher text to decrypt
     * @param materials the EncryptionMaterials to use to perform decryption
     * @return the plain text
     * @throws EncryptionException if any problems occur during decryption.
     */
    @Nonnull
    byte[] decrypt(@Nonnull byte[] cipherText, @Nonnull EncryptionMaterials materials) throws EncryptionException;

    /**
     * Create a signature or hmac based on the bytes provided.
     * @param bytesToSign the bytes to use when calculating the signature or hmac
     * @param materials the materials to use when generating the signature or hmac
     * @return the signature/hmac bytes
     * @throws SignatureGenerationException if any problems occur during signing/hmacing
     */
    @Nonnull
    byte[] sign(@Nonnull byte[] bytesToSign, @Nonnull EncryptionMaterials materials)
            throws SignatureGenerationException;

    /**
     * Validate that a signature/hmac matches the data that was signed.
     * @param signature the signature/hmac to validate
     * @param bytesToSign the data that was originally signed/hmac'd
     * @param materials the materials to use when validating the signature or hmac
     * @throws SignatureGenerationException if we are unable to generate the signature/hmac to validate against.
     * @throws SignatureValidationException if the validation fails (i.e, the signature or hmac do not match).
     * This indicates data corruption or a possible tampering attack.
     */
    @Nonnull
    void validateSignature(@Nonnull byte[] signature, @Nonnull byte[] bytesToSign,
                           @Nonnull EncryptionMaterials materials) throws SignatureGenerationException,
            SignatureValidationException;

    /**
     * Correctly Initialize a Cipher given the mode.  The mode is specified by {@link Cipher}'s Mode constants.
     * @param mode The mode of the cipher (i.e., encrypt or decrypt).  See {@link Cipher}'s Mode constants.
     * @param materials The EncryptionMaterials to initialize the cipher with
     * @return an initialized Cipher object
     * @throws EncryptionException if the Cipher could not be initialized.
     */
    @Nonnull
    Cipher getInitializedCipher(int mode, @Nonnull EncryptionMaterials materials) throws EncryptionException;

}
