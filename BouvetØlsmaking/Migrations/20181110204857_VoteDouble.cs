using Microsoft.EntityFrameworkCore.Migrations;

namespace BouvetØlsmaking.Migrations
{
    public partial class VoteDouble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Taste",
                table: "Vote",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<double>(
                name: "Overall",
                table: "Vote",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<double>(
                name: "Appearance",
                table: "Vote",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Taste",
                table: "Vote",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<int>(
                name: "Overall",
                table: "Vote",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<int>(
                name: "Appearance",
                table: "Vote",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
