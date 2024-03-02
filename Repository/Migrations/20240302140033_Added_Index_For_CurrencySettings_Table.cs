using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class Added_Index_For_CurrencySettings_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: (short)1,
                columns: new[] { "CreationDate", "DeletionDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2024, 3, 2, 18, 0, 32, 716, DateTimeKind.Local).AddTicks(3518), new DateTime(2024, 3, 2, 14, 0, 32, 716, DateTimeKind.Utc).AddTicks(3520), new DateTime(2024, 3, 2, 14, 0, 32, 716, DateTimeKind.Utc).AddTicks(3519) });

            migrationBuilder.UpdateData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: (short)2,
                columns: new[] { "CreationDate", "DeletionDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2024, 3, 2, 18, 0, 32, 716, DateTimeKind.Local).AddTicks(3525), new DateTime(2024, 3, 2, 14, 0, 32, 716, DateTimeKind.Utc).AddTicks(3526), new DateTime(2024, 3, 2, 14, 0, 32, 716, DateTimeKind.Utc).AddTicks(3525) });

            migrationBuilder.UpdateData(
                table: "UserStatus",
                keyColumn: "Id",
                keyValue: (short)1,
                columns: new[] { "CreationDate", "DeletionDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2024, 3, 2, 18, 0, 32, 716, DateTimeKind.Local).AddTicks(3058), new DateTime(2024, 3, 2, 14, 0, 32, 716, DateTimeKind.Utc).AddTicks(3068), new DateTime(2024, 3, 2, 14, 0, 32, 716, DateTimeKind.Utc).AddTicks(3068) });

            migrationBuilder.UpdateData(
                table: "UserStatus",
                keyColumn: "Id",
                keyValue: (short)2,
                columns: new[] { "CreationDate", "DeletionDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2024, 3, 2, 18, 0, 32, 716, DateTimeKind.Local).AddTicks(3084), new DateTime(2024, 3, 2, 14, 0, 32, 716, DateTimeKind.Utc).AddTicks(3085), new DateTime(2024, 3, 2, 14, 0, 32, 716, DateTimeKind.Utc).AddTicks(3085) });

            migrationBuilder.CreateIndex(
                name: "IX_UserCurrencySettings_CurrencyTo_CurrencyFrom_UserActivityHistoryId",
                table: "UserCurrencySettings",
                columns: new[] { "CurrencyTo", "CurrencyFrom", "UserActivityHistoryId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserCurrencySettings_CurrencyTo_CurrencyFrom_UserActivityHistoryId",
                table: "UserCurrencySettings");

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
        }
    }
}
