using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ER.Melksaz.Modules.IdentityModule.Infrastructure._migrations
{
    /// <inheritdoc />
    public partial class AccountLedger3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                schema: "Identity",
                table: "AccountsLedger",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GeneralCode",
                schema: "Identity",
                table: "AccountsLedger",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GroupCode",
                schema: "Identity",
                table: "AccountsLedger",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SubsidiaryCode",
                schema: "Identity",
                table: "AccountsLedger",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountsLedger_Code",
                schema: "Identity",
                table: "AccountsLedger",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AccountsLedger_Code",
                schema: "Identity",
                table: "AccountsLedger");

            migrationBuilder.DropColumn(
                name: "Code",
                schema: "Identity",
                table: "AccountsLedger");

            migrationBuilder.DropColumn(
                name: "GeneralCode",
                schema: "Identity",
                table: "AccountsLedger");

            migrationBuilder.DropColumn(
                name: "GroupCode",
                schema: "Identity",
                table: "AccountsLedger");

            migrationBuilder.DropColumn(
                name: "SubsidiaryCode",
                schema: "Identity",
                table: "AccountsLedger");
        }
    }
}
