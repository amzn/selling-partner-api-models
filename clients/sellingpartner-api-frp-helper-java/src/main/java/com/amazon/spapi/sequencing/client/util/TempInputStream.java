package com.amazon.spapi.sequencing.client.util;

import lombok.extern.apachecommons.CommonsLog;
import org.apache.commons.io.FileUtils;
import org.apache.commons.io.IOUtils;

import java.io.*;

/**
 * An InputStream from a temporary file that deletes the file after it was completely read or closed.
 */
@CommonsLog
public class TempInputStream extends InputStream {

    private final FileInputStream fileStream;
    private final File tempFile;

    /**
     * Creates a new {@code TempInputStream} for a given file.
     * @param file
     *            the file
     * @throws FileNotFoundException
     *             if file doesn't exist
     */
    public TempInputStream(File file) throws FileNotFoundException {
        fileStream = new FileInputStream(file);
        tempFile = file;
    }

    /**
     * File cleanup.
     */
    private void cleanup() {
        IOUtils.closeQuietly(fileStream);

        try {
            FileUtils.forceDelete(tempFile);
        } catch (FileNotFoundException e) {
            // Ignore not found exceptions
            return;
        } catch (IOException e) {
            log.warn("IOException when deleting TempInputStream file", e);
        }
    }

    /**
     * {@inheritDoc}
     */
    @Override
    public void close() throws IOException {
        try {
            fileStream.close();
        } finally {
            cleanup();
        }
    }

    /**
     * {@inheritDoc}
     */
    @Override
    public int read() throws IOException {
        try {
            int read = fileStream.read();
            if (read == -1) {
                cleanup();
            }
            return read;
        } catch (IOException e) {
            cleanup();
            throw e;
        }
    }

    /**
     * {@inheritDoc}
     */
    @Override
    public int read(byte[] bytes, int off, int len) throws IOException {
        try {
            int read = fileStream.read(bytes, off, len);
            if (read == -1) {
                cleanup();
            }
            return read;
        } catch (IOException e) {
            cleanup();
            throw e;
        }
    }

    /**
     * {@inheritDoc}
     */
    @Override
    public int read(byte[] bytes) throws IOException {
        try {
            int read = fileStream.read(bytes);
            if (read == -1) {
                cleanup();
            }
            return read;
        } catch (IOException e) {
            cleanup();
            throw e;
        }
    }

    /**
     * {@inheritDoc}
     */
    @Override
    public int available() throws IOException {
        return fileStream.available();
    }

    /**
     * {@inheritDoc}
     */
    @Override
    public long skip(long n) throws IOException {
        return fileStream.skip(n);
    }

    /**
     * {@inheritDoc}
     */
    @Override
    protected void finalize() throws IOException {
        close();
    }

}
