# CertificateGenerator
Generates Self Signed Certificates

## How to use

Invoke via command line. Args supported.

- 1st arg should be subject & issuer name.
- 2nd arg should amount of years for certificate to be valid.
- 3rd arg should be file path without extension.
- 4th arg should be password for private key.

Certificate (both public .cer and private .pfx keys) will be created at specified path.

Use MS SignTool.exe to sign your application with it.

There is also a test which would generate test certificate at bin path..
