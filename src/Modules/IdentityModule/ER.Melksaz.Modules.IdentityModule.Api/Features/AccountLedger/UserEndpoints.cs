using ER.Melksaz.BuildingBlocks.Api;

namespace ER.Melksaz.Modules.IdentityModule.Api.Features.AccountLedger;

public sealed class AccountEndpoints : ApiEndpointBase
{
    public static readonly AccountEndpoints Instance = new();

    private const string Tag = "Accounts";
    protected override ApiEndpointItem? Root => new ApiEndpointItem("accounts", null);

    public EndpointInfo CreateGroupEndpoint { get; }
    public EndpointInfo CreateGeneralEndpoint { get; }
    public EndpointInfo CreateSubsidaryEndpoint { get; }


    public AccountEndpoints()
    {
        this.CreateGroupEndpoint = this.Create("group", "Create new group", Tag);
        this.CreateGeneralEndpoint = this.Create("general", "Create new general", Tag);
        this.CreateSubsidaryEndpoint = this.Create("subsidary", "Create new subsidary", Tag);
    }
}
