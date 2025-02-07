using FeatureMasterX.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;

namespace FeatureMasterX
{
    public static class FeatureMasterXExtensions
    {
        #region PUBLIC

        public const string ListCheck = "ListCheck";
        public const string AllowedUsers = "AllowedUsers";

        #endregion

        public static IServiceCollection AddFeatureMasterX(this IServiceCollection services)
        {
            services.AddSingleton<IFeatureDefinitionProvider, ListCheckFeatureFilterProvider>()
                    .AddFeatureManagement()
                    .AddFeatureFilter<ListCheckFeatureFilter>();

            return services;
        }
    }
}