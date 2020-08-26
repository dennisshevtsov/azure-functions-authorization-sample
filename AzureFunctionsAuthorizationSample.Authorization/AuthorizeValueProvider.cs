using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host.Bindings;
using System;
using System.Linq;
using System.Net.Mime;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AzureFunctionsAuthorizationSample.Authorization
{
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
      var claimsIdentity = _httpRequest.GetAppServiceIdentity();

      if (claimsIdentity != null)
      {
        var subjectClaim = claimsIdentity.Claims.FirstOrDefault(
          claim => claim.Type == ClaimTypes.NameIdentifier);

        if (subjectClaim != null)
        {
          return new ExecutingContext
          {
            UserId = Guid.NewGuid(),
            UserName = "Test User",
          };
        }
      }

      _httpRequest.HttpContext.Response.ContentType = MediaTypeNames.Application.Json;
      _httpRequest.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;

      await _httpRequest.HttpContext.Response.WriteAsync($"{{ \"message\": \"unauthorized\" }}");
      throw new UnauthorizedException();
    }

    public string ToInvokeString() => "authorize";
  }
}
