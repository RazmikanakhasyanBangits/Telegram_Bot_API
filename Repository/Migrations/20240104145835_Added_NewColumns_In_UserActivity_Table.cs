using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations;

public partial class Added_NewColumns_In_UserActivity_Table : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        _ = migrationBuilder.DropColumn(
            name: "ChatExternalId",
            table: "UsersActivityHistories");

        _ = migrationBuilder.AddColumn<string>(
            name: "Bio",
            table: "UsersActivityHistories",
            type: "nvarchar(max)",
            nullable: true);

        _ = migrationBuilder.AddColumn<string>(
            name: "Description",
            table: "UsersActivityHistories",
            type: "nvarchar(max)",
            nullable: true);

        _ = migrationBuilder.AddColumn<long>(
            name: "MessageExternalId",
            table: "ChatDetails",
            type: "bigint",
            nullable: false,
            defaultValue: 0L);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        _ = migrationBuilder.DropColumn(
            name: "Bio",
            table: "UsersActivityHistories");

        _ = migrationBuilder.DropColumn(
            name: "Description",
            table: "UsersActivityHistories");

        _ = migrationBuilder.DropColumn(
            name: "MessageExternalId",
            table: "ChatDetails");

        _ = migrationBuilder.AddColumn<long>(
            name: "ChatExternalId",
            table: "UsersActivityHistories",
            type: "bigint",
            nullable: false,
            defaultValue: 0L);
    }
}
