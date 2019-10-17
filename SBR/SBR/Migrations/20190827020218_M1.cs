using Microsoft.EntityFrameworkCore.Migrations;

namespace SBR.Migrations
{
    public partial class M1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Categoria",
                table: "Propiedades",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "Propiedades",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Categoria",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Propiedades");
        }
    }
}
