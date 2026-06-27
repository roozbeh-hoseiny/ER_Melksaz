namespace ER.Melksaz.BuildingBlocks.Api;

public readonly record struct EndpointInfo(
    string Url,
    string Name,
    string Description,
    string Tag);
