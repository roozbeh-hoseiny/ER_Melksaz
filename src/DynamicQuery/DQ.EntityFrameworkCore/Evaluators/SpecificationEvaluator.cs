using DQ.Abstraction.Specifications;

namespace DQ.EntityFrameworkCore.Evaluators;

public sealed class SpecificationEvaluator : ISpecificationEvaluator
{
    private readonly IReadOnlyList<ISpecificationPartEvaluator> _evaluators;

    public SpecificationEvaluator(IEnumerable<ISpecificationPartEvaluator> evaluators)
    {
        this._evaluators = evaluators.ToArray();
    }

    public IQueryable<TEntity> Apply<TEntity>(
        IQueryable<TEntity> query,
        ISpecification<TEntity> specification)
        where TEntity : class
    {
        foreach (var evaluator in this._evaluators)
        {
            query =
                evaluator.Apply(
                    query,
                    specification);
        }

        return query;
    }
}