// <copyright file="HostBuilderExtensions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Autofac.Extensions.Hosting
{
    using System;
    using Autofac.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

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
            => hostBuilder.UseServiceProviderFactory(new AutofacServiceProviderFactory(configurationAction));

        /// <summary>
        /// Use the <see cref="AutofacChildLifetimeScopeServiceProviderFactory" /> as the factory for creating the service provider.
        /// </summary>
        /// <param name="hostBuilder">The instance of the <see cref="IHostBuilder"/>.</param>
        /// <param name="containerAccessor">A function to retrieve the <see cref="IContainer"/> instance.</param>
        /// <param name="configurationAction">Action on a <see cref="ContainerBuilder"/> that adds component registrations to the container.</param>
        /// <returns>The same instance of the <see cref="IHostBuilder" /> for chaining.</returns>
        public static IHostBuilder UseAutofacChildLifetimeScopeFactory(this IHostBuilder hostBuilder, Func<IContainer> containerAccessor, Action<ContainerBuilder> configurationAction = null)
            => hostBuilder.UseServiceProviderFactory(
                new AutofacChildLifetimeScopeServiceProviderFactory(containerAccessor, configurationAction));

        /// <summary>
        /// Use the <see cref="AutofacChildLifetimeScopeServiceProviderFactory" /> as the factory for creating the service provider.
        /// </summary>
        /// <param name="hostBuilder">The instance of the <see cref="IHostBuilder"/>.</param>
        /// <param name="container">The <see cref="IContainer"/> instance.</param>
        /// <param name="configurationAction">Action on a <see cref="ContainerBuilder"/> that adds component registrations to the container.</param>
        /// <returns>The same instance of the <see cref="IHostBuilder" /> for chaining.</returns>
        public static IHostBuilder UseAutofacChildLifetimeScopeFactory(this IHostBuilder hostBuilder, IContainer container, Action<ContainerBuilder> configurationAction = null)
            => hostBuilder.UseServiceProviderFactory(
                new AutofacChildLifetimeScopeServiceProviderFactory(container, configurationAction));
    }
}
