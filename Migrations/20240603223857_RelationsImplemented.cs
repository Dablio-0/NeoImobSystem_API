using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeoImobSystem_API.Migrations
{
    /// <inheritdoc />
    public partial class RelationsImplemented : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Casas_ContratoId",
                table: "Casas");

            migrationBuilder.AddColumn<uint>(
                name: "CasaId",
                table: "Contratos",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CasaProprietarios",
                columns: table => new
                {
                    Id = table.Column<uint>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CasaId = table.Column<uint>(type: "INTEGER", nullable: false),
                    ProprietarioId = table.Column<uint>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CasaProprietarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CasaProprietarios_Casas_CasaId",
                        column: x => x.CasaId,
                        principalTable: "Casas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CasaProprietarios_Proprietarios_ProprietarioId",
                        column: x => x.ProprietarioId,
                        principalTable: "Proprietarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContratoInquilino",
                columns: table => new
                {
                    Id = table.Column<uint>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ContratoId = table.Column<uint>(type: "INTEGER", nullable: false),
                    InquilinoId = table.Column<uint>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContratoInquilino", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContratoInquilino_Contratos_ContratoId",
                        column: x => x.ContratoId,
                        principalTable: "Contratos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContratoInquilino_Inquilinos_InquilinoId",
                        column: x => x.InquilinoId,
                        principalTable: "Inquilinos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Casas_ContratoId",
                table: "Casas",
                column: "ContratoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CasaProprietarios_CasaId",
                table: "CasaProprietarios",
                column: "CasaId");

            migrationBuilder.CreateIndex(
                name: "IX_CasaProprietarios_ProprietarioId",
                table: "CasaProprietarios",
                column: "ProprietarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ContratoInquilino_ContratoId",
                table: "ContratoInquilino",
                column: "ContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_ContratoInquilino_InquilinoId",
                table: "ContratoInquilino",
                column: "InquilinoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CasaProprietarios");

            migrationBuilder.DropTable(
                name: "ContratoInquilino");

            migrationBuilder.DropIndex(
                name: "IX_Casas_ContratoId",
                table: "Casas");

            migrationBuilder.DropColumn(
                name: "CasaId",
                table: "Contratos");

            migrationBuilder.CreateIndex(
                name: "IX_Casas_ContratoId",
                table: "Casas",
                column: "ContratoId");
        }
    }
}
