using ER.Melksaz.BuildingBlocks.Persistence.Core;
using ER.Melksaz.BuildingBlocks.Persistence.EFAccess.Configurations;
using Microsoft.EntityFrameworkCore;

namespace ER.Melksaz.BuildingBlocks.Persistence.EFAccess;

public abstract class SchemaBasedDbContextBase<T> : DbContext where T : DbContext
{
    private readonly string _schemaName = string.Empty;
    private readonly IEnumerable<EntityTypeConfigurationDependency> _configurations = [];

    protected abstract DbConnectionMode Mode { get; }

    public SchemaBasedDbContextBase(DbContextOptions<T> opts) : this(
        opts,
        string.Empty,
        Array.Empty<EntityTypeConfigurationDependency>())
    { }

    public SchemaBasedDbContextBase(DbContextOptions<T> opts, string schemaName) : this(
        opts,
        schemaName,
        Array.Empty<EntityTypeConfigurationDependency>())
    { }

    public SchemaBasedDbContextBase(
        DbContextOptions<T> opts,
        string schemaName,
        IEnumerable<EntityTypeConfigurationDependency> configurations) : base(opts)
    {
        this._schemaName = schemaName?.Trim() ?? string.Empty;
        this._configurations = configurations;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (!string.IsNullOrWhiteSpace(this._schemaName))
        {
            _ = modelBuilder.HasDefaultSchema(this._schemaName);
        }

        var configs = this._configurations;
        if (this.Mode == DbConnectionMode.ReadWrite) configs = configs.Where(x => x.EntityConfigType.Equals(EntityConfigType.Write));
        else if (this.Mode == DbConnectionMode.ReadOnly) configs = configs.Where(x => x.EntityConfigType.Equals(EntityConfigType.Read));

        foreach (var configuration in configs)
        {
            if (string.IsNullOrWhiteSpace(this._schemaName) || configuration.SchemaName.Equals(this._schemaName))
            {
                configuration.Configure(modelBuilder);
            }
        }
        base.OnModelCreating(modelBuilder);
    }

    public string GetSchemaName() => this._schemaName;
}
