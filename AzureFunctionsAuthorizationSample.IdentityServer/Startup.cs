// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.


namespace AzureFunctionsAuthorizationSample.IdentityServer
{
  using IdentityServer4.Models;
  using Microsoft.AspNetCore.Builder;
  using Microsoft.AspNetCore.Http;
  using Microsoft.Extensions.DependencyInjection;

  public sealed class Startup
  {
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddIdentityServer()
              //.AddInMemoryIdentityResources(
              //  new IdentityResource[] {
              //    new IdentityResources.OpenId(),
              //    new IdentityResources.Profile(),
              //  })
              .AddInMemoryApiScopes(
                new[] {
                  new ApiScope("azure-functions-api", "Azure Functions API"),
                })
              .AddInMemoryClients(
                new[] {
                  new Client
                  {
                    ClientId = "azure-functions-api-client",
                    ClientSecrets = { new Secret("azure-functions-api-client-secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "azure-functions-api", },
                  },
                })
              .AddDeveloperSigningCredential();
    }

    public void Configure(IApplicationBuilder app)
    {
      app.UseDeveloperExceptionPage();

      //app.UseRouting();
      app.UseIdentityServer();
    }
  }
}
