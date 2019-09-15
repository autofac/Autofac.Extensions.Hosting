using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace Autofac.Extensions.Hosting.Test
{
    public sealed class HostBuilderExtensionsTests
    {
        [Fact]
        public void UseAutofacAutofacServiceProviderResolveable()
        {
            var host = Host.CreateDefaultBuilder(null)
                .UseAutofac()
                .Build();

            Assert.IsAssignableFrom<AutofacServiceProvider>(host.Services);
        }

        [Fact]
        public void UseAutofacChildScopeFactoryWithDelegateAutofacServiceProviderResolveable()
        {
            var host = Host.CreateDefaultBuilder(null)
                .UseAutofacChildLifetimeScopeFactory(GetRootLifetimeScope)
                .Build();

            Assert.IsAssignableFrom<AutofacServiceProvider>(host.Services);
        }

        [Fact]
        public void UseAutofacChildScopeFactoryWithInstanceAutofacServiceProviderResolveable()
        {
            var container = GetRootLifetimeScope();

            var host = Host.CreateDefaultBuilder(null)
                .UseAutofacChildLifetimeScopeFactory(container)
                .Build();

            Assert.IsAssignableFrom<AutofacServiceProvider>(host.Services);
        }

        private static IContainer GetRootLifetimeScope() => new ContainerBuilder().Build();
    }
}
