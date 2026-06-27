namespace ER.Melksaz.BuildingBlocks.Api;

public sealed record ApiEndpointItem(string Segment, ApiEndpointItem? Parent = null);
