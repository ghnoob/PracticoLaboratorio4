using Microsoft.EntityFrameworkCore.Migrations;

namespace PracticoLaboratorio4.Migrations
{
    public partial class GeneroDescripcionUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Generos",
                type: "TEXT COLLATE NOCASE",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 20);

            migrationBuilder.CreateIndex(
                name: "IX_Generos_Descripcion",
                table: "Generos",
                column: "Descripcion",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Generos_Descripcion",
                table: "Generos");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Generos",
                type: "TEXT",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT COLLATE NOCASE",
                oldMaxLength: 20);
        }
    }
}
