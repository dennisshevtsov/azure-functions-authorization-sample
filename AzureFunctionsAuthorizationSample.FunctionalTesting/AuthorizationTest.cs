// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsAuthorizationSample.FunctionalTesting
{
  using System;
  using System.Net;
  using System.Net.Http;
  using System.Text;
  using System.Text.Json;
  using System.Threading.Tasks;

  using Microsoft.VisualStudio.TestTools.UnitTesting;

  [TestClass]
  public sealed class AuthorizationTest
  {
    private HttpClient _apiClient;

    [TestInitialize]
    public void Initialize()
    {
      _apiClient = new HttpClient();
      _apiClient.BaseAddress = new Uri("http://localhost:7071/api/");
    }

    [TestMethod]
    public async Task GetCurrentUser_Should_Return_Ok_X_Ms_Client_Principal_Is_Provided()
    {
      var principal = new
      {
        auth_typ = "Bearer",
        name_typ = "name",
        role_typ = "role",
        claims = new[]
        {
          new
          {
            typ = "sub",
            val = "123",
          },
          new
          {
            typ = "name",
            val = "John Smith",
          },
          new
          {
            typ = "role",
            val = "admin",
          }
        },
      };
      var json = JsonSerializer.Serialize(principal);
      var encoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));

      _apiClient.DefaultRequestHeaders.Add(
        "x-ms-client-principal", "eyJhdXRoX3R5cCI6IkJlYXJlciIsIm5hbWVfdHlwIjoibmFtZSIsInJvbGVfdHlwIjoicm9sZSIsImNsYWltcyI6W3sidHlwIjoibmJmIiwidmFsIjpbIjE1OTg2NDgxNTciXX0seyJ0eXAiOiJleHAiLCJ2YWwiOlsiMTU5ODY1MTc1NyJdfSx7InR5cCI6ImlzcyIsInZhbCI6WyJodHRwczovL2V4LXJlcS1kZXYtaWRlbnRpdHkuYXp1cmV3ZWJzaXRlcy5uZXQiXX0seyJ0eXAiOiJjbGllbnRfaWQiLCJ2YWwiOlsibXZjIl19LHsidHlwIjoic3ViIiwidmFsIjpbIjgxODcyNyJdfSx7InR5cCI6ImF1dGhfdGltZSIsInZhbCI6WyIxNTk4NjQ4MTU3Il19LHsidHlwIjoiaWRwIiwidmFsIjpbImxvY2FsIl19LHsidHlwIjoianRpIiwidmFsIjpbIkQzQjkyRjlBRDc1NkJFMUIzQzFDRTAwNTY5REEyMDE5Il19LHsidHlwIjoic2lkIiwidmFsIjpbIjVEMjg2RjZCMkE2QjI1MzRBNzM4MkU0MzQwQzhBOTNBIl19LHsidHlwIjoiaWF0IiwidmFsIjpbIjE1OTg2NDgxNTciXX0seyJ0eXAiOiJzY29wZSIsInZhbCI6WyJvcGVuaWQiLCJwcm9maWxlIiwiYXBpMSJdfSx7InR5cCI6ImFtciIsInZhbCI6WyJwd2QiXX1dfQ==");

      var userResponse = await _apiClient.GetAsync("user/me");
      Assert.AreEqual(HttpStatusCode.OK, userResponse.StatusCode);
    }

    [TestMethod]
    public async Task DecodeToken()
    {
      var token = "eyJhdXRoX3R5cCI6IkJlYXJlciIsIm5hbWVfdHlwIjoibmFtZSIsInJvbGVfdHlwIjoicm9sZSIsImNsYWltcyI6W3sidHlwIjoibmJmIiwidmFsIjoiMTU5ODY0ODE1NyJ9LHsidHlwIjoiZXhwIiwidmFsIjoiMTU5ODY1MTc1NyJ9LHsidHlwIjoiaXNzIiwidmFsIjoiaHR0cHM6Ly9leC1yZXEtZGV2LWlkZW50aXR5LmF6dXJld2Vic2l0ZXMubmV0In0seyJ0eXAiOiJjbGllbnRfaWQiLCJ2YWwiOiJtdmMifSx7InR5cCI6InN1YiIsInZhbCI6IjgxODcyNyJ9LHsidHlwIjoiYXV0aF90aW1lIiwidmFsIjoiMTU5ODY0ODE1NyJ9LHsidHlwIjoiaWRwIiwidmFsIjoibG9jYWwifSx7InR5cCI6Imp0aSIsInZhbCI6IkQzQjkyRjlBRDc1NkJFMUIzQzFDRTAwNTY5REEyMDE5In0seyJ0eXAiOiJzaWQiLCJ2YWwiOiI1RDI4NkY2QjJBNkIyNTM0QTczODJFNDM0MEM4QTkzQSJ9LHsidHlwIjoiaWF0IiwidmFsIjoiMTU5ODY0ODE1NyJ9LHsidHlwIjoic2NvcGUiLCJ2YWwiOiJvcGVuaWQifSx7InR5cCI6InNjb3BlIiwidmFsIjoicHJvZmlsZSJ9LHsidHlwIjoic2NvcGUiLCJ2YWwiOiJhcGkxIn0seyJ0eXAiOiJhbXIiLCJ2YWwiOiJwd2QifV19";
      var decoded = Encoding.UTF8.GetString(Convert.FromBase64String(token));
    }
  }
}
