using ER.Melksaz.BuildingBlocks.Persistence.Core;
using ER.Melksaz.BuildingBlocks.Persistence.EFAccess;
using ER.Melksaz.BuildingBlocks.Persistence.EFAccess.Configurations;
using ER.Melksaz.BuildingBlocks.Persistence.EFAccess.Helpers;
using ER.Melksaz.BuildingBlocks.Persistence.Models;
using ER.Melksaz.Modules.IdentityModule.Domain.Aggregates.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace ER.Melksaz.Modules.IdentityModule.Infrastructure.Persistence.DbContexts;

internal sealed class IdentityWriteDbContext : SchemaBasedDbContextBase<IdentityWriteDbContext>
{
    public static string SchemaName => IdentitySchemaInfo.Instance.Name;
    protected override DbConnectionMode Mode => DbConnectionMode.ReadWrite;

    public DbSet<User> Users => this.Set<User>();
    public DbSet<OutboxMessage> OutboxMessages => this.Set<OutboxMessage>();

    public IdentityWriteDbContext(
        DbContextOptions<IdentityWriteDbContext> opts,
        IEnumerable<EntityTypeConfigurationDependency> configurations) :
        base(opts, SchemaName, configurations)
    {
    }
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        EFHelpers.ApplyAllValueConvertersAndValueComparers(
            configurationBuilder,
            [],
            InfrastructureAssemblyReference.Assembly);
    }
}
