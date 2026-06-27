using System.Data.Common;

namespace ER.Melksaz.BuildingBlocks.Persistence.Abstractions;

public interface IBaseDbSession : IDisposable, IAsyncDisposable
{
    // we dont use IDbConnection, because System.Data.IDbConnection has no async version method
    DbConnection Connection { get; }
    DbTransaction? Transaction { get; }

    ValueTask OpenConnectionAsync(CancellationToken cancellationToken);
    ValueTask BeginTransactionAsync(bool throwIfAlreadyStarted, CancellationToken cancellationToken);
    ValueTask CommitAsync(CancellationToken cancellationToken);
    ValueTask RollbackAsync(CancellationToken cancellationToken);

    bool IsTransactionStarted();
}
