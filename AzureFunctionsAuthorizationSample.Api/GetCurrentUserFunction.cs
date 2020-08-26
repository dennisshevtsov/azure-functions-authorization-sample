// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsAuthorizationSample.Api
{
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Azure.WebJobs;
  using Microsoft.Azure.WebJobs.Extensions.Http;
  using Microsoft.AspNetCore.Http;

  using AzureFunctionsAuthorizationSample.Authorization;

  public sealed class GetCurrentUserFunction
  {
    [FunctionName(nameof(GetCurrentUserFunction))]
    public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Function,
                     "get",
                     Route = "user/me")] HttpRequest request,
        [Authorize(Permissions = new[] { "read-profile", })] IExecutingContext executingContext)
        => new OkObjectResult(executingContext);
  }
}
