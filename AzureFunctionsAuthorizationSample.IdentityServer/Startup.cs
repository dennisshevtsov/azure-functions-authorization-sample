// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsAuthorizationSample.IdentityServer
{
  using System.Collections.Generic;
  using System.Security.Claims;

  using IdentityModel;
  using IdentityServer4;
  using IdentityServer4.Models;
  using IdentityServer4.Test;
  using Microsoft.AspNetCore.Builder;
  using Microsoft.Extensions.DependencyInjection;

  public sealed class Startup
  {
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllers();
      services.AddIdentityServer()
              .AddInMemoryIdentityResources(
                new IdentityResource[] {
                  new IdentityResources.OpenId(),
                  new IdentityResources.Profile(),
                })
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
                  new Client
                  {
                    ClientId = "azure-functions-api-client-code",
                    ClientSecrets = { new Secret("azure-functions-api-client-code-secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "https://localhost:44354/signin-oidc", },
                    PostLogoutRedirectUris = { "https://localhost:44354/signout-callback-oidc", },
                    AllowedScopes = new List<string>
                    {
                      IdentityServerConstants.StandardScopes.OpenId,
                      IdentityServerConstants.StandardScopes.Profile,
                      "azure-functions-api",
                    },
                  },
                })
              .AddTestUsers(new List<TestUser>
              {
                new TestUser
                {
                  SubjectId = "12345",
                  Username = "testuser",
                  Password = "testuser",
                  Claims =
                  {
                    new Claim(JwtClaimTypes.Name, "Test User"),
                    new Claim(JwtClaimTypes.Email, "testuser@test.test"),
                  }
                },
              })
              .AddDeveloperSigningCredential();
    }

    public void Configure(IApplicationBuilder app)
    {
      app.UseDeveloperExceptionPage();
      app.UseRouting();
      app.UseIdentityServer();
      app.UseEndpoints(builder => builder.MapDefaultControllerRoute());
    }
  }
}
