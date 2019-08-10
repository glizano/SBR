using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SBR.Migrations
{
    public partial class ModelMigration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CitaId",
                table: "Propiedades",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactoPropietario",
                table: "Propiedades",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Propiedades",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Moneda",
                table: "Propiedades",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NombrePropietario",
                table: "Propiedades",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Nombre = table.Column<string>(nullable: true),
                    Contacto = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Caracteristicas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Nombre = table.Column<string>(nullable: true),
                    Valor = table.Column<string>(nullable: true),
                    ClienteId = table.Column<int>(nullable: true),
                    PropiedadId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caracteristicas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Caracteristicas_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Caracteristicas_Propiedades_PropiedadId",
                        column: x => x.PropiedadId,
                        principalTable: "Propiedades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Citas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    FechaInicio = table.Column<DateTime>(nullable: false),
                    FechaFinal = table.Column<DateTime>(nullable: false),
                    ClienteId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Citas_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Propiedades_CitaId",
                table: "Propiedades",
                column: "CitaId");

            migrationBuilder.CreateIndex(
                name: "IX_Caracteristicas_ClienteId",
                table: "Caracteristicas",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Caracteristicas_PropiedadId",
                table: "Caracteristicas",
                column: "PropiedadId");

            migrationBuilder.CreateIndex(
                name: "IX_Citas_ClienteId",
                table: "Citas",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Propiedades_Citas_CitaId",
                table: "Propiedades",
                column: "CitaId",
                principalTable: "Citas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Propiedades_Citas_CitaId",
                table: "Propiedades");

            migrationBuilder.DropTable(
                name: "Caracteristicas");

            migrationBuilder.DropTable(
                name: "Citas");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropIndex(
                name: "IX_Propiedades_CitaId",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "CitaId",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "ContactoPropietario",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "Moneda",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "NombrePropietario",
                table: "Propiedades");
        }
    }
}
