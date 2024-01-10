using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BankURL = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BotHistory",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChatId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommandText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reply = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BotHistory", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies_1", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Rates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromCurrency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    ToCurrency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    BuyValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SellValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Iteration = table.Column<int>(type: "int", nullable: false),
                    BankId = table.Column<int>(type: "int", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rates_Banks",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rates_Currencies",
                        column: x => x.FromCurrency,
                        principalTable: "Currencies",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rates_Currencies1",
                        column: x => x.ToCurrency,
                        principalTable: "Currencies",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rates_BankId",
                table: "Rates",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Rates_FromCurrency",
                table: "Rates",
                column: "FromCurrency");

            migrationBuilder.CreateIndex(
                name: "IX_Rates_ToCurrency",
                table: "Rates",
                column: "ToCurrency");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BotHistory");

            migrationBuilder.DropTable(
                name: "Rates");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "Currencies");
        }
    }
}
