using DQ.Abstraction.Specifications.Models;
using System.Linq.Expressions;

namespace DQ.Core.Specifications.Buidlers;

public sealed class SpecificationBuilder<TEntity>
{
    private SpecificationState<TEntity> _state;

    public SpecificationBuilder()
    {
        this._state = new SpecificationState<TEntity>();
    }

    public SpecificationBuilder<TEntity> Where(Expression<Func<TEntity, bool>> expression)
    {
        ArgumentNullException.ThrowIfNull(expression);

        this._state = this._state with
        {
            Criteria = new CriteriaExpressionNode<TEntity>(expression)
        };

        return this;
    }

    public SpecificationBuilder<TEntity> And(Expression<Func<TEntity, bool>> expression)
    {
        return this.Combine(new CriteriaExpressionNode<TEntity>(expression), CriteriaGroupOperator.And);
    }

    public SpecificationBuilder<TEntity> Or(Expression<Func<TEntity, bool>> expression)
    {
        return this.Combine(new CriteriaExpressionNode<TEntity>(expression), CriteriaGroupOperator.Or);
    }

    public SpecificationBuilder<TEntity> Include<TProperty>(Expression<Func<TEntity, TProperty>> expression)
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



    public SpecificationBuilder<TEntity> Include(string navigationPath)
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

    public SpecificationBuilder<TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> expression)
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

    public SpecificationBuilder<TEntity> OrderByDescending<TKey>(Expression<Func<TEntity, TKey>> expression)
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

    public SpecificationBuilder<TEntity> Skip(int value)
    {
        this._state = this._state with
        {
            Skip = value
        };

        return this;
    }

    public SpecificationBuilder<TEntity> Take(int value)
    {
        this._state = this._state with
        {
            Take = value
        };

        return this;
    }

    public SpecificationBuilder<TEntity> AsSplitQuery()
    {
        this._state = this._state with
        {
            AsSplitQuery = true
        };

        return this;
    }

    public SpecificationBuilder<TEntity> AsNoTracking()
    {
        this._state = this._state with
        {
            AsNoTracking = true,
            AsNoTrackingWithIdentityResolution = false
        };

        return this;
    }

    public SpecificationBuilder<TEntity> AsNoTrackingWithIdentityResolution()
    {
        this._state = this._state with
        {
            AsNoTracking = false,
            AsNoTrackingWithIdentityResolution = true
        };

        return this;
    }


    public SpecificationBuilder<TEntity> AsTracking()
    {
        this._state = this._state with
        {
            AsNoTracking = false,
            AsNoTrackingWithIdentityResolution = false
        };

        return this;
    }


    public Specification<TEntity> Build()
    {
        return new BuiltSpecification<TEntity>(this._state);
    }

    private SpecificationBuilder<TEntity> Combine(
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