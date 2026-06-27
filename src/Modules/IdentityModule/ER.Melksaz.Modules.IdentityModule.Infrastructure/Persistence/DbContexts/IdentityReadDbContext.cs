using ER.Melksaz.BuildingBlocks.Persistence.Core;
using ER.Melksaz.BuildingBlocks.Persistence.EFAccess;
using ER.Melksaz.BuildingBlocks.Persistence.EFAccess.Configurations;
using ER.Melksaz.BuildingBlocks.Persistence.EFAccess.Helpers;
using ER.Melksaz.Modules.IdentityModule.Application.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace ER.Melksaz.Modules.IdentityModule.Infrastructure.Persistence.DbContexts;

internal sealed class IdentityReadDbContext : SchemaBasedDbContextBase<IdentityReadDbContext>
{
    public static string SchemaName => IdentitySchemaInfo.Instance.Name;

    protected override DbConnectionMode Mode => DbConnectionMode.ReadOnly;

    public IQueryable<UserReadQuery> Users => this.Set<UserReadQuery>();


    public IdentityReadDbContext(
       DbContextOptions<IdentityReadDbContext> opts,
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
