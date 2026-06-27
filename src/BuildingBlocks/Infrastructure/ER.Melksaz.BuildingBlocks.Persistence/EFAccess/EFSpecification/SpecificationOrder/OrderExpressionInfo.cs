using System.Linq.Expressions;

namespace ER.Melksaz.BuildingBlocks.Persistence.EFAccess.EFSpecification.SpecificationOrder;

public sealed class OrderExpressionInfo<T, TKey> : IOrderExpressionInfo<T>
{
    public Expression<Func<T, TKey>> Expression { get; }

    public OrderType OrderType { get; }

    public OrderExpressionInfo(Expression<Func<T, TKey>> expression, OrderType orderType)
    {
        this.Expression = expression;
        this.OrderType = orderType;
    }

    public IOrderedQueryable<T>? Apply(IQueryable<T> query, IOrderedQueryable<T>? orderedQuery)
    {
        return this.OrderType switch
        {
            OrderType.OrderBy => query.OrderBy(this.Expression),

            OrderType.OrderByDescending => query.OrderByDescending(this.Expression),

            OrderType.ThenBy when orderedQuery != null =>
                orderedQuery.ThenBy(this.Expression),

            OrderType.ThenByDescending when orderedQuery != null =>
                orderedQuery.ThenByDescending(this.Expression),

            _ => orderedQuery
        };
    }
}
