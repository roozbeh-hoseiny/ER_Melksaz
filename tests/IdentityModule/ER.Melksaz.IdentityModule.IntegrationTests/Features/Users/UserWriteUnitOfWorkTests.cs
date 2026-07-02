namespace ER.Melksaz.IdentityModule.IntegrationTests.Features.Users;

[Collection("db")]
public class UserWriteUnitOfWorkTests : IntegrationTestBase
{
    public UserWriteUnitOfWorkTests(SqlServerFixture fixture)
        : base(fixture)
    {
    }
    [Fact]
    public async Task should_save_user_through_repository()
    {
        // arrange
        using var scope = this.Services.CreateScope();

        var uof = scope.ServiceProvider.GetRequiredService<IIdentityUnitOfWork>();
        var result = await PasswordHash.Create("Ehsan@4631@")
            .Map(password => User.Create(
                FirstName.CreateUnsafe("احسان"),
                LastName.CreateUnsafe("شایان"),
                NationalCode.CreateUnsafe("0070561222"),
                Mobile.CreateUnsafe("09123440731"),
                Email.CreateUnsafe("ehsan.shayan@gmail.com"),
                Username.CreateUnsafe("ehsan.shayan"),
                password))
            .Map(newUser => uof.WriteRepo.AddUser(newUser).Map(() => newUser))
            .Map(newUser => uof.SaveChangesWithResultAsync(CancellationToken.None));

        result.IsSuccess.Should().BeTrue();
    }
}