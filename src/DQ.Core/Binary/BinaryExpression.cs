using DQ.Core.QueryModel;

namespace DQ.Core.Binary;

public abstract record BinaryExpression : QueryExpression
{
    public QueryExpression Left { get; }
    public QueryExpression Right { get; }

    protected BinaryExpression(QueryExpression left, QueryExpression right)
    {
        this.Left = left;
        this.Right = right;
    }
}
