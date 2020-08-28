// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsAuthorizationSample.Authorization
{
  public interface IExecutingContext
  {
    public string Subject { get; }

    public string Name { get; }

    public string Email { get; }

    public string Role { get; }
  }
}
