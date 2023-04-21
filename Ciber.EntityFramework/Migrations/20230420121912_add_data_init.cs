using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ciber.EntityFramework.Migrations
{
    public partial class add_data_init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sqlFile = Path.Combine("queryInsertData.Sql");
            migrationBuilder.Sql(File.ReadAllText(sqlFile));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
