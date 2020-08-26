// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsAuthorizationSample.IdentityServer
{
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Hosting;
  using Microsoft.Extensions.Hosting;

  public sealed class Program
  {
    private readonly string[] _args;

    public Program(string[] args) => _args = args;

    public static async Task Main(string[] args) => await new Program(args).RunAsync();

    public Task RunAsync() => Host.CreateDefaultBuilder(_args)
                                  .ConfigureWebHostDefaults(builder => builder.UseStartup<Startup>())
                                  .Build()
                                  .RunAsync();
  }
}
