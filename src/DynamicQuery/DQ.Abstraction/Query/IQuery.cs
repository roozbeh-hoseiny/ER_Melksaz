using DQ.Abstraction.Ordering;
using DQ.Abstraction.Paging;
using DQ.Abstraction.Projections;
using DQ.Abstraction.Specifications;

namespace DQ.Abstraction.Query;

public interface IQuery<TEntity, TResult>
{
    ISpecification<TEntity>? Specification { get; }
    IProjection<TEntity, TResult>? Projection { get; }
    IReadOnlyList<IOrderSpecification<TEntity>> Orderings { get; }
    IPagingSpecification? Paging { get; }
}