using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BouvetØlsmaking.Migrations
{
    public partial class TastingBeerResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TastingResult",
                columns: table => new
                {
                    TastingBeerResultId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TastingId = table.Column<int>(nullable: false),
                    BeerId = table.Column<int>(nullable: false),
                    BeerClassId = table.Column<int>(nullable: false),
                    BeerName = table.Column<string>(nullable: true),
                    BreweryName = table.Column<string>(nullable: true),
                    BeerStyle = table.Column<string>(nullable: true),
                    Abv = table.Column<double>(nullable: false),
                    BreweryUrl = table.Column<string>(nullable: true),
                    RateBeerUrl = table.Column<string>(nullable: true),
                    ScoreTaste = table.Column<double>(nullable: false),
                    ScoreAppearance = table.Column<double>(nullable: false),
                    ScoreOverall = table.Column<double>(nullable: false),
                    ScoreFinal = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TastingResult", x => x.TastingBeerResultId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TastingResult");
        }
    }
}
