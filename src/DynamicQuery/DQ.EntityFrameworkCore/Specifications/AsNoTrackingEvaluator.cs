using DQ.Abstraction.Specifications;
using Microsoft.EntityFrameworkCore;

namespace DQ.EntityFrameworkCore.Specifications;

public sealed class AsNoTrackingEvaluator : ISpecificationPartEvaluator
{
    public IQueryable<TEntity> Apply<TEntity>(
        IQueryable<TEntity> query,
        ISpecification<TEntity> specification)
        where TEntity : class
    {
        if (specification.AsNoTracking)
        {
            return query.AsNoTracking();
        }

        return query;
    }
}
