using ER.Melksaz.BuildingBlocks.Persistence.EFAccess.Configurations;
using ER.Melksaz.Modules.IdentityModule.Domain.Aggregates.AccountAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ER.Melksaz.Modules.IdentityModule.Infrastructure.Persistence.Configurations.WriteConfigurations;

internal sealed class AccountLedgerConfiguration : EntityTypeConfigurationDependency<AccountLedger>
{
    public override string SchemaName => IdentitySchemaInfo.Instance.Name;
    public override EntityConfigType EntityConfigType => EntityConfigType.Write;

    public override void Configure(EntityTypeBuilder<AccountLedger> builder)
    {
        builder.ToTable(IdentitySchemaInfo.Instance.AccountsLedger_TableName);
        builder.HasKey(e => e.Id);

        builder.Property(x => x.Title).HasMaxLength(50).IsUnicode(true);

        builder
            .Property(x => x.AccountLevel)
            .HasColumnName(IdentitySchemaInfo.Instance.AccountsLedger_AccountLevel_ColumnName)
            .HasColumnType("Varchar(50)")
            .HasConversion(
                x => x.GetDiscriminator(),
                discriminator => discriminator.ToAccountLedgerType());

        // Domain Value Object
        // EF does not persist this
        builder.Ignore(x => x.AccountCode);

        builder.Property<string>("Code")
          .HasColumnName("Code")
          .HasMaxLength(50)
          .IsRequired();


        builder.Property<string>("GroupCode")
            .HasColumnName("GroupCode")
            .HasMaxLength(50)
            .IsRequired();


        builder.Property<string?>("GeneralCode")
            .HasColumnName("GeneralCode")
            .HasMaxLength(50);


        builder.Property<string?>("SubsidiaryCode")
            .HasColumnName("SubsidiaryCode")
            .HasMaxLength(50);


        builder.HasIndex("Code").IsUnique();
    }
}

public sealed class AccountLedgerMaterializationInterceptor : IMaterializationInterceptor
{
    public object InitializedInstance(
        MaterializationInterceptionData materializationData,
        object entity)
    {
        if (entity is AccountLedger ledger)
        {
            ledger.LoadCode();
        }

        return entity;
    }
}

public sealed class AccountLedgerSaveInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var context = eventData.Context;

        if (context is null)
            return base.SavingChangesAsync(eventData, result, cancellationToken);


        foreach (var entry in context.ChangeTracker
                     .Entries<AccountLedger>())
        {
            if (entry.State != EntityState.Added &&
                entry.State != EntityState.Modified)
                continue;


            var code = entry.Entity.AccountCode;

            entry.Property("Code").CurrentValue = code.FullCode;

            switch (code)
            {
                case GroupLedgerCode group:
                    entry.Property("GroupCode").CurrentValue = group.Code;
                    entry.Property("GeneralCode").CurrentValue = string.Empty;
                    entry.Property("SubsidiaryCode").CurrentValue = string.Empty;
                    break;

                case GeneralLedgerCode general:
                    entry.Property("GroupCode").CurrentValue = general.GroupCode.Code;
                    entry.Property("GeneralCode").CurrentValue = general.Code;
                    entry.Property("SubsidiaryCode").CurrentValue = string.Empty;
                    break;

                case SubsidaryLedgerCode subsidiary:
                    entry.Property("GroupCode").CurrentValue = subsidiary.GeneralCode.GroupCode.Code;
                    entry.Property("GeneralCode").CurrentValue = subsidiary.GeneralCode.Code;
                    entry.Property("SubsidiaryCode").CurrentValue = subsidiary.Code;

                    break;
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}