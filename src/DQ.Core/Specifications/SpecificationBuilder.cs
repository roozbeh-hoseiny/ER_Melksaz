using DQ.Abstraction.Specifications;
using DQ.Abstraction.Specifications.Models;
using DQ.Core.Projections;
using System.Linq.Expressions;

namespace DQ.Core.Specifications;

public sealed class SpecificationBuilder<TEntity> : ISpecificationBuilder<TEntity>
{
    private SpecificationState<TEntity> _state;

    public SpecificationBuilder()
    {
        this._state = new SpecificationState<TEntity>();
    }

    public ISpecificationBuilder<TEntity> Where(Expression<Func<TEntity, bool>> expression)
    {
        ArgumentNullException.ThrowIfNull(expression);

        this._state = this._state with
        {
            Criteria = new CriteriaExpressionNode<TEntity>(expression)
        };

        return this;
    }

    public ISpecificationBuilder<TEntity> And(Expression<Func<TEntity, bool>> expression)
    {
        return this.Combine(new CriteriaExpressionNode<TEntity>(expression), CriteriaGroupOperator.And);
    }

    public ISpecificationBuilder<TEntity> Or(Expression<Func<TEntity, bool>> expression)
    {
        return this.Combine(new CriteriaExpressionNode<TEntity>(expression), CriteriaGroupOperator.Or);
    }

    public ISpecificationBuilder<TEntity> Include<TProperty>(Expression<Func<TEntity, TProperty>> expression)
    {
        this._state = this._state with
        {
            Includes =
                [
                    ..this._state.Includes,
                    new ExpressionIncludeDefinition<TEntity,TProperty>(
                        expression)
                ]
        };

        return this;
    }

    public ISpecificationBuilder<TEntity> Include(string navigationPath)
    {
        this._state = this._state with
        {
            Includes =
                [
                    ..this._state.Includes,
                    new StringIncludeDefinition<TEntity>(
                        navigationPath)
                ]
        };

        return this;
    }

    public ISpecificationBuilder<TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> expression)
    {
        this._state = this._state with
        {
            Orders =
                [
                    ..this._state.Orders,
                    new AscendingOrderDefinition<TEntity,TKey>(
                        expression)
                ]
        };


        return this;
    }

    public ISpecificationBuilder<TEntity> OrderByDescending<TKey>(Expression<Func<TEntity, TKey>> expression)
    {
        this._state = this._state with
        {
            Orders =
                [
                    ..this._state.Orders,
                    new DescendingOrderDefinition<TEntity,TKey>(
                        expression)
                ]
        };


        return this;
    }

    public ISpecificationBuilder<TEntity> Skip(int value)
    {
        this._state = this._state with
        {
            Skip = value
        };

        return this;
    }

    public ISpecificationBuilder<TEntity> Take(int value)
    {
        this._state = this._state with
        {
            Take = value
        };

        return this;
    }

    public ISpecificationBuilder<TEntity> AsSplitQuery()
    {
        this._state = this._state with
        {
            AsSplitQuery = true
        };

        return this;
    }
    public ISpecificationBuilder<TEntity> AsNoTracking()
    {
        this._state = this._state with
        {
            AsNoTracking = true,
            AsNoTrackingWithIdentityResolution = false
        };

        return this;
    }
    public ISpecificationBuilder<TEntity> AsNoTrackingWithIdentityResolution()
    {
        this._state = this._state with
        {
            AsNoTracking = false,
            AsNoTrackingWithIdentityResolution = true
        };

        return this;
    }
    public ISpecificationBuilder<TEntity> AsTracking()
    {
        this._state = this._state with
        {
            AsNoTracking = false,
            AsNoTrackingWithIdentityResolution = false
        };

        return this;
    }
    public ISpecificationBuilder<TEntity> Select<TProjection>(Expression<Func<TEntity, TProjection>> expression)
    {
        ArgumentNullException.ThrowIfNull(expression);

        this._state =
            this._state with
            {
                Projection = expression
            };

        return this;
    }
    public ISpecificationBuilder<TEntity> Select<TProjection>(ProjectionDefinition<TEntity, TProjection> definition)
    {
        this._state =
            this._state with
            {
                Projection = definition
            };
        return this;
    }

    public ISpecification<TEntity> Build()
    {
        return new BuiltSpecification<TEntity>(this._state);
    }

    private ISpecificationBuilder<TEntity> Combine(
        CriteriaNode<TEntity> node,
        CriteriaGroupOperator @operator)
    {
        ArgumentNullException.ThrowIfNull(node);


        if (this._state.Criteria is null)
        {
            this._state = this._state with
            {
                Criteria = node
            };


            return this;
        }

        if (this._state.Criteria is CriteriaGroupNode<TEntity> group && group.Operator == @operator)
        {
            this._state = this._state with
            {
                Criteria = group with
                {
                    Children = [.. group.Children, node]
                }
            };


            return this;
        }

        this._state = this._state with
        {
            Criteria = new CriteriaGroupNode<TEntity>(@operator, [this._state.Criteria, node])
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