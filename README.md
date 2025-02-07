# FeatureMasterX

![FeatureMasterX Logo](assets/image.png)

## Overview

**FeatureMasterX** is a powerful and easy-to-use extension for Microsoft's FeatureManagement library. It simplifies feature toggling by allowing more intuitive configurations for users, accounts, and other contexts. 

### 🔹 Key Features
- Reduces boilerplate code with intuitive APIs.
- Supports user-based and global feature flags.
- Optimized for scalability and flexibility.

## ListCheckFeatureFilter

The first implementation, **ListCheckFeatureFilter**, allows you to group multiple features under `ListCheck`. It evaluates access based on the `EnabledFor` property:
- If `EnabledFor` is set to **ALL**, always return true.
- Otherwise, only the specified can access the feature.

## Configuration Example

```json
{
  "FeatureManagement": {
    "ListCheck": {
      "NewFeature": {
        "EnabledFor": [ "user1@example.com", "admin@example.com" ]
      },
      "AllFeature": {
        "EnabledFor": [ "ALL" ]
      }
    }
  }
}
```

## Installation

```sh
Install-Package FeatureMasterX
```

## Usage

Register in services

```csharp

builder.AddFeatureMasterX();
```

```csharp

var featureManager = services.BuildServiceProvider().GetRequiredService<IFeatureManager>();
if (await featureManager.IsEnabledAsync("NewFeature", "user10@example.com"))
{
    // Feature is enabled for the current user
}
```

## License

This project is licensed under the MIT License.