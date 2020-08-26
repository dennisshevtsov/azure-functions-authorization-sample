using System;
using System.Collections.Generic;
using System.Text;

namespace AzureFunctionsAuthorizationSample.Authorization
{
  public sealed class ExecutingContext : IExecutingContext
  {
    public Guid UserId { get; set; }

    public string UserName { get; set; }
  }
}
