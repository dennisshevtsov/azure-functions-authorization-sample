// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsAuthorizationSample.Authorization
{
  using Microsoft.AspNetCore.Http;
  using Microsoft.Azure.WebJobs.Host.Bindings;

  public static class BindingContextExtensions
  {
    public static bool TryGetHttpRequest(
      this BindingContext bindingContext, out HttpRequest httpRequest)
    {
      if (bindingContext != null && bindingContext.BindingData != null)
      {
        foreach (var bindingDataElement in bindingContext.BindingData)
        {
          if (bindingDataElement.Value is HttpRequest httpRequestBindingElementValue)
          {
            httpRequest = httpRequestBindingElementValue;

            return true;
          }
        }
      }

      httpRequest = null;

      return false;
    }
  }
}
