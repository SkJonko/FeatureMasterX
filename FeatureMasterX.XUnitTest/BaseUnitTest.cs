using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;

namespace FeatureMasterX.XUnitTest
{
    public abstract class BaseUnitTest : IAsyncLifetime
    {
        #region Internal values

        /// <summary>
        /// The field of the <see cref="Instance"/>
        /// </summary>
        internal IServiceProvider? _instance { get; set; }

        /// <summary>
        /// The field of the <see cref="IFeatureManager"/>
        /// </summary>
        internal IFeatureManager _featureManager { get; set; } = null!;

        #endregion

        public Task DisposeAsync() =>
            Task.CompletedTask;

        public async Task InitializeAsync()
        {
            _instance = CreateServiceCollection();

            _featureManager = _instance.GetRequiredService<IFeatureManager>();
        }

        #region Private Methods

        /// <summary>
        /// Create Service Collection and Add the necessary services in DI.
        /// </summary>
        /// <returns></returns>
        private static IServiceProvider CreateServiceCollection()
        {
            var serviceCollection = new ServiceCollection();

            var configuration = new ConfigurationBuilder()
                                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                .Build();

            serviceCollection.AddSingleton<IConfiguration>(configuration);
            serviceCollection.AddFeatureMasterX();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            return serviceProvider;
        }

        #endregion

    }
}