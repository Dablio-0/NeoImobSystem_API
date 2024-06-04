using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeoImobSystem_API.Migrations
{
    /// <inheritdoc />
    public partial class Correcao_FKCasaContratoModelCasa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CasaId",
                table: "Contratos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<uint>(
                name: "CasaId",
                table: "Contratos",
                type: "INTEGER",
                nullable: true);
        }
    }
}
