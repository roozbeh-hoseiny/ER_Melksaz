using DQ.Abstraction.Specifications;

namespace DQ.EntityFrameworkCore.Evaluators;

/// <summary>
/// Applies specifications to Entity Framework Core queries.
/// </summary>
/// <remarks>
/// This evaluator converts a specification into an IQueryable pipeline.
/// The evaluator does not execute the query. Execution remains the
/// responsibility of Entity Framework Core through methods such as
/// ToListAsync, FirstAsync, CountAsync, etc.
/// </remarks>
public static class SpecificationEvaluator
{
    /// <summary>
    /// Applies the specified specification to the query.
    /// </summary>
    /// <typeparam name="TEntity">
    /// The entity type.
    /// </typeparam>
    /// <param name="query">
    /// The source query.
    /// </param>
    /// <param name="specification">
    /// The specification containing filtering criteria.
    /// </param>
    /// <returns>
    /// An IQueryable with the specification applied.
    /// </returns>
    public static IQueryable<TEntity> Apply<TEntity>(
        IQueryable<TEntity> query,
        ISpecification<TEntity>? specification)
        where TEntity : class
    {
        ArgumentNullException.ThrowIfNull(query);

        if (specification?.Criteria is null)
        {
            return query;
        }

        return query.Where(specification.Criteria);
    }
}