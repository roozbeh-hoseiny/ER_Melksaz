using Microsoft.AspNetCore.Routing;

namespace ER.Melksaz.BuildingBlocks.Api;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}
