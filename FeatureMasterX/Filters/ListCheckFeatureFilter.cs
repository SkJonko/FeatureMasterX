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
        /// <param name="userEmail"></param>
        /// <returns></returns>
        public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context, string userEmail)
        {
            var allowedUsers = JsonSerializer.Deserialize<List<string>>(context?.Parameters?.GetSection(FeatureMasterXExtensions.AllowedUsers)?.Get<string>());

            if (allowedUsers?.Contains("ALL") ?? false)
                return Task.FromResult(true);

            if (string.IsNullOrEmpty(userEmail))
                return Task.FromResult(false);

            if (allowedUsers?.Contains(userEmail) ?? false)
                return Task.FromResult(true);

            return Task.FromResult(false);
        }
    }
}
