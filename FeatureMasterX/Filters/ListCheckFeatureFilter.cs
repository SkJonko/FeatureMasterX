using Microsoft.Extensions.Configuration;
using Microsoft.FeatureManagement;
using System.Text.Json;

namespace FeatureMasterX.Filters
{
    /// <summary>
    /// The User Feature filter.
    /// </summary>
    [FilterAlias(FeatureMasterXExtensions.ListCheck)]
    internal class ListCheckFeatureFilter : IContextualFeatureFilter<string>
    {
        /// <summary>
        /// Evaluate the feature filter
        /// </summary>
        /// <param name="context"></param>
        /// <param name="appsettingsData">The data that stored under the EnabledFor of appsettings.</param>
        /// <returns></returns>
        public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context, string appsettingsData)
        {
            var allowedUsers = JsonSerializer.Deserialize<List<string>>(context?.Parameters?.GetSection(FeatureMasterXExtensions.AllowedUsers)?.Get<string>());

            if (allowedUsers?.Contains("ALL") ?? false)
                return Task.FromResult(true);

            if (string.IsNullOrEmpty(appsettingsData))
                return Task.FromResult(false);

            if (allowedUsers?.Contains(appsettingsData) ?? false)
                return Task.FromResult(true);

            return Task.FromResult(false);
        }
    }
}
