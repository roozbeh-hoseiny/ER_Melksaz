namespace ER.Melksaz.IdentityModule.IntegrationTests;

[CollectionDefinition("db")]
public class DatabaseCollection : ICollectionFixture<SqlServerFixture>
{
}