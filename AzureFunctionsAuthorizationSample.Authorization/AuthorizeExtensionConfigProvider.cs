// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsAuthorizationSample.Authorization
{
  using Microsoft.Azure.WebJobs.Host.Config;

  public sealed class AuthorizeExtensionConfigProvider : IExtensionConfigProvider
  {
    public void Initialize(ExtensionConfigContext context)
      => context.AddBindingRule<AuthorizeAttribute>()
                .Bind(new AuthorizeBindingProvider());
  }
}
