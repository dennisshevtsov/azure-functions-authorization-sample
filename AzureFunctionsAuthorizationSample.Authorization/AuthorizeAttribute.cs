// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsAuthorizationSample.Authorization
{
  using System;

  using Microsoft.Azure.WebJobs.Description;
  
  [Binding]
  [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
  public sealed class AuthorizeAttribute : Attribute
  {
    public string[] Permissions { get; set; } 
  }
}
