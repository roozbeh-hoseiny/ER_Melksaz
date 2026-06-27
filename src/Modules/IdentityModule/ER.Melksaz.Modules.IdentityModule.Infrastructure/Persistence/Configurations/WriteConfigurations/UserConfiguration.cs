using ER.Melksaz.BuildingBlocks.Persistence;
using ER.Melksaz.BuildingBlocks.Persistence.EFAccess.Configurations;
using ER.Melksaz.BuildingBlocks.Persistence.EFAccess.Extensions;
using ER.Melksaz.Modules.IdentityModule.Domain.Aggregates.UserAggregate;
using ER.Melksaz.Modules.IdentityModule.Domain.ValueObjects;
using ER.Melksaz.Security.Encryption;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ER.Melksaz.Modules.IdentityModule.Infrastructure.Persistence.Configurations.WriteConfigurations;

internal sealed class UserConfiguration : EntityTypeConfigurationDependency<User>
{
    private const string VarBinaryType = "varbinary(50)";
    private readonly IEncryptionService _encryptionService = null!;

    public override string SchemaName => IdentitySchemaInfo.Instance.Name;
    public override EntityConfigType EntityConfigType => EntityConfigType.Write;


    public UserConfiguration() : base() { }
    public UserConfiguration(IEncryptionService encryptionService) : base()
    {
        this._encryptionService = encryptionService;
    }

    public override void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(IdentitySchemaInfo.Instance.Users_TableName);
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).HasMaxLength(26).IsUnicode(false).IsRequired(true);

        builder.Property(e => e.FirstName).IsUnicode(true).HasMaxLength(FirstName.MaxLength);
        builder.Property(e => e.LastName).IsUnicode(true).HasMaxLength(LastName.MaxLength);
        builder.Property(e => e.Username).IsUnicode(false).HasMaxLength(Username.MaxLength);

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
                p.Property(x => x.Hash).HasColumnName("Password_Hash").HasColumnType(VarBinaryType);
                p.Property(x => x.Salt).HasColumnName("Password_Salt").HasColumnType(VarBinaryType);
            });

        builder.Property<byte[]?>(IdentitySchemaInfo.Instance.Users_MobileHash_ColumnName).HasColumnType(VarBinaryType);
        builder.Property<byte[]?>(IdentitySchemaInfo.Instance.Users_EmailHash_ColumnName).HasColumnType(VarBinaryType);
        builder.Property<byte[]?>(IdentitySchemaInfo.Instance.Users_NationalCodeHash_ColumnName).HasColumnType(VarBinaryType);

        builder
            .UseHashedProperty(nameof(User.Mobile), IdentitySchemaInfo.Instance.Users_MobileHash_ColumnName)
            .UseHashedProperty(nameof(User.Email), IdentitySchemaInfo.Instance.Users_EmailHash_ColumnName)
            .UseHashedProperty(nameof(User.NationalCode), IdentitySchemaInfo.Instance.Users_NationalCodeHash_ColumnName);

        builder.HasIndex(a => a.Username)
           .IsUnique()
           .HasFilter($"[{nameof(User.Username)}] <> ''");

        builder.HasIndex(IdentitySchemaInfo.Instance.Users_MobileHash_ColumnName)
           .IsUnique()
           .HasFilter($"[{IdentitySchemaInfo.Instance.Users_MobileHash_ColumnName}] IS NOT NULL")
           .HasDatabaseName(IdentitySchemaInfo.Users_Mobile_UniqueIndexName);

        builder.HasIndex(IdentitySchemaInfo.Instance.Users_EmailHash_ColumnName)
           .IsUnique()
           .HasFilter($"[{IdentitySchemaInfo.Instance.Users_EmailHash_ColumnName}] IS NOT NULL")
           .HasDatabaseName(IdentitySchemaInfo.Users_Email_UniqueIndexName);

        builder.HasIndex(IdentitySchemaInfo.Instance.Users_NationalCodeHash_ColumnName)
          .IsUnique()
          .HasFilter($"[{IdentitySchemaInfo.Instance.Users_NationalCodeHash_ColumnName}] IS NOT NULL")
          .HasDatabaseName(IdentitySchemaInfo.Users_NationalCode_UniqueIndexName);

        builder.Property<byte[]>(PersitenceConstants.RowVersionColumnName)
               .IsRowVersion()
               .IsConcurrencyToken();
    }
}
