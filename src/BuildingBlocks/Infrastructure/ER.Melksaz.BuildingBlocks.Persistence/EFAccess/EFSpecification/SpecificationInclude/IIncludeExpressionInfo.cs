namespace ER.Melksaz.BuildingBlocks.Persistence.EFAccess.EFSpecification.SpecificationInclude;

public interface IIncludeExpressionInfo<T>
{
    IQueryable<T> Apply(IQueryable<T> query);
}
