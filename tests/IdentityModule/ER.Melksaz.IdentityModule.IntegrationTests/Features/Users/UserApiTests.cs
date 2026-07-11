using System.Net.Http.Json;

namespace ER.Melksaz.IdentityModule.IntegrationTests.Features.Users;

public class UserApiTests : IntegrationTestBase
{
    public UserApiTests(IntegrationTestFixture fixture) : base(fixture) { }

    [Fact]
    public async Task AddUser_Should_Succeed()
    {
        var response = await this.Client.PostAsJsonAsync("api/v1/users/create",
            new
            {
                firstName = "روزبه",
                lastName = "حسینی",
                nationalCode = "0063811820",
                mobile = "09126431326",
                email = "roozbeh.hoseiny@gmail.com",
                username = "roozbeh",
                password = "Roozbeh@123456"
            });

        response.IsSuccessStatusCode.Should().BeTrue();
    }
}