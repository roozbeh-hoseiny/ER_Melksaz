using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ER.Melksaz.Modules.IdentityModule.Infrastructure._migrations
{
    /// <inheritdoc />
    public partial class A : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Identity");

            migrationBuilder.CreateTable(
                name: "OutboxMessages",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EventType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Event = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OccuredOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsIntegrationEvent = table.Column<bool>(type: "bit", nullable: false),
                    ProcessedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Error = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(26)", unicode: false, maxLength: 26, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NationalCode = table.Column<string>(type: "varchar(2000)", nullable: false),
                    Mobile = table.Column<string>(type: "varchar(2000)", nullable: false),
                    Email = table.Column<string>(type: "varchar(2000)", nullable: false),
                    Username = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Email_Hash = table.Column<byte[]>(type: "varbinary(50)", nullable: true),
                    Mobile_Hash = table.Column<byte[]>(type: "varbinary(50)", nullable: true),
                    NationalCode_Hash = table.Column<byte[]>(type: "varbinary(50)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Password_Hash = table.Column<byte[]>(type: "varbinary(50)", nullable: false),
                    Password_Salt = table.Column<byte[]>(type: "varbinary(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_EmailHash_Unique",
                schema: "Identity",
                table: "Users",
                column: "Email_Hash",
                unique: true,
                filter: "[Email_Hash] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_MobileHash_Unique",
                schema: "Identity",
                table: "Users",
                column: "Mobile_Hash",
                unique: true,
                filter: "[Mobile_Hash] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_NationalCodeHash_Unique",
                schema: "Identity",
                table: "Users",
                column: "NationalCode_Hash",
                unique: true,
                filter: "[NationalCode_Hash] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                schema: "Identity",
                table: "Users",
                column: "Username",
                unique: true,
                filter: "[Username] <> ''");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OutboxMessages",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "Identity");
        }
    }
}
