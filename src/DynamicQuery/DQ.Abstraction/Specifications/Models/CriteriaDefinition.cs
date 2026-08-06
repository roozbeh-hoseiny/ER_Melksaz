using System.Linq.Expressions;

namespace DQ.Abstraction.Specifications.Models;

public abstract record CriteriaNode<TEntity>;
public sealed record CriteriaExpressionNode<TEntity>(Expression<Func<TEntity, bool>> Expression) : CriteriaNode<TEntity>;
public sealed record CriteriaGroupNode<TEntity>(CriteriaGroupOperator Operator, IReadOnlyList<CriteriaNode<TEntity>> Children) : CriteriaNode<TEntity>;


public enum CriteriaGroupOperator
{
    And = 1,
    Or = 2
}



