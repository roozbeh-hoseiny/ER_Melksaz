using DQ.Core.QueryModel;

namespace DQ.Core.Binary;

public sealed record AndExpression(QueryExpression Left, QueryExpression Right) : BinaryExpression(Left, Right);