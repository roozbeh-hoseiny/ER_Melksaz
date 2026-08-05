using DQ.Core.Expressions;
using DQ.Core.Specifications.Models;
using System.Linq.Expressions;

namespace DQ.Core.Specifications.Buidlers;

/// <summary>
/// Provides a fluent API for creating specifications.
/// </summary>
/// <typeparam name="TEntity">
/// The entity type.
/// </typeparam>
public sealed class SpecificationBuilder<TEntity>
{
    private SpecificationState<TEntity> _state;

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="SpecificationBuilder{TEntity}"/> class.
    /// </summary>
    internal SpecificationBuilder()
    {
        this._state = new SpecificationState<TEntity>();
    }


    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="SpecificationBuilder{TEntity}"/> class.
    /// </summary>
    /// <param name="state">
    /// The initial specification state.
    /// </param>
    internal SpecificationBuilder(SpecificationState<TEntity> state)
    {
        ArgumentNullException.ThrowIfNull(state);

        this._state = state;
    }


    /// <summary>
    /// Adds the main filtering criteria.
    /// </summary>
    /// <param name="criteria">
    /// The filtering expression.
    /// </param>
    /// <returns>
    /// The current builder instance.
    /// </returns>
    public SpecificationBuilder<TEntity> Where(Expression<Func<TEntity, bool>> criteria)
    {
        ArgumentNullException.ThrowIfNull(criteria);

        this._state = this._state with
        {
            Criteria = criteria
        };

        return this;
    }


    /// <summary>
    /// Combines the current criteria with another criteria using AND.
    /// </summary>
    /// <param name="criteria">
    /// The criteria expression.
    /// </param>
    /// <returns>
    /// The current builder instance.
    /// </returns>
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

        var replacedBody = ExpressionParameterReplacer.Replace(
                criteria.Body,
                criteria.Parameters[0],
                parameter);

        var body = Expression.AndAlso(
                this._state.Criteria.Body,
                replacedBody);

        this._state = this._state with
        {
            Criteria = Expression.Lambda<Func<TEntity, bool>>(
                    body,
                    parameter)
        };

        return this;
    }


    /// <summary>
    /// Combines the current criteria with another criteria using OR.
    /// </summary>
    /// <param name="criteria">
    /// The criteria expression.
    /// </param>
    /// <returns>
    /// The current builder instance.
    /// </returns>
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

        var replacedBody = ExpressionParameterReplacer.Replace(
                criteria.Body,
                criteria.Parameters[0],
                parameter);

        var body = Expression.OrElse(
                this._state.Criteria.Body,
                replacedBody);

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
    /// Adds an include expression.
    /// </summary>
    /// <typeparam name="TProperty">
    /// Navigation property type.
    /// </typeparam>
    /// <param name="navigation">
    /// Navigation expression.
    /// </param>
    /// <returns>
    /// The current builder instance.
    /// </returns>
    public SpecificationBuilder<TEntity> Include<TProperty>(Expression<Func<TEntity, TProperty>> navigation)
    {
        ArgumentNullException.ThrowIfNull(navigation);

        var include =
            new IncludeDefinition<TEntity>(
                ConvertExpression(navigation));

        this._state = this._state with
        {
            Includes = [.. this._state.Includes, include]
        };

        return this;
    }

    /// <summary>
    /// Enables query tracking.
    /// </summary>
    /// <returns>
    /// The current builder instance.
    /// </returns>
    public SpecificationBuilder<TEntity> AsTracking() => this.AsTracking(false);

    /// <summary>
    /// Disables query tracking.
    /// </summary>
    /// <returns>
    /// The current builder instance.
    /// </returns>
    public SpecificationBuilder<TEntity> AsNoTracking() => this.AsTracking(true);


    /// <summary>
    /// Enables split query execution.
    /// </summary>
    /// <returns>
    /// The current builder instance.
    /// </returns>
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
    /// <typeparam name="TKey">
    /// Ordering key type.
    /// </typeparam>
    /// <param name="expression">
    /// Ordering expression.
    /// </param>
    /// <returns>
    /// The current builder instance.
    /// </returns>
    public SpecificationBuilder<TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> expression) => this.OrderBy(expression, false);


    /// <summary>
    /// Adds descending ordering.
    /// </summary>
    /// <typeparam name="TKey">
    /// Ordering key type.
    /// </typeparam>
    /// <param name="expression">
    /// Ordering expression.
    /// </param>
    /// <returns>
    /// The current builder instance.
    /// </returns>
    public SpecificationBuilder<TEntity> OrderByDescending<TKey>(Expression<Func<TEntity, TKey>> expression) => this.OrderBy(expression, true);

    /// <summary>
    /// Adds paging information.
    /// </summary>
    /// <param name="pageNumber">
    /// Page number starting from one.
    /// </param>
    /// <param name="pageSize">
    /// Number of items per page.
    /// </param>
    /// <returns>
    /// The current builder instance.
    /// </returns>
    public SpecificationBuilder<TEntity> Page(
        int pageNumber,
        int pageSize)
    {
        if (pageNumber <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(pageNumber));
        }

        if (pageSize <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(pageSize));
        }

        this._state = this._state with
        {
            Skip = (pageNumber - 1) * pageSize,
            Take = pageSize
        };

        return this;
    }


    /// <summary>
    /// Creates the final specification instance.
    /// </summary>
    /// <returns>
    /// A built specification.
    /// </returns>
    public Specification<TEntity> Build()
    {
        return new BuiltSpecification<TEntity>(this._state);
    }

    private static Expression<Func<TEntity, object>> ConvertExpression<TProperty>(Expression<Func<TEntity, TProperty>> expression)
    {
        Expression body = expression.Body;

        if (typeof(TProperty).IsValueType)
        {
            body =
                Expression.Convert(
                    body,
                    typeof(object));
        }

        return Expression.Lambda<Func<TEntity, object>>(
            body,
            expression.Parameters);
    }
    private SpecificationBuilder<TEntity> AsTracking(bool asNoTracking)
    {
        this._state = this._state with
        {
            AsNoTracking = asNoTracking
        };

        return this;
    }
    private SpecificationBuilder<TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> expression, bool descending)
    {
        ArgumentNullException.ThrowIfNull(expression);

        var order = new OrderDefinition<TEntity>(
                expression,
                descending);

        this._state = this._state with
        {
            Orders = [.. this._state.Orders, order]
        };

        return this;
    }

    private sealed class BuiltSpecification<T> : Specification<T>
    {
        public BuiltSpecification(SpecificationState<T> state) : base(state)
        {
        }
    }
}