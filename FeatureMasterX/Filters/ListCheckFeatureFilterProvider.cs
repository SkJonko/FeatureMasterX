﻿using Microsoft.Extensions.Configuration;
using Microsoft.FeatureManagement;
using System.Text.Json;

namespace FeatureMasterX.Filters
{
    /// <summary>
    /// Provides a feature filter that checks if the user is in a list of allowed users.
    /// </summary>
    public class ListCheckFeatureFilterProvider : IFeatureDefinitionProvider
    {
        #region Private Values

        private readonly IConfiguration _configuration;

        #endregion

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="configuration"></param>
        public ListCheckFeatureFilterProvider(IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));

            _configuration = configuration;
        }

        /// <inheritdoc/>
        public Task<FeatureDefinition> GetFeatureDefinitionAsync(string featureName)
        {
            var section = _configuration.GetSection($"FeatureManagement:{FeatureMasterXExtensions.ListCheck}:{featureName}:EnabledFor");

            var allowedUsers = section.Get<List<string>>() ?? [];

            return Task.FromResult(new FeatureDefinition
            {
                Name = featureName,
                EnabledFor = [.. allowedUsers.Select(user => new FeatureFilterConfiguration
                {
                    Name = FeatureMasterXExtensions.ListCheck,
                    Parameters = new ConfigurationBuilder()
                        .AddInMemoryCollection(new Dictionary<string, string>
                        {
                            { FeatureMasterXExtensions.AllowedUsers, JsonSerializer.Serialize(allowedUsers) }
                        })
                        .Build()
                })]
            });
        }

        /// <inheritdoc/>
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async IAsyncEnumerable<FeatureDefinition> GetAllFeatureDefinitionsAsync()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            var featureManagementSection = _configuration.GetSection("FeatureManagement");

            var features = featureManagementSection.GetChildren();

            foreach (var feature in features)
            {
                var allowedUsers = feature.GetSection("EnabledFor").Get<List<string>>() ?? [];

                yield return new FeatureDefinition
                {
                    Name = feature.Key,
                    EnabledFor = [.. allowedUsers.Select(user => new FeatureFilterConfiguration
                    {
                        Name = FeatureMasterXExtensions.ListCheck,
                        Parameters = new ConfigurationBuilder()
                            .AddInMemoryCollection(new Dictionary<string, string>
                            {
                                { FeatureMasterXExtensions.AllowedUsers, JsonSerializer.Serialize(allowedUsers) }
                            })
                        .Build()
                    })]
                };
            }
        }
    }
}