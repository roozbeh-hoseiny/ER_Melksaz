using ER.Melksaz.BuildingBlocks.Persistence.EFAccess.EFSpecification.Abstractions;
using System.Linq.Expressions;

namespace ER.Melksaz.BuildingBlocks.Persistence.EFAccess.EFSpecification.Core;

public abstract class EFSpecification<T, TResult> : EFSpecification<T>, ISpecification<T, TResult>
    where T : class
{
    private Expression<Func<T, TResult>>? _selector;
    public Expression<Func<T, TResult>> Selector => this._selector ?? throw new ArgumentNullException(nameof(this._selector));

    public ISpecification<T, TResult> Select(Expression<Func<T, TResult>> expression)
    {
        this._selector = expression;
        return this;
    }
}
