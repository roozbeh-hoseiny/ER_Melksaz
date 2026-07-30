using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ER.Melksaz.Modules.IdentityModule.Infrastructure._migrations
{
    /// <inheritdoc />
    public partial class AccountLedger2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountLevel",
                schema: "Identity",
                table: "AccountsLedger",
                type: "Varchar(50)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountLevel",
                schema: "Identity",
                table: "AccountsLedger");
        }
    }
}
