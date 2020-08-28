// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsAuthorizationSample.Authorization
{
  using System;
  using System.Linq;
  using System.Net.Mime;
  using System.Security.Claims;
  using System.Text.Json;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Http;
  using Microsoft.Azure.WebJobs.Extensions.Http;
  using Microsoft.Azure.WebJobs.Host.Bindings;

  public sealed class AuthorizeValueProvider : IValueProvider
  {
    private readonly HttpRequest _httpRequest;

    public AuthorizeValueProvider(HttpRequest httpRequest)
    {
      _httpRequest = httpRequest ?? throw new ArgumentNullException(nameof(httpRequest));
    }

    public Type Type => typeof(IExecutingContext);

    public async Task<object> GetValueAsync()
    {
      //_httpRequest.HttpContext.Response.ContentType = MediaTypeNames.Application.Json;
      //_httpRequest.HttpContext.Response.StatusCode = StatusCodes.Status200OK;
      //await _httpRequest.HttpContext.Response.WriteAsync(
      //  JsonSerializer.Serialize(_httpRequest.Headers.Select(x => new { x.Key, x.Value }).ToArray()), _httpRequest.HttpContext.RequestAborted);

      var claimsIdentity = _httpRequest.GetAppServiceIdentity();

      if (claimsIdentity != null)
      {
        var executingContext = new ExecutingContext();

        var subjectClaim = claimsIdentity.Claims.FirstOrDefault(
          claim => claim.Type == "sub");
        if (subjectClaim != null)
        {
          executingContext.Subject = subjectClaim.Value;
        }

        var nameClaim = claimsIdentity.Claims.FirstOrDefault(
          claim => claim.Type == "name");
        if (nameClaim != null)
        {
          executingContext.Name = nameClaim.Value;
        }

        var emailClaim = claimsIdentity.Claims.FirstOrDefault(
          claim => claim.Type == "email");
        if (emailClaim != null)
        {
          executingContext.Email = emailClaim.Value;
        }

        var roleClaim = claimsIdentity.Claims.FirstOrDefault(
          claim => claim.Type == "role");
        if (roleClaim != null)
        {
          executingContext.Role = roleClaim.Value;
        }

        return executingContext;
      }

      _httpRequest.HttpContext.Response.ContentType = MediaTypeNames.Application.Json;
      _httpRequest.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;

      await _httpRequest.HttpContext.Response.WriteAsync(
        $"{{ \"message\": \"unauthorized\" }}", _httpRequest.HttpContext.RequestAborted);

      throw new UnauthorizedException();
    }

    public string ToInvokeString() => "authorize";
  }
}
