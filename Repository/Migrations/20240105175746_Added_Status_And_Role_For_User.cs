using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace DataAccess.Migrations
{
    public partial class Added_Status_And_Role_For_User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.AddColumn<short>(
                name: "RoleId",
                table: "UsersActivityHistories",
                type: "smallint",
                nullable: false,
                defaultValue: (short)2);

            _ = migrationBuilder.AddColumn<short>(
                name: "StatusId",
                table: "UsersActivityHistories",
                type: "smallint",
                nullable: false,
                defaultValue: (short)1);

            _ = migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_UserRole", x => x.Id);
                });

            _ = migrationBuilder.CreateTable(
                name: "UserStatus",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_UserStatus", x => x.Id);
                });

            _ = migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "CreationDate", "DeletionDate", "LastUpdatedDate", "Name" },
                values: new object[,]
                {
                    { (short)1, new DateTime(2024, 1, 5, 21, 57, 46, 238, DateTimeKind.Local).AddTicks(4716), new DateTime(2024, 1, 5, 17, 57, 46, 238, DateTimeKind.Utc).AddTicks(4719), new DateTime(2024, 1, 5, 17, 57, 46, 238, DateTimeKind.Utc).AddTicks(4718), "Admin" },
                    { (short)2, new DateTime(2024, 1, 5, 21, 57, 46, 238, DateTimeKind.Local).AddTicks(5096), new DateTime(2024, 1, 5, 17, 57, 46, 238, DateTimeKind.Utc).AddTicks(5098), new DateTime(2024, 1, 5, 17, 57, 46, 238, DateTimeKind.Utc).AddTicks(5097), "User" }
                });

            _ = migrationBuilder.InsertData(
                table: "UserStatus",
                columns: new[] { "Id", "CreationDate", "DeletionDate", "LastUpdatedDate", "Name" },
                values: new object[,]
                {
                    { (short)1, new DateTime(2024, 1, 5, 21, 57, 46, 236, DateTimeKind.Local).AddTicks(6112), new DateTime(2024, 1, 5, 17, 57, 46, 237, DateTimeKind.Utc).AddTicks(6002), new DateTime(2024, 1, 5, 17, 57, 46, 237, DateTimeKind.Utc).AddTicks(6001), "Active" },
                    { (short)2, new DateTime(2024, 1, 5, 21, 57, 46, 237, DateTimeKind.Local).AddTicks(6912), new DateTime(2024, 1, 5, 17, 57, 46, 237, DateTimeKind.Utc).AddTicks(6920), new DateTime(2024, 1, 5, 17, 57, 46, 237, DateTimeKind.Utc).AddTicks(6920), "Blocked" }
                });

            _ = migrationBuilder.CreateIndex(
                name: "IX_UsersActivityHistories_RoleId",
                table: "UsersActivityHistories",
                column: "RoleId");

            _ = migrationBuilder.CreateIndex(
                name: "IX_UsersActivityHistories_StatusId",
                table: "UsersActivityHistories",
                column: "StatusId");

            _ = migrationBuilder.AddForeignKey(
                name: "FK_UsersActivityHistories_UserRole_RoleId",
                table: "UsersActivityHistories",
                column: "RoleId",
                principalTable: "UserRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            _ = migrationBuilder.AddForeignKey(
                name: "FK_UsersActivityHistories_UserStatus_StatusId",
                table: "UsersActivityHistories",
                column: "StatusId",
                principalTable: "UserStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.DropForeignKey(
                name: "FK_UsersActivityHistories_UserRole_RoleId",
                table: "UsersActivityHistories");

            _ = migrationBuilder.DropForeignKey(
                name: "FK_UsersActivityHistories_UserStatus_StatusId",
                table: "UsersActivityHistories");

            _ = migrationBuilder.DropTable(
                name: "UserRole");

            _ = migrationBuilder.DropTable(
                name: "UserStatus");

            _ = migrationBuilder.DropIndex(
                name: "IX_UsersActivityHistories_RoleId",
                table: "UsersActivityHistories");

            _ = migrationBuilder.DropIndex(
                name: "IX_UsersActivityHistories_StatusId",
                table: "UsersActivityHistories");

            _ = migrationBuilder.DropColumn(
                name: "RoleId",
                table: "UsersActivityHistories");

            _ = migrationBuilder.DropColumn(
                name: "StatusId",
                table: "UsersActivityHistories");
        }
    }
}
