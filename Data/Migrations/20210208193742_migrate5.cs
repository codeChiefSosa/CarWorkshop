using Microsoft.EntityFrameworkCore.Migrations;

namespace CarWorkshop.Data.Migrations
{
    public partial class migrate5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Marka",
                table: "Cars");

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "Cars",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brand",
                table: "Cars");

            migrationBuilder.AddColumn<string>(
                name: "Marka",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
