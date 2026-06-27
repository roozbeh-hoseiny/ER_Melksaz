using System.Linq.Expressions;

namespace ER.Melksaz.BuildingBlocks.Persistence.EFAccess.EFSpecification.Abstractions;

/// <summary>
/// Specification supporting projection to TResult.
/// </summary>
public interface ISpecification<T, TResult> : IEFSpecification<T>
{
    Expression<Func<T, TResult>> Selector { get; }
}
