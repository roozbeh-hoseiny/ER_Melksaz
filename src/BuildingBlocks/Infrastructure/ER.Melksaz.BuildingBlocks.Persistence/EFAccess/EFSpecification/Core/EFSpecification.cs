using ER.Melksaz.BuildingBlocks.Persistence.Abstractions;
using ER.Melksaz.BuildingBlocks.Persistence.EFAccess.EFSpecification.Abstractions;
using ER.Melksaz.BuildingBlocks.Persistence.EFAccess.EFSpecification.SpecificationInclude;
using ER.Melksaz.BuildingBlocks.Persistence.EFAccess.EFSpecification.SpecificationOrder;
using System.Linq.Expressions;

namespace ER.Melksaz.BuildingBlocks.Persistence.EFAccess.EFSpecification.Core;

/// <summary>
/// Base class for creating specifications.
/// </summary>
public abstract class EFSpecification<T> : IEFSpecification<T>, IBaseSpecification<T>
    where T : class
{
    #region " Fields "
    private readonly List<IIncludeExpressionInfo<T>> _includes = [];
    private readonly List<string> _includeStrings = [];
    private readonly List<IOrderExpressionInfo<T>> _orderExpressions = [];
    private readonly List<SearchExpressionInfo<T>> _searchExpressions = [];
    #endregion

    #region " Properties "
    public Expression<Func<T, bool>>? Criteria { get; private set; }

    public IReadOnlyList<IIncludeExpressionInfo<T>> Includes => this._includes;

    public IReadOnlyList<string> IncludeStrings => this._includeStrings;

    public IReadOnlyList<IOrderExpressionInfo<T>> OrderExpressions => this._orderExpressions;

    public IReadOnlyList<SearchExpressionInfo<T>> SearchCriterias => this._searchExpressions;

    public int? Skip { get; private set; }

    public int? Take { get; private set; }

    public bool IsPagingEnabled { get; private set; }

    public bool AsNoTracking { get; private set; }

    public bool AsSplitQuery { get; private set; }
    #endregion

    #region " Methods "
    protected IEFSpecification<T> Where(Expression<Func<T, bool>> criteria)
    {
        this.Criteria = criteria;
        return this;
    }

    protected IEFSpecification<T> AddInclude<TProperty>(Expression<Func<T, TProperty>> includeExpression)
    {
        this._includes.Add(new IncludeExpressionInfo<T, TProperty>(includeExpression));
        return this;
    }

    protected IEFSpecification<T> AddInclude(string include)
    {
        this._includeStrings.Add(include);
        return this;
    }

    protected IEFSpecification<T> ApplyPaging(int skip, int take)
    {
        this.Skip = skip;
        this.Take = take;
        this.IsPagingEnabled = true;

        return this;
    }

    protected IEFSpecification<T> ApplyOrderBy<TKey>(Expression<Func<T, TKey>> expression)
    {
        this._orderExpressions.Add(new OrderExpressionInfo<T, TKey>(expression, OrderType.OrderBy));

        return this;
    }

    protected IEFSpecification<T> ApplyOrderByDescending<TKey>(Expression<Func<T, TKey>> expression)
    {
        this._orderExpressions.Add(new OrderExpressionInfo<T, TKey>(expression, OrderType.OrderByDescending));
        return this;
    }

    protected IEFSpecification<T> ApplyThenBy<TKey>(Expression<Func<T, TKey>> expression)
    {
        this._orderExpressions.Add(new OrderExpressionInfo<T, TKey>(expression, OrderType.ThenBy));
        return this;
    }

    protected IEFSpecification<T> ApplySearch(Expression<Func<T, string>> selector, string term)
    {
        this._searchExpressions.Add(new SearchExpressionInfo<T>(selector, term));
        return this;
    }

    protected IEFSpecification<T> WithNoTracking()
    {
        this.AsNoTracking = true;
        return this;
    }

    protected IEFSpecification<T> WithSplitQuery()
    {
        this.AsSplitQuery = true;
        return this;
    }
    #endregion
}
