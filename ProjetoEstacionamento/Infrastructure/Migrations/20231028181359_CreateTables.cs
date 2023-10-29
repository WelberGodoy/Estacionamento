using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Estacionamento.Migrations
{
    /// <inheritdoc />
    public partial class CreateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VAGA",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    quantidade = table.Column<int>(type: "int", nullable: false),
                    tipo_vaga = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VAGA", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "VEICULO",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    placa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    entrada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    saida = table.Column<DateTime>(type: "datetime2", nullable: true),
                    tipo_veiculo = table.Column<int>(type: "int", nullable: false),
                    id_vaga = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VEICULO", x => x.id);
                    table.ForeignKey(
                        name: "FK_VEICULO_VAGA_id_vaga",
                        column: x => x.id_vaga,
                        principalTable: "VAGA",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_VAGA_tipo_vaga",
                table: "VAGA",
                column: "tipo_vaga",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VEICULO_id_vaga",
                table: "VEICULO",
                column: "id_vaga");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VEICULO");

            migrationBuilder.DropTable(
                name: "VAGA");
        }
    }
}
