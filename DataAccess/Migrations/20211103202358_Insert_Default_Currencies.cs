using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class Insert_Default_Currencies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO [dbo].[Currencies]
  VALUES ('AMD', 'Armenian Dram'),
		 ('USD', 'US Dollar'),
		 ('EUR', 'Euro'),
		 ('RUB', 'Russian Ruble'),
		 ('GBP', 'Great Britain Pound')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [dbo].[Currencies]");
        }
    }
}
