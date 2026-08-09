using DQ.Abstraction.Specifications;
using Microsoft.EntityFrameworkCore;

namespace DQ.EntityFrameworkCore.Specifications;

public sealed class SplitQueryEvaluator : ISpecificationPartEvaluator
{
    public IQueryable<TEntity> Apply<TEntity>(
        IQueryable<TEntity> query,
        ISpecification<TEntity> specification)
        where TEntity : class
    {
        return specification.AsSplitQuery
            ? query.AsSplitQuery()
            : query;
    }
}