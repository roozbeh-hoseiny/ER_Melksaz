using ER.Melksaz.BuildingBlocks.Persistence.Abstractions;
using System.Data;
using System.Data.Common;

namespace ER.Melksaz.BuildingBlocks.Persistence.Core;

public abstract class DbSessionBase : IBaseDbSession
{
    #region " Fields "
    private bool _disposed;
    private readonly DbConnection _connection;
    private DbTransaction? _transaction;
    #endregion

    #region " Properties "
    public DbConnection Connection => this._connection;
    public DbTransaction? Transaction => this._transaction;
    #endregion

    #region " Constructor "
    protected DbSessionBase(DbConnection dbConnection)
    {
        this._connection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
    }
    #endregion

    public async ValueTask OpenConnectionAsync(CancellationToken cancellationToken = default)
    {
        this.ThrowIfDisposed();

        if (this._connection.State != ConnectionState.Open)
        {
            await this._connection.OpenAsync(cancellationToken);
        }
    }

    public async ValueTask BeginTransactionAsync(bool throwIfAlreadyStarted = false, CancellationToken cancellationToken = default)
    {
        this.ThrowIfDisposed();

        if (this.IsTransactionStarted())
        {
            if (throwIfAlreadyStarted)
                throw new InvalidOperationException("A transaction has already been started.");
            else
                return;
        }

        await this.OpenConnectionAsync(cancellationToken);

        this._transaction = await this._connection.BeginTransactionAsync(cancellationToken);
    }

    public async ValueTask CommitAsync(CancellationToken cancellationToken = default)
    {
        this.ThrowIfDisposed();

        if (!this.IsTransactionStarted())
            throw new InvalidOperationException("No active transaction to commit.");

        await this._transaction!.CommitAsync(cancellationToken);

        await this.DisposeTransactionAsync();
    }

    public async ValueTask RollbackAsync(CancellationToken cancellationToken = default)
    {
        this.ThrowIfDisposed();

        if (!this.IsTransactionStarted())
            return;

        await this._transaction!.RollbackAsync(cancellationToken);

        await this.DisposeTransactionAsync();
    }

    public bool IsTransactionStarted()
    {
        return this._transaction is not null;
    }

    #region " Dispose "
    private async ValueTask DisposeTransactionAsync()
    {
        if (this._transaction is not null)
        {
            await this._transaction.DisposeAsync();
            this._transaction = null;
        }
    }

    private void DisposeTransaction()
    {
        if (this._transaction is not null)
        {
            this._transaction.Dispose();
            this._transaction = null;
        }
    }

    private void ThrowIfDisposed()
    {
        if (this._disposed)
            throw new ObjectDisposedException(nameof(DbSessionBase));
    }

    public void Dispose()
    {
        if (this._disposed) return;

        this.DisposeTransaction();
        this._connection.Dispose();

        this._disposed = true;
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        if (this._disposed) return;

        if (this._transaction != null)
            await this._transaction.DisposeAsync();

        await this._connection.DisposeAsync();

        this._transaction = null;
        this._disposed = true;

        GC.SuppressFinalize(this);
    }


    #endregion
}
