# Selling Partner API Documents Helper

This library provides helper classes that you can use to work with encrypted documents. Specifically, they help with:
* Encrypting and uploading a document
* Downloading an encrypted document and reading its decrypted contents

Note: It’s the developer’s responsibility to always maintain encryption at rest. Unencrypted document content should never be stored on disk, even temporarily, because documents can contain sensitive information. 
The helper classes provided in this library are built to assist with this.

## Example usage

### Upload

```java
import java.io.ByteArrayInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.PipedInputStream;
import java.io.PipedOutputStream;
import java.nio.charset.StandardCharsets;
 
import com.amazon.spapi.documents.UploadHelper;
import com.amazon.spapi.documents.UploadSpecification;
import com.amazon.spapi.documents.exception.CryptoException;
import com.amazon.spapi.documents.exception.HttpResponseException;
import com.amazon.spapi.documents.impl.AESCryptoStreamFactory;
 
/* We want to maintain encryption at rest, so do not write unencrypted data to disk.  This is bad:
   InputStream source = new FileInputStream(new File("/path/to/data.txt"));
 
   Instead, if your data can fit in memory, you can create an InputStream from a String (see encryptAndUpload_fromString()).
   Otherwise, you can pipe data into an InputStream using Piped streams (see encryptAndUpload_fromPipedInputStream()).
*/
public class UploadExample {
    private final UploadHelper uploadHelper = new UploadHelper.Builder().build();
 
    public void encryptAndUpload_fromString(String key, String initializationVector, String url) {
        AESCryptoStreamFactory aesCryptoStreamFactory =
                new AESCryptoStreamFactory.Builder(key, initializationVector)
                        .build();
 
        String contentType = String.format("text/plain; charset=%s", StandardCharsets.UTF_8);
 
        // The character set must be the same one that is specified in contentType.
        try (InputStream source = new ByteArrayInputStream("my document contents".getBytes(StandardCharsets.UTF_8))) {
            UploadSpecification uploadSpec =
                    new UploadSpecification.Builder(contentType, aesCryptoStreamFactory, source, url)
                            .build();
 
            uploadHelper.upload(uploadSpec);
        } catch (CryptoException | HttpResponseException | IOException e) {
            // Handle exception.
        }
    }
    public void encryptAndUpload_fromPipedInputStream(String key, String initializationVector, String url) {
        AESCryptoStreamFactory aesCryptoStreamFactory =
                new AESCryptoStreamFactory.Builder(key, initializationVector)
                        .build();
 
        String contentType = String.format("text/plain; charset=%s", StandardCharsets.UTF_8);
 
        try (PipedInputStream source = new PipedInputStream()) {
            new Thread (
                new Runnable() {
                    public void run() {
                        try (PipedOutputStream documentContents = new PipedOutputStream(source)) {
                            // The character set must be the same one that is specified in contentType.
                            documentContents.write("my document contents\n".getBytes(StandardCharsets.UTF_8));
                            documentContents.write("more document contents".getBytes(StandardCharsets.UTF_8));
                        } catch (IOException e) {
                            // Handle exception.
                        }
                    }
                }
            ).start();
 
            UploadSpecification uploadSpec =
                    new UploadSpecification.Builder(contentType, aesCryptoStreamFactory, source, url)
                            .build();
 
            uploadHelper.upload(uploadSpec);
        } catch (CryptoException | HttpResponseException | IOException e) {
            // Handle exception.
        }
    }
}
```

### Download

```java
import java.io.BufferedReader;
import java.io.IOException;
 
import com.amazon.spapi.documents.CompressionAlgorithm;
import com.amazon.spapi.documents.DownloadBundle;
import com.amazon.spapi.documents.DownloadHelper;
import com.amazon.spapi.documents.DownloadSpecification;
import com.amazon.spapi.documents.exception.CryptoException;
import com.amazon.spapi.documents.exception.HttpResponseException;
import com.amazon.spapi.documents.exception.MissingCharsetException;
import com.amazon.spapi.documents.impl.AESCryptoStreamFactory;
 
public class DownloadExample {
    final DownloadHelper downloadHelper = new DownloadHelper.Builder().build();
 
    public void downloadAndDecrypt(String key, String initializationVector, String url, String compressionAlgorithm) {
        AESCryptoStreamFactory aesCryptoStreamFactory =
                new AESCryptoStreamFactory.Builder(key, initializationVector).build();
 
        DownloadSpecification downloadSpec = new DownloadSpecification.Builder(aesCryptoStreamFactory, url)
                .withCompressionAlgorithm(CompressionAlgorithm.fromEquivalent(compressionAlgorithm))
                .build();
 
        try (DownloadBundle downloadBundle = downloadHelper.download(downloadSpec)) {
            // This example assumes that the downloaded document has a charset in the content type, e.g. 
            // text/plain; charset=UTF-8
            try (BufferedReader reader = downloadBundle.newBufferedReader()) {
                String line;
                do {
                    line = reader.readLine();
                    // Process the decrypted line.
                } while (line != null);
            }
        } catch (CryptoException | HttpResponseException | IOException | MissingCharsetException e) {
            // Handle exception here.
        }
    }
}
```

## Requirements

Building the Selling Partner API Documents Helper requires:
1. Java 1.8+
2. Maven/Gradle


## Installation

To install the Selling Partner API Documents Helper to your local Maven repository, simply execute:

```shell
mvn clean install
```

To deploy it to a remote Maven repository instead, configure the settings of the repository and execute:

```shell
mvn clean deploy
```

Refer to the [OSSRH Guide](http://central.sonatype.org/pages/ossrh-guide.html) for more information.

### Maven users

Add this dependency to your project's POM:

```xml
    <dependency>
        <groupId>com.amazon.sellingpartnerapi</groupId>
        <artifactId>sellingpartner-api-documents-helper-java</artifactId>
        <version>1.0.0</version>
    </dependency>
```

### Gradle users

Add this dependency to your project's build file:

```groovy
implementation "com.amazon.sellingpartnerapi:sellingpartner-api-documents-helper-java:1.0.0"
```

### Others

At first generate the JAR by executing:

```shell
mvn clean package
```

Then manually install the following JARs:

* `target/sellingpartner-api-documents-helper-java.jar`
* `target/lib/*.jar`

## License
Swagger Codegen templates are subject to the [Swagger Codegen License](https://github.com/swagger-api/swagger-codegen#license).

All other work licensed as follows:

Copyright Amazon.com Inc. or its affiliates.

All Rights Reserved.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this library except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
