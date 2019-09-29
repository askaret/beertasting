using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BouvetØlsmaking.Migrations
{
    public partial class Beerclass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BeerClassId",
                table: "Beer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Beerclass",
                columns: table => new
                {
                    BeerClassId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beerclass", x => x.BeerClassId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Beerclass");

            migrationBuilder.DropColumn(
                name: "BeerClassId",
                table: "Beer");
        }
    }
}
