// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.


namespace AzureFunctionsAuthorizationSample.Authorization
{
  using System.Threading.Tasks;

  using Microsoft.Azure.WebJobs.Host.Bindings;

  public sealed class AuthorizeBindingProvider : IBindingProvider
  {
    public Task<IBinding> TryCreateAsync(BindingProviderContext context)
       => Task.FromResult<IBinding>(new AuthorizeBinding());
  }
}
