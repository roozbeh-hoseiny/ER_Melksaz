using ER.Melksaz.IdentityModule.IntegrationTests.Infrastructure.Fixtures;

namespace ER.Melksaz.IdentityModule.IntegrationTests.Infrastructure.Collections;

[CollectionDefinition("db")]
public sealed class DatabaseCollection : ICollectionFixture<IntegrationTestFixture>
{
}