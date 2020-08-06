package com.amazon.spapi.sequencing.client.util;

import javax.annotation.Nullable;
import java.io.IOException;
import java.io.OutputStream;

/**
 * {@code OutputStream} wrapper class that forwards data written to this stream to an associated stream and allows
 * switching of the associated stream on the fly. Associated stream can be {@code null} in which case all operations
 * are, essentially, NOOP.
 */
public class DemuxOutputStream extends OutputStream {

    private OutputStream out;

    /**
     * Creates a new {@code DemuxOutputStream} wrapper around given output stream. Accepts {@code null}.
     * @param stream
     *            the output stream (can be {@code null})
     */
    public DemuxOutputStream(@Nullable OutputStream stream) {
        out = stream;
    }

    /**
     * Binds new output stream. Originally bound output stream will be flushed and returned. Accepts {@code null}.
     * @param stream
     *            the new input stream to bind (can be {@code null})
     * @return the previously bound input stream (can be {@code null})
     * @throws IOException
     *             if there was an error flushing the currently bound stream
     */
    @Nullable
    public OutputStream bind(@Nullable OutputStream stream) throws IOException {
        OutputStream tmp = out;
        out = stream;
        if (tmp != null) {
            tmp.flush();
        }
        return tmp;
    }

    /**
     * {@inheritDoc}
     */
    @Override
    public void close() throws IOException {
        if (out != null) {
            out.close();
        }
    }

    /**
     * {@inheritDoc}
     */
    @Override
    public void flush() throws IOException {
        if (out != null) {
            out.flush();
        }
    }

    /**
     * {@inheritDoc}
     */
    @Override
    public void write(byte[] data, int offset, int len) throws IOException {
        if (out != null) {
            out.write(data, offset, len);
        }
    }

    /**
     * {@inheritDoc}
     */
    @Override
    public void write(byte[] data) throws IOException {
        if (out != null) {
            out.write(data);
        }
    }

    /**
     * {@inheritDoc}
     */
    @Override
    public void write(int data) throws IOException {
        if (out != null) {
            out.write(data);
        }
    }

}
