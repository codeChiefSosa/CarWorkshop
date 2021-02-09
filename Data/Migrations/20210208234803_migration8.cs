using Microsoft.EntityFrameworkCore.Migrations;

namespace CarWorkshop.Data.Migrations
{
    public partial class migration8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Plates",
                table: "Cars",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Plates",
                table: "Cars");
        }
    }
}
