using System.Linq.Expressions;

namespace DQ.Abstraction.Specifications.Models;

public abstract record IncludeDefinition<TEntity>;
public sealed record ExpressionIncludeDefinition<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> Expression) : IncludeDefinition<TEntity>;
public sealed record StringIncludeDefinition<TEntity>(string NavigationPath) : IncludeDefinition<TEntity>;