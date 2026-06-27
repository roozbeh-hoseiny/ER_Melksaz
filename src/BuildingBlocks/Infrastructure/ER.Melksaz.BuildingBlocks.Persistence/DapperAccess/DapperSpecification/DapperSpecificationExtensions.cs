namespace ER.Melksaz.BuildingBlocks.Persistence.DapperAccess.DapperSpecification;

public static class DapperSpecificationExtensions
{
    public static IDapperSqlSpecification And(this IDapperSqlSpecification left, IDapperSqlSpecification right)
    {
        return new DapperAndSpecification(left, right);
    }
    public static IDapperSqlSpecification Or(this IDapperSqlSpecification left, IDapperSqlSpecification right)
    {
        return new DapperOrSpecification(left, right);
    }
    public static IDapperSqlSpecification Equals<TValue>(this IDapperSqlSpecification src, string field, TValue value)
    {
        return DapperSpecHelper.Equal(field, value);
    }
}