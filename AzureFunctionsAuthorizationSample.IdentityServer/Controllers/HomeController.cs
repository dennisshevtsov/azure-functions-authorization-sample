

namespace AzureFunctionsAuthorizationSample.IdentityServer.Controllers
{
  using System;
  using System.Threading.Tasks;

  using IdentityServer4.Models;
  using IdentityServer4.Services;
  using Microsoft.AspNetCore.Mvc;

  public sealed class HomeController : Controller
  {
    private readonly IIdentityServerInteractionService _identityServerInteractionService;

    public HomeController(IIdentityServerInteractionService identityServerInteractionService)
    {
      _identityServerInteractionService = identityServerInteractionService
        ?? throw new ArgumentNullException(nameof(identityServerInteractionService));
    }

    [HttpGet]
    public async Task<ErrorMessage> Error(string errorId)
      => await _identityServerInteractionService.GetErrorContextAsync(errorId);
  }
}
