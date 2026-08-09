using DQ.Abstraction.Specifications;
using DQ.EntityFrameworkCore.Specifications;

namespace DQ.EntityFrameworkCore.Queries;

public static class QueryExtensions
{
    public static IQueryable<TEntity> ApplySpecification<TEntity>(
        this IQueryable<TEntity> query,
        ISpecification<TEntity> specification,
        ISpecificationEvaluator evaluator)
        where TEntity : class
    {
        ArgumentNullException.ThrowIfNull(query);
        ArgumentNullException.ThrowIfNull(specification);
        ArgumentNullException.ThrowIfNull(evaluator);

        return evaluator.Apply(
            query,
            specification);
    }

}