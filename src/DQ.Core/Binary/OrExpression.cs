using DQ.Core.QueryModel;

namespace DQ.Core.Binary;

public sealed record OrExpression(QueryExpression Left, QueryExpression Right) : BinaryExpression(Left, Right);
