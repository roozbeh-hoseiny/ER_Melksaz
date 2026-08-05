using System.Linq.Expressions;

namespace DQ.Abstraction.Specifications.Models;

public abstract record CriteriaNode<TEntity>;
public sealed record CriteriaExpressionNode<TEntity>(Expression<Func<TEntity, bool>> Expression) : CriteriaNode<TEntity>;
public sealed record CriteriaGroupNode<TEntity>(CriteriaGroupOperator Operator, IReadOnlyList<CriteriaNode<TEntity>> Children) : CriteriaNode<TEntity>;

//public sealed record CriteriaDefinition<TEntity>
//{
//    public Expression<Func<TEntity, bool>> Expression
//    {
//        get;
//    }

//    public CriteriaGroupOperator Operator
//    {
//        get;
//    }

//    public CriteriaDefinition(Expression<Func<TEntity, bool>> expression, CriteriaGroupOperator @operator)
//    {
//        this.Expression = expression;
//        this.Operator = @operator;
//    }
//}
public enum CriteriaGroupOperator
{
    And = 1,
    Or = 2
}
