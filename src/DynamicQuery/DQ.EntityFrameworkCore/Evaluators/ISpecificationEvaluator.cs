using DQ.Abstraction.Specifications;

namespace DQ.EntityFrameworkCore.Evaluators;

public interface ISpecificationEvaluator
{
    IQueryable<TEntity> Apply<TEntity>(
        IQueryable<TEntity> query,
        ISpecification<TEntity> specification)
        where TEntity : class;
}