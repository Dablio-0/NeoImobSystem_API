using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeoImobSystem_API.Migrations
{
    /// <inheritdoc />
    public partial class Correcao_FKCasaContrato : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContratoInquilino_Contratos_ContratoId",
                table: "ContratoInquilino");

            migrationBuilder.DropForeignKey(
                name: "FK_ContratoInquilino_Inquilinos_InquilinoId",
                table: "ContratoInquilino");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContratoInquilino",
                table: "ContratoInquilino");

            migrationBuilder.RenameTable(
                name: "ContratoInquilino",
                newName: "ContratoInquilinos");

            migrationBuilder.RenameIndex(
                name: "IX_ContratoInquilino_InquilinoId",
                table: "ContratoInquilinos",
                newName: "IX_ContratoInquilinos_InquilinoId");

            migrationBuilder.RenameIndex(
                name: "IX_ContratoInquilino_ContratoId",
                table: "ContratoInquilinos",
                newName: "IX_ContratoInquilinos_ContratoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContratoInquilinos",
                table: "ContratoInquilinos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContratoInquilinos_Contratos_ContratoId",
                table: "ContratoInquilinos",
                column: "ContratoId",
                principalTable: "Contratos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContratoInquilinos_Inquilinos_InquilinoId",
                table: "ContratoInquilinos",
                column: "InquilinoId",
                principalTable: "Inquilinos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContratoInquilinos_Contratos_ContratoId",
                table: "ContratoInquilinos");

            migrationBuilder.DropForeignKey(
                name: "FK_ContratoInquilinos_Inquilinos_InquilinoId",
                table: "ContratoInquilinos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContratoInquilinos",
                table: "ContratoInquilinos");

            migrationBuilder.RenameTable(
                name: "ContratoInquilinos",
                newName: "ContratoInquilino");

            migrationBuilder.RenameIndex(
                name: "IX_ContratoInquilinos_InquilinoId",
                table: "ContratoInquilino",
                newName: "IX_ContratoInquilino_InquilinoId");

            migrationBuilder.RenameIndex(
                name: "IX_ContratoInquilinos_ContratoId",
                table: "ContratoInquilino",
                newName: "IX_ContratoInquilino_ContratoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContratoInquilino",
                table: "ContratoInquilino",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContratoInquilino_Contratos_ContratoId",
                table: "ContratoInquilino",
                column: "ContratoId",
                principalTable: "Contratos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContratoInquilino_Inquilinos_InquilinoId",
                table: "ContratoInquilino",
                column: "InquilinoId",
                principalTable: "Inquilinos",
                principalColumn: "Id");
        }
    }
}
