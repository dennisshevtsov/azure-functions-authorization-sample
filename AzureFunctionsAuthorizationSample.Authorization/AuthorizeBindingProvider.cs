using Microsoft.Azure.WebJobs.Host.Bindings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctionsAuthorizationSample.Authorization
{
  public sealed class AuthorizeBindingProvider : IBindingProvider
  {
    public Task<IBinding> TryCreateAsync(BindingProviderContext context)
       => Task.FromResult<IBinding>(new AuthorizeBinding());
  }
}
