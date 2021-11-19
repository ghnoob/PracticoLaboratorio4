using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PracticoLaboratorio4.Migrations
{
    public partial class Pelicula : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Peliculas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Titulo = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    GeneroId = table.Column<int>(type: "INTEGER", nullable: false),
                    DirectorId = table.Column<int>(type: "INTEGER", nullable: false),
                    Resumen = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    FechaEstreno = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UrlImagenPortada = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    Trailer = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Peliculas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Peliculas_Directores_DirectorId",
                        column: x => x.DirectorId,
                        principalTable: "Directores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Peliculas_Generos_GeneroId",
                        column: x => x.GeneroId,
                        principalTable: "Generos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Peliculas_DirectorId",
                table: "Peliculas",
                column: "DirectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Peliculas_GeneroId",
                table: "Peliculas",
                column: "GeneroId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Peliculas");
        }
    }
}
