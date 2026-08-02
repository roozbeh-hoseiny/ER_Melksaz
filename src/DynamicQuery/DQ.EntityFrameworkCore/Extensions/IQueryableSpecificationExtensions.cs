using DQ.Abstraction.Specifications;
using DQ.EntityFrameworkCore.Evaluators;

namespace DQ.EntityFrameworkCore.Extensions;

/// <summary>
/// Provides extension methods for applying specifications to IQueryable.
/// </summary>
public static class IQueryableSpecificationExtensions
{
    /// <summary>
    /// Applies a specification to an Entity Framework Core query.
    /// </summary>
    /// <typeparam name="TEntity">
    /// The entity type.
    /// </typeparam>
    /// <param name="query">
    /// The source query.
    /// </param>
    /// <param name="specification">
    /// The specification to apply.
    /// </param>
    /// <returns>
    /// A new query with the specification criteria applied.
    /// </returns>
    public static IQueryable<TEntity> ApplySpecification<TEntity>(
        this IQueryable<TEntity> query,
        ISpecification<TEntity>? specification)
        where TEntity : class
    {
        return SpecificationEvaluator.Apply(query, specification);
    }
}