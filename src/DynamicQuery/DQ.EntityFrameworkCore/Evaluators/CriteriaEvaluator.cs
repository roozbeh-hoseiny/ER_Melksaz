using DQ.Abstraction.Specifications;
using DQ.Core.Specifications;

namespace DQ.EntityFrameworkCore.Evaluators;

public sealed class CriteriaEvaluator : ISpecificationPartEvaluator
{
    /// <inheritdoc />
    public IQueryable<TEntity> Apply<TEntity>(
        IQueryable<TEntity> query,
        ISpecification<TEntity> specification)
        where TEntity : class
    {
        ArgumentNullException.ThrowIfNull(query);
        ArgumentNullException.ThrowIfNull(specification);

        var expression = CriteriaExpressionBuilder<TEntity>.Build(specification.Criteria);

        return expression is null
            ? query
            : query.Where(expression);
    }
}