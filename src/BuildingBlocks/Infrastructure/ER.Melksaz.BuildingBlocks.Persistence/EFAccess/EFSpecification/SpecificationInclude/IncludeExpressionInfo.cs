using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ER.Melksaz.BuildingBlocks.Persistence.EFAccess.EFSpecification.SpecificationInclude;

public sealed class IncludeExpressionInfo<T, TProperty> : IIncludeExpressionInfo<T>
    where T : class
{
    public Expression<Func<T, TProperty>> Expression { get; }

    public IncludeExpressionInfo(Expression<Func<T, TProperty>> expression)
    {
        this.Expression = expression;
    }

    public IQueryable<T> Apply(IQueryable<T> query)
    {
        return query.Include(this.Expression);
    }
}
