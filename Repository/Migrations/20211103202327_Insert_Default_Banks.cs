using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class Insert_Default_Banks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO  [dbo].[Banks]
VALUES(N'ԱՄԵՐԻԱ ԲԱՆԿ', 'https://ameriabank.am/'),
      (N'ԷՎՈԿԱ ԲԱՆԿ', 'https://www.evoca.am'),
	  (N'ԱԿԲԱ ԲԱՆԿ', 'https://www.acba.am/'),
	  (N'ԻՆԵԿՈ ԲԱՆԿ', 'https://www.inecobank.am/hy/Individual'),
	  (N'ՅՈւՆԻԲԱՆԿ', 'https://www.unibank.am/hy/')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [dbo].[Banks]");
        }
    }
}
