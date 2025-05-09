using FeatureMasterX.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
using System.Diagnostics.CodeAnalysis;

namespace FeatureMasterX
{
    /// <summary>
    /// Extensions for FeatureMasterX.
    /// </summary>
    public static class FeatureMasterXExtensions
    {
        #region PUBLIC

        /// <summary>
        /// The name of the feature filter that checks if the user is in a list of allowed users.
        /// </summary>
        public const string ListCheck = "ListCheck";

        /// <summary>
        /// The name of the configuration key that contains the list of allowed users.
        /// </summary>
        public const string AllowedUsers = "AllowedUsers";

        #endregion

        /// <summary>
        /// Adds the FeatureMasterX services to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        [ExcludeFromCodeCoverage]
        public static IServiceCollection AddFeatureMasterX(this IServiceCollection services)
        {
            services.AddSingleton<IFeatureDefinitionProvider, ListCheckFeatureFilterProvider>()
                    .AddFeatureManagement()
                    .AddFeatureFilter<ListCheckFeatureFilter>();

            return services;
        }

        /// <summary>
        /// Adds the Scoped FeatureMasterX services to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        [ExcludeFromCodeCoverage]
        public static IServiceCollection AddScopedFeatureMasterX(this IServiceCollection services)
        {
            services.AddSingleton<IFeatureDefinitionProvider, ListCheckFeatureFilterProvider>()
                    .AddScopedFeatureManagement()
                    .AddFeatureFilter<ListCheckFeatureFilter>();

            return services;
        }
    }
}