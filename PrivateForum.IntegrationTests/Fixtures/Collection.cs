using Xunit;

namespace PrivateForum.IntegrationTests.Fixtures
{
    [CollectionDefinition("SystemCollection")]
    public class Collection: ICollectionFixture<TestContext>
    {

    }
}
