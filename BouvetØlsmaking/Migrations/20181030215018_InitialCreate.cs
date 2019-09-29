using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BouvetØlsmaking.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Beerstyle",
                columns: table => new
                {
                    BeerstyleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beerstyle", x => x.BeerstyleId);
                });

            migrationBuilder.CreateTable(
                name: "Brewery",
                columns: table => new
                {
                    BreweryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Website = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brewery", x => x.BreweryId);
                });

            migrationBuilder.CreateTable(
                name: "Taster",
                columns: table => new
                {
                    TasterId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmailAddress = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    IsAdmin = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taster", x => x.TasterId);
                });

            migrationBuilder.CreateTable(
                name: "Tasting",
                columns: table => new
                {
                    TastingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsBlind = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasting", x => x.TastingId);
                });

            migrationBuilder.CreateTable(
                name: "Vote",
                columns: table => new
                {
                    VoteId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BeerId = table.Column<int>(nullable: false),
                    TastingId = table.Column<int>(nullable: false),
                    TasterId = table.Column<int>(nullable: false),
                    Taste = table.Column<int>(nullable: false),
                    Appearance = table.Column<int>(nullable: false),
                    Overall = table.Column<int>(nullable: false),
                    Note = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vote", x => x.VoteId);
                });

            migrationBuilder.CreateTable(
                name: "Beer",
                columns: table => new
                {
                    BeerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BeerStyleId = table.Column<int>(nullable: false),
                    BreweryId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ABV = table.Column<double>(nullable: false),
                    RateBeerLink = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beer", x => x.BeerId);
                    table.ForeignKey(
                        name: "FK_Beer_Beerstyle_BeerStyleId",
                        column: x => x.BeerStyleId,
                        principalTable: "Beerstyle",
                        principalColumn: "BeerstyleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Beer_Brewery_BreweryId",
                        column: x => x.BreweryId,
                        principalTable: "Brewery",
                        principalColumn: "BreweryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TastingBeer",
                columns: table => new
                {
                    TastingBeerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TastingId = table.Column<int>(nullable: false),
                    BeerId = table.Column<int>(nullable: false),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TastingBeer", x => x.TastingBeerId);
                    table.ForeignKey(
                        name: "FK_TastingBeer_Beer_BeerId",
                        column: x => x.BeerId,
                        principalTable: "Beer",
                        principalColumn: "BeerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TastingBeer_Tasting_TastingId",
                        column: x => x.TastingId,
                        principalTable: "Tasting",
                        principalColumn: "TastingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Beer_BeerStyleId",
                table: "Beer",
                column: "BeerStyleId");

            migrationBuilder.CreateIndex(
                name: "IX_Beer_BreweryId",
                table: "Beer",
                column: "BreweryId");

            migrationBuilder.CreateIndex(
                name: "IX_TastingBeer_BeerId",
                table: "TastingBeer",
                column: "BeerId");

            migrationBuilder.CreateIndex(
                name: "IX_TastingBeer_TastingId",
                table: "TastingBeer",
                column: "TastingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Taster");

            migrationBuilder.DropTable(
                name: "TastingBeer");

            migrationBuilder.DropTable(
                name: "Vote");

            migrationBuilder.DropTable(
                name: "Beer");

            migrationBuilder.DropTable(
                name: "Tasting");

            migrationBuilder.DropTable(
                name: "Beerstyle");

            migrationBuilder.DropTable(
                name: "Brewery");
        }
    }
}
