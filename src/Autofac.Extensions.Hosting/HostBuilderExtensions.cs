// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Autofac.Extensions.Hosting;

/// <summary>
/// Extension methods on <see cref="IHostBuilder"/> to register the <see cref="IServiceProviderFactory{TContainerBuilder}"/>.
/// </summary>
public static class HostBuilderExtensions
{
    /// <summary>
    /// Use the <see cref="AutofacServiceProviderFactory" /> as the factory for creating the service provider.
    /// </summary>
    /// <param name="hostBuilder">The instance of the <see cref="IHostBuilder"/>.</param>
    /// <param name="configurationAction">Action on a <see cref="ContainerBuilder"/> that adds component registrations to the container.</param>
    /// <returns>The same instance of the <see cref="IHostBuilder" /> for chaining.</returns>
    public static IHostBuilder UseAutofac(this IHostBuilder hostBuilder, Action<ContainerBuilder> configurationAction = null)
    {
        if (hostBuilder is null)
        {
            throw new ArgumentNullException(nameof(hostBuilder));
        }

        return hostBuilder.UseServiceProviderFactory(new AutofacServiceProviderFactory(configurationAction));
    }

    /// <summary>
    /// Use the <see cref="AutofacChildLifetimeScopeServiceProviderFactory" /> as the factory for creating the service provider.
    /// </summary>
    /// <param name="hostBuilder">The instance of the <see cref="IHostBuilder"/>.</param>
    /// <param name="containerAccessor">A function to retrieve the <see cref="IContainer"/> instance.</param>
    /// <param name="configurationAction">Action on a <see cref="ContainerBuilder"/> that adds component registrations to the container.</param>
    /// <returns>The same instance of the <see cref="IHostBuilder" /> for chaining.</returns>
    public static IHostBuilder UseAutofacChildLifetimeScopeFactory(this IHostBuilder hostBuilder, Func<IContainer> containerAccessor, Action<ContainerBuilder> configurationAction = null)
    {
        if (hostBuilder is null)
        {
            throw new ArgumentNullException(nameof(hostBuilder));
        }

        return hostBuilder.UseServiceProviderFactory(
            new AutofacChildLifetimeScopeServiceProviderFactory(containerAccessor, configurationAction));
    }

    /// <summary>
    /// Use the <see cref="AutofacChildLifetimeScopeServiceProviderFactory" /> as the factory for creating the service provider.
    /// </summary>
    /// <param name="hostBuilder">The instance of the <see cref="IHostBuilder"/>.</param>
    /// <param name="container">The <see cref="IContainer"/> instance.</param>
    /// <param name="configurationAction">Action on a <see cref="ContainerBuilder"/> that adds component registrations to the container.</param>
    /// <returns>The same instance of the <see cref="IHostBuilder" /> for chaining.</returns>
    public static IHostBuilder UseAutofacChildLifetimeScopeFactory(this IHostBuilder hostBuilder, IContainer container, Action<ContainerBuilder> configurationAction = null)
    {
        if (hostBuilder is null)
        {
            throw new ArgumentNullException(nameof(hostBuilder));
        }

        return hostBuilder.UseServiceProviderFactory(
            new AutofacChildLifetimeScopeServiceProviderFactory(container, configurationAction));
    }
}
