using System.Linq.Expressions;

namespace DQ.Abstraction.Ordering;

public interface IOrderSpecification<TEntity>
{
    LambdaExpression KeySelector { get; }

    bool Descending { get; }
}