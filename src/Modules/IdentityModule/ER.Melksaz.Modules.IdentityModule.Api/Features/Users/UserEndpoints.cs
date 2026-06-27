using ER.Melksaz.BuildingBlocks.Api;

namespace ER.Melksaz.Modules.IdentityModule.Api.Features.Users;

public sealed class UserEndpoints : ApiEndpointBase
{
    public static readonly UserEndpoints Instance = new();

    private const string Tag = "Users";
    protected override ApiEndpointItem? Root => new ApiEndpointItem("users", null);

    public EndpointInfo CreateUserEndpoint { get; }


    public UserEndpoints()
    {
        this.CreateUserEndpoint = this.Create("create", "Create new user", Tag);
    }
}
