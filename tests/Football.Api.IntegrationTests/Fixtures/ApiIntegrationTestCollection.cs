namespace Football.Api.IntegrationTests.Fixtures;

[CollectionDefinition(ApiIntegrationTestCollection.Name)]
public class ApiIntegrationTestCollection : ICollectionFixture<ApiIntegrationTestFixture>
{
    public const string Name = "ApiIntegrationTests";
}
