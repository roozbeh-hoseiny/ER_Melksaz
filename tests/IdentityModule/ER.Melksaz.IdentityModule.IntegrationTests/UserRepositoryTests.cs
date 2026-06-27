using ER.Melksaz.Modules.IdentityModule.Application.Persistence;
using ER.Melksaz.Modules.IdentityModule.Domain.Aggregates.UserAggregate;
using ER.Melksaz.Modules.IdentityModule.Domain.ValueObjects;
using ER.Melksaz.PrimitiveResults;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace ER.Melksaz.IdentityModule.IntegrationTests;

[Collection("db")]
public class UserRepositoryTests : IntegrationTestBase
{
    public UserRepositoryTests(SqlServerFixture fixture)
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
            .Map(newUser => uof.SaveChangesWithResultAsync(CancellationToken.None))
            .ConfigureAwait(false);

        result.IsSuccess.Should().BeTrue();
    }
}
