using Microsoft.Azure.WebJobs.Host.Config;

namespace AzureFunctionsAuthorizationSample.Authorization
{
  public sealed class AuthorizeExtensionConfigProvider : IExtensionConfigProvider
  {
    public void Initialize(ExtensionConfigContext context)
      => context.AddBindingRule<AuthorizeAttribute>()
                .Bind(new AuthorizeBindingProvider());
  }
}
