using DQ.Abstraction.Specifications.Models;
using System.Linq.Expressions;

namespace DQ.Core.Specifications.Buidlers;

public static class CriteriaExpressionBuilder<TEntity>
{
    public static Expression<Func<TEntity, bool>>? Build(CriteriaNode<TEntity>? node)
    {
        if (node is null)
        {
            return null;
        }

        var parameter =
            Expression.Parameter(
                typeof(TEntity),
                "entity");

        var body =
            BuildExpression(
                node,
                parameter);

        return Expression.Lambda<Func<TEntity, bool>>(
            body,
            parameter);
    }


    private static Expression BuildExpression(
        CriteriaNode<TEntity> node,
        ParameterExpression parameter)
    {
        return node switch
        {
            CriteriaExpressionNode<TEntity> expressionNode
                => ReplaceParameter(
                    expressionNode.Expression,
                    parameter),


            CriteriaGroupNode<TEntity> groupNode
                => BuildGroupExpression(
                    groupNode,
                    parameter),


            _ =>
                throw new NotSupportedException(
                    $"Criteria node '{node.GetType().Name}' is not supported.")
        };
    }


    private static Expression BuildGroupExpression(
        CriteriaGroupNode<TEntity> group,
        ParameterExpression parameter)
    {
        if (group.Children.Count == 0)
        {
            throw new InvalidOperationException(
                "Criteria group cannot be empty.");
        }


        Expression? result = null;


        foreach (var child in group.Children)
        {
            var childExpression =
                BuildExpression(
                    child,
                    parameter);


            result =
                result is null
                    ? childExpression
                    : Combine(
                        result,
                        childExpression,
                        group.Operator);
        }


        return result!;
    }


    private static Expression Combine(
        Expression left,
        Expression right,
        CriteriaGroupOperator @operator)
    {
        return @operator switch
        {
            CriteriaGroupOperator.And
                => Expression.AndAlso(
                    left,
                    right),

            CriteriaGroupOperator.Or
                => Expression.OrElse(
                    left,
                    right),

            _ =>
                throw new NotSupportedException(
                    $"Criteria operator '{@operator}' is not supported.")
        };
    }


    private static Expression ReplaceParameter(
        LambdaExpression expression,
        ParameterExpression parameter)
    {
        return new ParameterReplaceVisitor(
                expression.Parameters[0],
                parameter)
            .Visit(expression.Body)!;
    }


    private sealed class ParameterReplaceVisitor
        : ExpressionVisitor
    {
        private readonly ParameterExpression _source;

        private readonly ParameterExpression _target;


        public ParameterReplaceVisitor(
            ParameterExpression source,
            ParameterExpression target)
        {
            this._source = source;
            this._target = target;
        }


        protected override Expression VisitParameter(
            ParameterExpression node)
        {
            return node == this._source
                ? this._target
                : base.VisitParameter(node);
        }
    }
}