using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class UserCurrencySettingsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.CreateTable(
                name: "UserCurrencySettings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrencyFrom = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    CurrencyTo = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    Position = table.Column<int>(type: "int", nullable: false),
                    UserActivityHistoryId = table.Column<long>(type: "bigint", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCurrencySettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCurrencySettings_UsersActivityHistories_UserActivityHistoryId",
                        column: x => x.UserActivityHistoryId,
                        principalTable: "UsersActivityHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: (short)1,
                columns: new[] { "CreationDate", "DeletionDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2024, 3, 1, 23, 50, 10, 559, DateTimeKind.Local).AddTicks(4595), new DateTime(2024, 3, 1, 19, 50, 10, 559, DateTimeKind.Utc).AddTicks(4596), new DateTime(2024, 3, 1, 19, 50, 10, 559, DateTimeKind.Utc).AddTicks(4595) });

            migrationBuilder.UpdateData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: (short)2,
                columns: new[] { "CreationDate", "DeletionDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2024, 3, 1, 23, 50, 10, 559, DateTimeKind.Local).AddTicks(4615), new DateTime(2024, 3, 1, 19, 50, 10, 559, DateTimeKind.Utc).AddTicks(4615), new DateTime(2024, 3, 1, 19, 50, 10, 559, DateTimeKind.Utc).AddTicks(4615) });

            migrationBuilder.UpdateData(
                table: "UserStatus",
                keyColumn: "Id",
                keyValue: (short)1,
                columns: new[] { "CreationDate", "DeletionDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2024, 3, 1, 23, 50, 10, 559, DateTimeKind.Local).AddTicks(4253), new DateTime(2024, 3, 1, 19, 50, 10, 559, DateTimeKind.Utc).AddTicks(4264), new DateTime(2024, 3, 1, 19, 50, 10, 559, DateTimeKind.Utc).AddTicks(4263) });

            migrationBuilder.UpdateData(
                table: "UserStatus",
                keyColumn: "Id",
                keyValue: (short)2,
                columns: new[] { "CreationDate", "DeletionDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2024, 3, 1, 23, 50, 10, 559, DateTimeKind.Local).AddTicks(4407), new DateTime(2024, 3, 1, 19, 50, 10, 559, DateTimeKind.Utc).AddTicks(4408), new DateTime(2024, 3, 1, 19, 50, 10, 559, DateTimeKind.Utc).AddTicks(4408) });

            migrationBuilder.CreateIndex(
                name: "IX_UserCurrencySettings_UserActivityHistoryId",
                table: "UserCurrencySettings",
                column: "UserActivityHistoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCurrencySettings");

            migrationBuilder.UpdateData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: (short)1,
                columns: new[] { "CreationDate", "DeletionDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 5, 21, 57, 46, 238, DateTimeKind.Local).AddTicks(4716), new DateTime(2024, 1, 5, 17, 57, 46, 238, DateTimeKind.Utc).AddTicks(4719), new DateTime(2024, 1, 5, 17, 57, 46, 238, DateTimeKind.Utc).AddTicks(4718) });

            migrationBuilder.UpdateData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: (short)2,
                columns: new[] { "CreationDate", "DeletionDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 5, 21, 57, 46, 238, DateTimeKind.Local).AddTicks(5096), new DateTime(2024, 1, 5, 17, 57, 46, 238, DateTimeKind.Utc).AddTicks(5098), new DateTime(2024, 1, 5, 17, 57, 46, 238, DateTimeKind.Utc).AddTicks(5097) });

            migrationBuilder.UpdateData(
                table: "UserStatus",
                keyColumn: "Id",
                keyValue: (short)1,
                columns: new[] { "CreationDate", "DeletionDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 5, 21, 57, 46, 236, DateTimeKind.Local).AddTicks(6112), new DateTime(2024, 1, 5, 17, 57, 46, 237, DateTimeKind.Utc).AddTicks(6002), new DateTime(2024, 1, 5, 17, 57, 46, 237, DateTimeKind.Utc).AddTicks(6001) });

            migrationBuilder.UpdateData(
                table: "UserStatus",
                keyColumn: "Id",
                keyValue: (short)2,
                columns: new[] { "CreationDate", "DeletionDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 5, 21, 57, 46, 237, DateTimeKind.Local).AddTicks(6912), new DateTime(2024, 1, 5, 17, 57, 46, 237, DateTimeKind.Utc).AddTicks(6920), new DateTime(2024, 1, 5, 17, 57, 46, 237, DateTimeKind.Utc).AddTicks(6920) });
        }
    }
}
