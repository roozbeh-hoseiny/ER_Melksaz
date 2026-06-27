using ER.Melksaz.BuildingBlocks.Persistence.Core;

namespace ER.Melksaz.Modules.IdentityModule.Infrastructure.Persistence;

internal sealed class IdentitySchemaInfo : SchemaInfoBase
{
    public static readonly IdentitySchemaInfo Instance = new();

    public string Users_TableName => "Users";
    public string Users_MobileHash_ColumnName => "Mobile_Hash";
    public string Users_EmailHash_ColumnName => "Email_Hash";
    public string Users_NationalCodeHash_ColumnName => "NationalCode_Hash";

    public const string Users_Mobile_UniqueIndexName = "IX_Users_MobileHash_Unique";
    public const string Users_Email_UniqueIndexName = "IX_Users_EmailHash_Unique";
    public const string Users_NationalCode_UniqueIndexName = "IX_Users_NationalCodeHash_Unique";

    public IdentitySchemaInfo() : base("Identity") { }
}
