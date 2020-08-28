// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsAuthorizationSample.Authorization
{
  public sealed class ExecutingContext : IExecutingContext
  {
    public string Subject { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Role { get; set; }
  }
}
