using DQ.Core.Expressions.Visitors;
using DQ.Core.Specifications.Models;
using System.Linq.Expressions;

namespace DQ.Core.Specifications.Buidlers;

/// <summary>
/// Provides a fluent API for building specifications.
/// </summary>
/// <typeparam name="TEntity">
/// The entity type.
/// </typeparam>
public sealed class SpecificationBuilder<TEntity>
{
    #region Fields

    private SpecificationState<TEntity> _state;

    #endregion


    #region Constructors

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="SpecificationBuilder{TEntity}"/> class.
    /// </summary>
    internal SpecificationBuilder()
    {
        this._state = new SpecificationState<TEntity>();
    }

    #endregion


    #region Methods

    /// <summary>
    /// Adds a filtering criteria.
    /// </summary>
    /// <param name="criteria">
    /// The filtering expression.
    /// </param>
    /// <returns>
    /// The current builder.
    /// </returns>
    public SpecificationBuilder<TEntity> Where(
        Expression<Func<TEntity, bool>> criteria)
    {
        ArgumentNullException.ThrowIfNull(criteria);

        this._state = this._state with
        {
            Criteria = criteria
        };

        return this;
    }


    /// <summary>
    /// Adds another filtering criteria using AND.
    /// </summary>
    public SpecificationBuilder<TEntity> And(Expression<Func<TEntity, bool>> criteria)
    {
        ArgumentNullException.ThrowIfNull(criteria);

        if (this._state.Criteria is null)
        {
            this._state = this._state with
            {
                Criteria = criteria
            };

            return this;
        }

        var parameter = this._state.Criteria.Parameters[0];

        var replacedBody =
            new ReplaceExpressionVisitor(
                criteria.Parameters[0],
                parameter)
            .Visit(criteria.Body);

        var body =
            Expression.AndAlso(
                this._state.Criteria.Body,
                replacedBody!);

        this._state = this._state with
        {
            Criteria =
                Expression.Lambda<Func<TEntity, bool>>(
                    body,
                    parameter)
        };


        return this;
    }


    /// <summary>
    /// Adds another filtering criteria using OR.
    /// </summary>
    public SpecificationBuilder<TEntity> Or(Expression<Func<TEntity, bool>> criteria)
    {
        ArgumentNullException.ThrowIfNull(criteria);

        if (this._state.Criteria is null)
        {
            this._state = this._state with
            {
                Criteria = criteria
            };

            return this;
        }

        var parameter = this._state.Criteria.Parameters[0];

        var replacedBody =
            new ReplaceExpressionVisitor(
                criteria.Parameters[0],
                parameter)
            .Visit(criteria.Body);


        var body =
            Expression.OrElse(
                this._state.Criteria.Body,
                replacedBody!);


        this._state = this._state with
        {
            Criteria =
                Expression.Lambda<Func<TEntity, bool>>(
                    body,
                    parameter)
        };


        return this;
    }


    /// <summary>
    /// Adds an Entity Framework Core include expression.
    /// </summary>
    public SpecificationBuilder<TEntity> Include<TProperty>(
        Expression<Func<TEntity, TProperty>> navigation)
    {
        ArgumentNullException.ThrowIfNull(navigation);

        var includes =
            this._state.Includes
                .Append(
                    new IncludeDefinition<TEntity>(
                        ConvertExpression(navigation)))
                .ToArray();

        this._state = this._state with
        {
            Includes = includes
        };

        return this;
    }


    /// <summary>
    /// Enables no tracking queries.
    /// </summary>
    public SpecificationBuilder<TEntity> AsNoTracking()
    {
        this._state = this._state with
        {
            AsNoTracking = true
        };

        return this;
    }


    /// <summary>
    /// Enables split query execution.
    /// </summary>
    public SpecificationBuilder<TEntity> AsSplitQuery()
    {
        this._state = this._state with
        {
            AsSplitQuery = true
        };

        return this;
    }


    /// <summary>
    /// Adds ascending ordering.
    /// </summary>
    public SpecificationBuilder<TEntity> OrderBy<TKey>(
        Expression<Func<TEntity, TKey>> expression)
    {
        ArgumentNullException.ThrowIfNull(expression);

        var orders =
            this._state.Orders
                .Append(
                    new OrderDefinition<TEntity>(
                        expression,
                        false))
                .ToArray();

        this._state = this._state with
        {
            Orders = orders
        };

        return this;
    }


    /// <summary>
    /// Adds descending ordering.
    /// </summary>
    public SpecificationBuilder<TEntity> OrderByDescending<TKey>(
        Expression<Func<TEntity, TKey>> expression)
    {
        ArgumentNullException.ThrowIfNull(expression);

        var orders =
            this._state.Orders
                .Append(
                    new OrderDefinition<TEntity>(
                        expression,
                        true))
                .ToArray();

        this._state = this._state with
        {
            Orders = orders
        };

        return this;
    }


    /// <summary>
    /// Adds paging information.
    /// </summary>
    public SpecificationBuilder<TEntity> Page(
        int pageNumber,
        int pageSize)
    {
        if (pageNumber <= 0)
            throw new ArgumentOutOfRangeException(nameof(pageNumber));

        if (pageSize <= 0)
            throw new ArgumentOutOfRangeException(nameof(pageSize));


        this._state = this._state with
        {
            Skip = (pageNumber - 1) * pageSize,
            Take = pageSize
        };

        return this;
    }


    /// <summary>
    /// Creates the final specification.
    /// </summary>
    public Specification<TEntity> Build()
    {
        return new BuiltSpecification<TEntity>(this._state);
    }


    private static Expression<Func<TEntity, object>> ConvertExpression<TProperty>(Expression<Func<TEntity, TProperty>> expression)
    {
        ArgumentNullException.ThrowIfNull(expression);

        Expression body;

        if (typeof(TProperty).IsValueType)
        {
            body = Expression.Convert(expression.Body, typeof(object));
        }
        else
        {
            body = expression.Body;
        }

        return Expression.Lambda<Func<TEntity, object>>(
            body,
            expression.Parameters);
    }


    private sealed class BuiltSpecification<T>
        : Specification<T>
    {
        public BuiltSpecification(
            SpecificationState<T> state)
            : base(state)
        {
        }
    }

    #endregion
}