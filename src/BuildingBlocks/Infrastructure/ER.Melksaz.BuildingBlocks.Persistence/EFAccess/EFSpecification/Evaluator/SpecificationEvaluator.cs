using ER.Melksaz.BuildingBlocks.Persistence.EFAccess.EFSpecification.Abstractions;
using ER.Melksaz.BuildingBlocks.Persistence.EFAccess.EFSpecification.SpecificationInclude;
using ER.Melksaz.BuildingBlocks.Persistence.EFAccess.EFSpecification.SpecificationOrder;
using Microsoft.EntityFrameworkCore;

namespace ER.Melksaz.BuildingBlocks.Persistence.EFAccess.EFSpecification.Evaluator;

public static class SpecificationEvaluator
{
    public static IQueryable<T> GetQuery<T>(
        IQueryable<T> query,
        IEFSpecification<T> spec)
        where T : class
    {
        if (spec.Criteria is not null)
        {
            query = query.Where(spec.Criteria);
        }

        foreach (IIncludeExpressionInfo<T> include in spec.Includes)
        {
            query = include.Apply(query);
        }

        foreach (string include in spec.IncludeStrings)
        {
            query = query.Include(include);
        }

        if (spec.AsNoTracking)
        {
            query = query.AsNoTracking();
        }

        if (spec.AsSplitQuery)
        {
            query = query.AsSplitQuery();
        }

        IOrderedQueryable<T>? orderedQuery = null;

        foreach (IOrderExpressionInfo<T> order in spec.OrderExpressions)
        {
            orderedQuery = order.Apply(query, orderedQuery);

            if (orderedQuery is not null)
            {
                query = orderedQuery;
            }
        }

        if (spec.IsPagingEnabled)
        {
            query = query.Skip(spec.Skip!.Value)
                         .Take(spec.Take!.Value);
        }

        return query;
    }

    public static IQueryable<TResult> GetQuery<T, TResult>(
        IQueryable<T> query,
        ISpecification<T, TResult> spec)
        where T : class
    {
        IQueryable<T> baseQuery = GetQuery(query, (IEFSpecification<T>)spec);

        if (typeof(T).Equals(typeof(TResult)))
        {
            return baseQuery.Cast<TResult>();
        }

        if (spec.Selector is null)
        {
            throw new InvalidOperationException("Projection selector was not provided.");
        }

        return baseQuery.Select(spec.Selector);
    }
}
