using DQ.Abstraction.Specifications;
using DQ.Core.Specifications.Models;
using System.Linq.Expressions;

namespace DQ.Core.Specifications;

/// <summary>
/// Base implementation of a specification.
/// </summary>
/// <typeparam name="TEntity">
/// The entity type.
/// </typeparam>
public abstract class Specification<TEntity> : ISpecification<TEntity>
{
    #region Fields

    private readonly SpecificationState<TEntity> _state;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="Specification{TEntity}"/> class.
    /// </summary>
    protected Specification()
    {
        this._state = new SpecificationState<TEntity>();
    }


    /// <summary>
    /// Initializes a new instance from an existing state.
    /// </summary>
    protected Specification(SpecificationState<TEntity> state)
    {
        ArgumentNullException.ThrowIfNull(state);

        this._state = state;
    }

    #endregion

    #region Properties

    /// <inheritdoc />
    public virtual Expression<Func<TEntity, bool>>? Criteria
        => this._state.Criteria;


    /// <inheritdoc />
    public IReadOnlyList<LambdaExpression> Includes
        => this._state.Includes
            .Select(x => x.Expression)
            .Cast<LambdaExpression>()
            .ToArray();


    /// <inheritdoc />
    public IReadOnlyList<LambdaExpression> Orders
        => this._state.Orders
            .Select(x => x.Expression)
            .ToArray();


    /// <inheritdoc />
    public bool AsNoTracking
        => this._state.AsNoTracking;


    /// <inheritdoc />
    public bool AsSplitQuery
        => this._state.AsSplitQuery;


    /// <inheritdoc />
    public int? Skip
        => this._state.Skip;


    /// <inheritdoc />
    public int? Take
        => this._state.Take;

    #endregion

    #region Methods

    /// <summary>
    /// Gets the internal state of this specification.
    /// </summary>
    internal SpecificationState<TEntity> GetState()
    {
        return this._state;
    }

    #endregion

    /// <summary>
    /// Combines the current specification with another specification using
    /// a logical AND operation.
    /// </summary>
    /// <param name="specification">
    /// The specification to combine with the current instance.
    /// </param>
    /// <returns>
    /// A new specification representing the logical AND of both specifications.
    /// </returns>
    public Specification<TEntity> And(ISpecification<TEntity> specification)
    {
        ArgumentNullException.ThrowIfNull(specification);

        return new AndSpecification<TEntity>(this, specification);
    }

    /// <summary>
    /// Combines the current specification with another specification using
    /// a logical OR operation.
    /// </summary>
    /// <param name="specification">
    /// The specification to combine with the current instance.
    /// </param>
    /// <returns>
    /// A new specification representing the logical OR of both specifications.
    /// </returns>
    public Specification<TEntity> Or(ISpecification<TEntity> specification)
    {
        ArgumentNullException.ThrowIfNull(specification);

        return new OrSpecification<TEntity>(this, specification);
    }

    /// <summary>
    /// Negates the current specification.
    /// </summary>
    /// <returns>
    /// A new specification representing the logical NOT of the current specification.
    /// </returns>
    public Specification<TEntity> Not()
    {
        return new NotSpecification<TEntity>(this);
    }
}

