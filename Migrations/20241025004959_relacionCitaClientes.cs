using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoFinalLab.Migrations
{
    /// <inheritdoc />
    public partial class relacionCitaClientes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Cita_CitaId",
                table: "Clientes");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_CitaId",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "CitaId",
                table: "Clientes");

            migrationBuilder.CreateTable(
                name: "CitaCliente",
                columns: table => new
                {
                    CitasId = table.Column<int>(type: "int", nullable: false),
                    ClientesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CitaCliente", x => new { x.CitasId, x.ClientesId });
                    table.ForeignKey(
                        name: "FK_CitaCliente_Cita_CitasId",
                        column: x => x.CitasId,
                        principalTable: "Cita",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CitaCliente_Clientes_ClientesId",
                        column: x => x.ClientesId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CitaCliente_ClientesId",
                table: "CitaCliente",
                column: "ClientesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CitaCliente");

            migrationBuilder.AddColumn<int>(
                name: "CitaId",
                table: "Clientes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_CitaId",
                table: "Clientes",
                column: "CitaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Cita_CitaId",
                table: "Clientes",
                column: "CitaId",
                principalTable: "Cita",
                principalColumn: "Id");
        }
    }
}
