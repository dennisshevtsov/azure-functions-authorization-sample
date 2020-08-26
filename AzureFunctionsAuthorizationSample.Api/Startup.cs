// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

[assembly: Microsoft.Azure.WebJobs.Hosting.WebJobsStartup(typeof(AzureFunctionsAuthorizationSample.Api.Startup))]

namespace AzureFunctionsAuthorizationSample.Api
{
  using Microsoft.Azure.WebJobs;
  using Microsoft.Azure.WebJobs.Hosting;

  using AzureFunctionsAuthorizationSample.Authorization;
  
  public sealed class Startup : IWebJobsStartup
  {
    public void Configure(IWebJobsBuilder builder)
      => builder.AddExtension<AuthorizeExtensionConfigProvider>();
  }
}
