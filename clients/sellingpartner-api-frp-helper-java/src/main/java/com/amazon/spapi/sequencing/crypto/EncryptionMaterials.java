package com.amazon.spapi.sequencing.crypto;

import com.google.common.base.Preconditions;
import lombok.EqualsAndHashCode;
import lombok.Getter;

import javax.annotation.Nonnull;
import javax.annotation.Nullable;
import java.security.Key;
import java.util.Arrays;

/**
 * Key and initialization vector for use when encrypting things.
 */
@EqualsAndHashCode
public class EncryptionMaterials {

    @Nonnull
    @Getter
    private final Key key;
    @Nullable
    private final byte[] initializationVector;

    /**
     * All Args Constructor.
     * @param key non null Key for this EncryptionMaterials
     * @param initializationVector Nullable initializationVector (used for CBC mode encryption).
     */
    public EncryptionMaterials(@Nonnull Key key, @Nullable byte[] initializationVector) {
        Preconditions.checkArgument(key != null, "Key must be non-null.");

        this.key = key;
        this.initializationVector = copyInitializationVector(initializationVector);
    }

    /**
     * Get a copy of the initialization vector bytes.
     * @return A copy of the initialization vector bytes.
     */
    @Nullable
    public byte[] getInitializationVector() {
        return copyInitializationVector(initializationVector);
    }

    /**
     * A null-safe helper that copies the initialization vector bytes into a new array if the IV is non-null.
     * @param iv the byte array to copy
     * @return a copy of iv if iv is non null, otherwise null.
     */
    @Nullable
    private byte[] copyInitializationVector(byte[] iv) {
        byte[] newInitializationVector = null;
        if (iv != null) {
            newInitializationVector = Arrays.copyOf(iv, iv.length);
        }

        return newInitializationVector;
    }
}

