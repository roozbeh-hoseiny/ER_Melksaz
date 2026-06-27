using System.Linq.Expressions;

namespace ER.Melksaz.BuildingBlocks.Persistence.EFAccess.EFSpecification.Core;

public sealed class SearchExpressionInfo<T>
{
    public Expression<Func<T, string>> Selector { get; }

    public string SearchTerm { get; }

    public SearchExpressionInfo(Expression<Func<T, string>> selector, string term)
    {
        this.Selector = selector;
        this.SearchTerm = term;
    }
}
