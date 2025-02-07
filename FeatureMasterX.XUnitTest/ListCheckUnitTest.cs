namespace FeatureMasterX.XUnitTest
{
    public class ListCheckUnitTest : BaseUnitTest
    {
        [Fact]
        public async Task Success_Case_NewFeature()
        {
            Assert.True(await _featureManager.IsEnabledAsync("NewFeature", "user1@example.com"));
        }

        [Fact]
        public async Task Failed_Case_NewFeature()
        {
            Assert.False(await _featureManager.IsEnabledAsync("NewFeature", "user10@example.com"));
        }

        [Fact]
        public async Task AllFeature_Test()
        {
            Assert.True(await _featureManager.IsEnabledAsync("AllFeature", "user10@example.com"));
        }
    }
}