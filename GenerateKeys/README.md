# GenerateKeys

This app is provided to generate SSL certificates for use with the Example apps
when you use their `_ssl.cfg` configurations.

These Example ssl configurations expect the following files to exist:

* Examples/QuickFixn-TestCA.cer
* Examples/QuickFixn-TestClient.pfx
* Examples/QuickFixn-TestServer.pfx

**This program create these files.**

A full C# app may seem like overkill, but it's the only way we've yet found that
is cross-platform, i.e. works on Windows and Unix.
(Powershell scripts require a module that is Windows-only.)

## Usage Example

```
~/quickfixn/GenerateKeys$ dotnet run ../Examples/
Writing CACertificate: ../Examples/QuickFixn-TestCA.cer
Writing ServerCertificate: ../Examples/QuickFixn-TestServer.pfx
Writing ClientCertificate: ../Examples/QuickFixn-TestClient.pfx
```
