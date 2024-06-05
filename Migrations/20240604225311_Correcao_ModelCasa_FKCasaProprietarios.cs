using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeoImobSystem_API.Migrations
{
    /// <inheritdoc />
    public partial class Correcao_ModelCasa_FKCasaProprietarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProprietariosId",
                table: "Casas",
                type: "TEXT",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProprietariosId",
                table: "Casas");
        }
    }
}
