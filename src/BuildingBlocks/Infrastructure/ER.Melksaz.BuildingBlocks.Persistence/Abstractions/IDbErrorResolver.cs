using Microsoft.EntityFrameworkCore;

namespace ER.Melksaz.BuildingBlocks.Persistence.Abstractions;

public interface IDbErrorResolver
{
    bool IsUniqueConstraintError(Exception ex, DbContext context);
}

public sealed class DbErrorResolverDefault : IDbErrorResolver
{
    private readonly IEnumerable<IDbErrorDetector> _detectors;

    public DbErrorResolverDefault(IEnumerable<IDbErrorDetector> detectors)
    {
        this._detectors = detectors;
    }
    public bool IsUniqueConstraintError(Exception ex, DbContext context)
    {
        var provider = context.Database.ProviderName;
        var detector = this._detectors.FirstOrDefault(d => d.ProviderName.Equals(provider, StringComparison.InvariantCultureIgnoreCase));
        return detector?.IsUniqueConstraintError(ex) ?? false;
    }
}