// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Autofac.Extensions.Hosting.Test;

public sealed class HostBuilderExtensionsTests
{
    [Fact]
    public void UseAutofacAutofacServiceProviderResolvable()
    {
        var host = Host.CreateDefaultBuilder(null)
            .UseAutofac()
            .Build();

        Assert.IsAssignableFrom<AutofacServiceProvider>(host.Services);
    }

    [Fact]
    public void UseAutofacChildScopeFactoryWithDelegateAutofacServiceProviderResolvable()
    {
        var host = Host.CreateDefaultBuilder(null)
            .UseAutofacChildLifetimeScopeFactory(GetRootLifetimeScope)
            .Build();

        Assert.IsAssignableFrom<AutofacServiceProvider>(host.Services);
    }

    [Fact]
    public void UseAutofacChildScopeFactoryWithInstanceAutofacServiceProviderResolvable()
    {
        var container = GetRootLifetimeScope();

        var host = Host.CreateDefaultBuilder(null)
            .UseAutofacChildLifetimeScopeFactory(container)
            .Build();

        Assert.IsAssignableFrom<AutofacServiceProvider>(host.Services);
    }

    private static IContainer GetRootLifetimeScope() => new ContainerBuilder().Build();
}
