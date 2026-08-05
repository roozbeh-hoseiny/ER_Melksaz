using DQ.Abstraction.Specifications;
using DQ.Abstraction.Specifications.Models;

namespace DQ.Core.Specifications;

public abstract class Specification<TEntity> : ISpecification<TEntity>
{
    private readonly SpecificationState<TEntity> _state;

    public CriteriaNode<TEntity>? Criteria => this._state.Criteria;
    public IReadOnlyList<IncludeDefinition<TEntity>> Includes => this._state.Includes;
    public IReadOnlyList<OrderDefinition<TEntity>> Orders => this._state.Orders;
    public bool AsNoTracking => this._state.AsNoTracking;
    public bool AsNoTrackingWithIdentityResolution => this._state.AsNoTrackingWithIdentityResolution;
    public bool AsSplitQuery => this._state.AsSplitQuery;
    public int? Skip => this._state.Skip;
    public int? Take => this._state.Take;

    public object? Projection => this._state.Projection;

    protected Specification(SpecificationState<TEntity> state)
    {
        ArgumentNullException.ThrowIfNull(state);

        this._state = state;
    }
}