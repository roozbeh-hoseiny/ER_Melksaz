using ER.Melksaz.BuildingBlocks.Persistence.EFAccess.EFSpecification.Core;
using ER.Melksaz.BuildingBlocks.Persistence.EFAccess.EFSpecification.SpecificationInclude;
using ER.Melksaz.BuildingBlocks.Persistence.EFAccess.EFSpecification.SpecificationOrder;
using System.Linq.Expressions;

namespace ER.Melksaz.BuildingBlocks.Persistence.EFAccess.EFSpecification.Abstractions;

/// <summary>
/// Defines query specification for entity queries.
/// </summary>
public interface IEFSpecification<T>
{
    Expression<Func<T, bool>>? Criteria { get; }

    IReadOnlyList<IIncludeExpressionInfo<T>> Includes { get; }

    IReadOnlyList<string> IncludeStrings { get; }

    IReadOnlyList<IOrderExpressionInfo<T>> OrderExpressions { get; }

    IReadOnlyList<SearchExpressionInfo<T>> SearchCriterias { get; }

    int? Skip { get; }

    int? Take { get; }

    bool IsPagingEnabled { get; }

    bool AsNoTracking { get; }

    bool AsSplitQuery { get; }
}
