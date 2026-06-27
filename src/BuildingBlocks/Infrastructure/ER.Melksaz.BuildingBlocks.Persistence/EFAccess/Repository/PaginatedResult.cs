namespace ER.Melksaz.BuildingBlocks.Persistence.EFAccess.Repository;

public sealed record PaginatedResult<TResult, TKey>(long TotalCount, TKey LastSeen, TResult[] Data);