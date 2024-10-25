using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoFinalLab.Migrations
{
    /// <inheritdoc />
    public partial class VetCita : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NombreVet",
                table: "Cita",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NombreVet",
                table: "Cita");
        }
    }
}
