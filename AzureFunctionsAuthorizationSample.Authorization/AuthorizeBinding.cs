using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Protocols;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctionsAuthorizationSample.Authorization
{
  public sealed class AuthorizeBinding : IBinding
  {
    public bool FromAttribute => true;

    public Task<IValueProvider> BindAsync(object value, ValueBindingContext context)
      => throw new InvalidOperationException();

    public Task<IValueProvider> BindAsync(BindingContext context)
    {
      if (context.TryGetHttpRequest(out var httpRequest))
      {
        return Task.FromResult<IValueProvider>(new AuthorizeValueProvider(httpRequest));
      }

      throw new InvalidOperationException();
    }

    public ParameterDescriptor ToParameterDescriptor() => new ParameterDescriptor { Name = "authorize", };
  }
}
