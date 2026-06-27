using Microsoft.EntityFrameworkCore;

namespace ER.Melksaz.BuildingBlocks.Persistence.EFAccess.Configurations;

public abstract class EntityTypeConfigurationDependency
{

    public abstract string SchemaName { get; }
    public abstract EntityConfigType EntityConfigType { get; }
    public abstract void Configure(ModelBuilder modelBuilder);
}
