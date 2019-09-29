using Microsoft.EntityFrameworkCore.Migrations;

namespace BouvetØlsmaking.Migrations
{
    public partial class TastingBeerResultNAme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BeerClassName",
                table: "TastingResult",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BeerClassName",
                table: "TastingResult");
        }
    }
}
