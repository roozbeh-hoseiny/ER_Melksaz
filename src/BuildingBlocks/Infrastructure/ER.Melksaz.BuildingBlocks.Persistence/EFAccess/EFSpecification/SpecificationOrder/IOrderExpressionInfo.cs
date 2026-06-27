namespace ER.Melksaz.BuildingBlocks.Persistence.EFAccess.EFSpecification.SpecificationOrder;

public interface IOrderExpressionInfo<T>
{
    IOrderedQueryable<T>? Apply(IQueryable<T> query, IOrderedQueryable<T>? orderedQuery);
}
