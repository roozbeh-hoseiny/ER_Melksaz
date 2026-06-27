using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ER.Melksaz.BuildingBlocks.Persistence.EFAccess.Configurations;

public abstract class EntityTypeConfigurationDependency<TEntity>
    : EntityTypeConfigurationDependency, IEntityTypeConfiguration<TEntity>
    where TEntity : class
{
    protected EntityTypeConfigurationDependency()
    {

    }

    public abstract void Configure(EntityTypeBuilder<TEntity> builder);

    public override void Configure(ModelBuilder modelBuilder)
        => this.Configure(modelBuilder.Entity<TEntity>());
}