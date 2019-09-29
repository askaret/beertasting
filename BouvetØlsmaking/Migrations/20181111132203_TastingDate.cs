using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BouvetØlsmaking.Migrations
{
    public partial class TastingDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TastingDate",
                table: "Tasting",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TastingDate",
                table: "Tasting");
        }
    }
}
