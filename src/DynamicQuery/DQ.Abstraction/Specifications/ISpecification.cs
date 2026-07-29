using System.Linq.Expressions;

namespace DQ.Abstraction.Specifications;

public interface ISpecification<TEntity>
{
    Expression<Func<TEntity, bool>>? Criteria { get; }
}