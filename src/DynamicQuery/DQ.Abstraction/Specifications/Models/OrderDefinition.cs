using System.Linq.Expressions;

namespace DQ.Abstraction.Specifications.Models;

public enum OrderDirection
{
    Ascending = 1,
    Descending = 2
}


public abstract record OrderDefinition<TEntity>
{
    public abstract LambdaExpression Expression { get; }

    public abstract OrderDirection Direction { get; }
}
public sealed record AscendingOrderDefinition<TEntity, TKey>(Expression<Func<TEntity, TKey>> OrderExpression) : OrderDefinition<TEntity>
{
    public override LambdaExpression Expression => this.OrderExpression;
    public override OrderDirection Direction => OrderDirection.Ascending;
}
public sealed record DescendingOrderDefinition<TEntity, TKey>(Expression<Func<TEntity, TKey>> OrderExpression) : OrderDefinition<TEntity>
{
    public override LambdaExpression Expression => this.OrderExpression;
    public override OrderDirection Direction => OrderDirection.Descending;
}