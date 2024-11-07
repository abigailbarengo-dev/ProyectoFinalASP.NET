using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoFinalLab.Migrations
{
    /// <inheritdoc />
    public partial class Imagenes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Imagen",
                table: "Veterinario",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Imagen",
                table: "Mascotas",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imagen",
                table: "Veterinario");

            migrationBuilder.DropColumn(
                name: "Imagen",
                table: "Mascotas");
        }
    }
}
