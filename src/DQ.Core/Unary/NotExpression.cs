using DQ.Core.QueryModel;

namespace DQ.Core.Unary;

public sealed record NotExpression(QueryExpression Expression) : QueryExpression;