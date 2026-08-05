using DQ.Abstraction.Specifications;

namespace DQ.EntityFrameworkCore.Evaluators;

/// <summary>
/// Represents a single specification evaluator.
/// </summary>
public interface ISpecificationPartEvaluator
{
    /// <summary>
    /// Applies the specification part.
    /// </summary>
    IQueryable<TEntity> Apply<TEntity>(
        IQueryable<TEntity> query,
        ISpecification<TEntity> specification)
        where TEntity : class;
}
