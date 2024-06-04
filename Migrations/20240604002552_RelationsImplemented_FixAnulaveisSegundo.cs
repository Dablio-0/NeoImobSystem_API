using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeoImobSystem_API.Migrations
{
    /// <inheritdoc />
    public partial class RelationsImplemented_FixAnulaveisSegundo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CasaProprietarios_Casas_CasaId",
                table: "CasaProprietarios");

            migrationBuilder.DropForeignKey(
                name: "FK_CasaProprietarios_Proprietarios_ProprietarioId",
                table: "CasaProprietarios");

            migrationBuilder.DropForeignKey(
                name: "FK_ContratoInquilino_Contratos_ContratoId",
                table: "ContratoInquilino");

            migrationBuilder.DropForeignKey(
                name: "FK_ContratoInquilino_Inquilinos_InquilinoId",
                table: "ContratoInquilino");

            migrationBuilder.AlterColumn<uint>(
                name: "InquilinoId",
                table: "ContratoInquilino",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(uint),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<uint>(
                name: "ContratoId",
                table: "ContratoInquilino",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(uint),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<uint>(
                name: "ProprietarioId",
                table: "CasaProprietarios",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(uint),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<uint>(
                name: "CasaId",
                table: "CasaProprietarios",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(uint),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_CasaProprietarios_Casas_CasaId",
                table: "CasaProprietarios",
                column: "CasaId",
                principalTable: "Casas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CasaProprietarios_Proprietarios_ProprietarioId",
                table: "CasaProprietarios",
                column: "ProprietarioId",
                principalTable: "Proprietarios",
                principalColumn: "Id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CasaProprietarios_Casas_CasaId",
                table: "CasaProprietarios");

            migrationBuilder.DropForeignKey(
                name: "FK_CasaProprietarios_Proprietarios_ProprietarioId",
                table: "CasaProprietarios");

            migrationBuilder.DropForeignKey(
                name: "FK_ContratoInquilino_Contratos_ContratoId",
                table: "ContratoInquilino");

            migrationBuilder.DropForeignKey(
                name: "FK_ContratoInquilino_Inquilinos_InquilinoId",
                table: "ContratoInquilino");

            migrationBuilder.AlterColumn<uint>(
                name: "InquilinoId",
                table: "ContratoInquilino",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0u,
                oldClrType: typeof(uint),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<uint>(
                name: "ContratoId",
                table: "ContratoInquilino",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0u,
                oldClrType: typeof(uint),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<uint>(
                name: "ProprietarioId",
                table: "CasaProprietarios",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0u,
                oldClrType: typeof(uint),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<uint>(
                name: "CasaId",
                table: "CasaProprietarios",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0u,
                oldClrType: typeof(uint),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CasaProprietarios_Casas_CasaId",
                table: "CasaProprietarios",
                column: "CasaId",
                principalTable: "Casas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CasaProprietarios_Proprietarios_ProprietarioId",
                table: "CasaProprietarios",
                column: "ProprietarioId",
                principalTable: "Proprietarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContratoInquilino_Contratos_ContratoId",
                table: "ContratoInquilino",
                column: "ContratoId",
                principalTable: "Contratos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContratoInquilino_Inquilinos_InquilinoId",
                table: "ContratoInquilino",
                column: "InquilinoId",
                principalTable: "Inquilinos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
