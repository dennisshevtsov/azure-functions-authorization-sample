using System;

namespace AzureFunctionsAuthorizationSample.Authorization
{
  public interface IExecutingContext
  {
    public Guid UserId { get; }

    public string UserName { get; }
  }
}
