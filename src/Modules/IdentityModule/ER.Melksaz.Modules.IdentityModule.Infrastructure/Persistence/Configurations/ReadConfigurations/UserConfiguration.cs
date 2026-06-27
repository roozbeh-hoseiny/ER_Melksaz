using ER.Melksaz.BuildingBlocks.Persistence.EFAccess.Configurations;
using ER.Melksaz.Modules.IdentityModule.Application.Persistence.Models;
using ER.Melksaz.Modules.IdentityModule.Domain.ValueObjects;
using ER.Melksaz.Security.Encryption;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ER.Melksaz.Modules.IdentityModule.Infrastructure.Persistence.Configurations.ReadConfigurations;

internal sealed class UserConfiguration : EntityTypeConfigurationDependency<UserReadQuery>
{
    private readonly IEncryptionService _encryptionService = null!;

    public override string SchemaName => IdentitySchemaInfo.Instance.Name;
    public override EntityConfigType EntityConfigType => EntityConfigType.Read;


    public UserConfiguration() : base() { }
    public UserConfiguration(IEncryptionService encryptionService) : base()
    {
        this._encryptionService = encryptionService;
    }

    public override void Configure(EntityTypeBuilder<UserReadQuery> builder)
    {
        builder.ToTable(IdentitySchemaInfo.Instance.Users_TableName);
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Mobile)
            .HasColumnType($"varchar(2000)")
            .HasConversion(
                rawData => this._encryptionService.UnsafeEncrypt(rawData.Value),
                dbData => Mobile.CreateUnsafe(this._encryptionService.UnsafeDecrypt(dbData)));

        builder.Property(e => e.NationalCode)
           .HasColumnType($"varchar(2000)")
           .HasConversion(
                   rawData => this._encryptionService.UnsafeEncrypt(rawData.Value),
                   dbData => NationalCode.CreateUnsafe(this._encryptionService.UnsafeDecrypt(dbData)));

        builder.Property(e => e.Email)
         .HasColumnType($"varchar(2000)")
         .HasConversion(
                 rawData => this._encryptionService.UnsafeEncrypt(rawData.Value),
                 dbData => Email.CreateUnsafe(this._encryptionService.UnsafeDecrypt(dbData)));

        builder.ComplexProperty(
            e => e.Password,
            p =>
            {
                p.Property(x => x.Hash).HasColumnName("Password_Hash");
                p.Property(x => x.Salt).HasColumnName("Password_Salt");
            });
    }
}
