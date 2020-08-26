// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsAuthorizationSample.FunctionalTesting
{
  using System.Net;
  using System.Net.Http;
  using System.Threading.Tasks;

  using IdentityModel.Client;
  using Microsoft.VisualStudio.TestTools.UnitTesting;
  
  [TestClass]
  public class AuthenticationTest
  {
    private HttpClient _identityClient;
    private HttpClient _apiClient;

    [TestInitialize]
    public void Initialize()
    {
      _identityClient = new HttpClient();
      _apiClient = new HttpClient();
    }

    [TestCleanup]
    public void Cleanup()
    {
      _identityClient?.Dispose();
      _apiClient?.Dispose();
    }

    [TestMethod]
    public async Task GetCurrentUser_Should_Return_Ok_If_Token_Is_Provided()
    {
      var discoveryResponse = await _identityClient.GetDiscoveryDocumentAsync("http://localhost:50110");
      Assert.IsFalse(discoveryResponse.IsError, discoveryResponse.Error);

      var tokenResponse = await _identityClient.RequestClientCredentialsTokenAsync(
        new ClientCredentialsTokenRequest
        {
          Address = discoveryResponse.TokenEndpoint,
          ClientId = "azure-functions-api-client",
          ClientSecret = "azure-functions-api-client-secret",
          Scope = "azure-functions-api",
        });
      Assert.IsFalse(tokenResponse.IsError, tokenResponse.Error);

      _apiClient.SetBearerToken(tokenResponse.AccessToken);
      var userResponse = await _apiClient.GetAsync("user/me");
      Assert.AreEqual(HttpStatusCode.OK, userResponse.StatusCode);
    }

    [TestMethod]
    public async Task GetCurrentUser_Should_Return_Unauthorized_If_Token_Is_Not_Provided()
    {
      var userResponse = await _apiClient.GetAsync("user/me");
      Assert.AreEqual(HttpStatusCode.OK, userResponse.StatusCode);
    }
  }
}
