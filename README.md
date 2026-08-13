# Autofac.Extensions.Hosting

Autofac extensions that are useful in connecting the `Autofac.Extensions.DependencyInjection` package to .NET generic hosting.

> :warning: **This package is not officially published yet.**
>
> If you're feeling adventurous, you can consume beta packages from the GitHub packages feed. As it is currently very small there is no value in publishing it yet. It may never be published. **YOU HAVE BEEN WARNED.**

[![Build status](https://github.com/autofac/Autofac.Extensions.Hosting/actions/workflows/main.yml/badge.svg)](https://github.com/autofac/Autofac.Extensions.Hosting/actions/workflows/main.yml) [![codecov](https://codecov.io/gh/Autofac/Autofac.Extensions.Hosting/branch/develop/graph/badge.svg)](https://codecov.io/gh/Autofac/Autofac.Extensions.Hosting) [![NuGet](https://img.shields.io/nuget/v/Autofac.Extensions.Hosting.svg)](https://nuget.org/packages/Autofac.Extensions.Hosting)

Please file issues and pull requests for this package in this repository rather than in the Autofac core repo.

- [NuGet](https://www.nuget.org/packages/Autofac.Extensions.Hosting)
- [Contributing](https://autofac.readthedocs.io/en/latest/contributors.html)

## UseAutofac extension

This quick start shows how to use the `IServiceProviderFactory{T}` integration that ASP.NET Core supports to help automatically build the root service provider for you. If you want more manual control, [check out the documentation for examples](https://autofac.readthedocs.io/en/latest/integration/aspnetcore.html).

- Reference the `Autofac.Extensions.DependencyInjection` package from NuGet.
- Reference the `Autofac.Extensions.Hosting` package from NuGet.
- In your `Program.Main` method, where you configure the `HostBuilder`, call `UseAutofac` to hook Autofac into the startup pipeline.
- In the `ConfigureServices` method of your `Startup` class register things into the `IServiceCollection` using extension methods provided by other libraries.
- In the `ConfigureContainer` method of your `Startup` class register things directly into an Autofac `ContainerBuilder`.

The `IServiceProvider` will automatically be created for you, so there's nothing you have to do but *register things*.

```C#
public class Program
{
  public static async Task Main(string[] args)
  {
    // The UseAutofac call here allows for
    // ConfigureContainer to be supported in Startup with
    // a strongly-typed ContainerBuilder.
    var host = Host.CreateDefaultBuilder(args)
      .UseAutofac()
      .ConfigureWebHostDefaults(webHostBuilder => {
        webHostBuilder.UseContentRoot(Directory.GetCurrentDirectory())
        .UseIISIntegration()
        .UseStartup<Startup>()
      })
      .Build();

    await host.RunAsync();
  }
}

public class Startup
{
  public Startup(IWebHostEnvironment env)
  {
    var builder = new ConfigurationBuilder()
        .SetBasePath(env.ContentRootPath)
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
        .AddEnvironmentVariables();
    this.Configuration = builder.Build();
  }

  public IConfiguration Configuration { get; private set; }

  // ConfigureServices is where you register dependencies. This gets
  // called by the runtime before the ConfigureContainer method, below.
  public void ConfigureServices(IServiceCollection services)
  {
    // Add services to the collection. Don't build or return
    // any IServiceProvider or the ConfigureContainer method
    // won't get called.
    services.AddMvc();
  }

  // ConfigureContainer is where you can register things directly
  // with Autofac. This runs after ConfigureServices so the things
  // here will override registrations made in ConfigureServices.
  // Don't build the container; that gets done for you. If you
  // need a reference to the container, you need to use the
  // "Without ConfigureContainer" mechanism shown later.
  public void ConfigureContainer(ContainerBuilder builder)
  {
      builder.RegisterModule(new AutofacModule());
  }

  // Configure is where you add middleware. This is called after
  // ConfigureContainer. You can use IApplicationBuilder.ApplicationServices
  // here if you need to resolve things from the container.
  public void Configure(
    IApplicationBuilder app,
    ILoggerFactory loggerFactory)
  {
      loggerFactory.AddConsole(this.Configuration.GetSection("Logging"));
      loggerFactory.AddDebug();
      app.UseMvc();
  }
}
```

Our [ASP.NET Core](https://autofac.readthedocs.io/en/latest/integration/aspnetcore.html) integration documentation contains more information about using Autofac with ASP.NET Core.

## Get Help

**Need help with Autofac?** We have [a documentation site](https://autofac.readthedocs.io/) as well as [API documentation](https://autofac.org/apidoc/). We're ready to answer your questions on [Stack Overflow](https://stackoverflow.com/questions/tagged/autofac) or check out the [discussion forum](https://groups.google.com/forum/#forum/autofac).
