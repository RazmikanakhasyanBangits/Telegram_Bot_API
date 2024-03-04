using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations;

public partial class Added_UserActivity_Table : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "UsersActivityHistories",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                UserExternalId = table.Column<long>(type: "bigint", nullable: false),
                ChatExternalId = table.Column<long>(type: "bigint", nullable: false),
                CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UsersActivityHistories", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "ChatDetails",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Response = table.Column<string>(type: "nvarchar(max)", nullable: true),
                CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                UserActivityHistoryId = table.Column<long>(type: "bigint", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ChatDetails", x => x.Id);
                table.ForeignKey(
                    name: "FK_ChatDetails_UsersActivityHistories_UserActivityHistoryId",
                    column: x => x.UserActivityHistoryId,
                    principalTable: "UsersActivityHistories",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_ChatDetails_Id",
            table: "ChatDetails",
            column: "Id");

        migrationBuilder.CreateIndex(
            name: "IX_ChatDetails_UserActivityHistoryId",
            table: "ChatDetails",
            column: "UserActivityHistoryId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "ChatDetails");

        migrationBuilder.DropTable(
            name: "UsersActivityHistories");
    }
}
