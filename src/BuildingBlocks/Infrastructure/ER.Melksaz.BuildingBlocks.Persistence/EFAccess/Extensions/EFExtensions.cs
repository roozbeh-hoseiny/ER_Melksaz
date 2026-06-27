using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ER.Melksaz.BuildingBlocks.Persistence.EFAccess.Extensions;

public static class EFExtensions
{
    public static PropertyBuilder IsUlidId(this PropertyBuilder<string> src) =>
        src.HasMaxLength(26).IsUnicode(false).IsRequired(true);
}
