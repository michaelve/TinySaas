using CodEaisy.TinySaas.Core;
using CodEaisy.TinySaas.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace CodEaisy.TinySaas.Extensions
{
    /// <summary>
    /// Nice method to create the tenant builder
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the services (application specific tenant class)
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddMultiTenancy<TTenant, TTenantStore, TResolutionStrategy>(this IServiceCollection services)
            where TTenant : ITenant
            where TTenantStore : ITenantStore<TTenant>
            where TResolutionStrategy : ITenantResolutionStrategy
        {
            var tenantBuilder = new TenantBuilder<TTenant>(services);
            tenantBuilder.WithStore<TTenantStore>();
            tenantBuilder.WithResolutionStrategy<TResolutionStrategy>();

            return services;
        }

        /// <summary>
        /// Add the services (default tenant class)
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddMultiTenancy<TTenantStore, TResolutionStrategy>(this IServiceCollection services)
            where TTenantStore : ITenantStore<ITenant>
            where TResolutionStrategy : ITenantResolutionStrategy
            => services.AddMultiTenancy<ITenant, TTenantStore, TResolutionStrategy>();
    }
}