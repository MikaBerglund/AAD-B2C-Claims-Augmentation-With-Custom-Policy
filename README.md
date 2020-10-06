# Azure AD B2C Claims Augmentation With Custom Policy

This repository contains instructions and sample code for creating a custom policy in Azure AD B2C that connects to a REST API to get additional claims to include in issued tokens.

## Placeholders

- `{{yourtenant}}` - The name of the tenant, i.e. `{{yourtenant}}.onmicrosoft.com`.
- `{{IdentityExperienceFrameworkAppId}}` - The application ID (client ID) of your Identity Experience Framework App.
- `{{ProxyIdentityExperienceFrameworkAppId}}` - The application ID (client ID) of your Proxy Identity Experience Framework App.

## Signing and Encryption Keys

The following keys are required for custom policies.

### Signing Key

- **Name**: TokenSigningKeyContainer
- **Type**: RSA
- **Usage**: Signature

### Encryption Key

- **Name**: TokenEncryptionKeyContainer
- **Type**: RSA
- **Usage**: Encryption

## Application Registrations

The following applications are required to work with custom policies. The Identity *Experience Framework App* application is a web API, and the *Proxy Identity Experience Framework App* application is a native app with delegated permissions to *Experience Framework App*.

Details about application registrations can be found [here](https://docs.microsoft.com/en-us/azure/active-directory-b2c/custom-policy-get-started#register-identity-experience-framework-applications).
