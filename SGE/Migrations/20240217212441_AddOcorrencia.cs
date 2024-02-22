using System;
using Microsoft.EntityFrameworkCore.Migrations;


namespace SGE.Migrations
{
    /// <inheritdoc />
    public partial class AddOcorrencia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipoOcorrencia",
                columns: table => new
                {
                    TipoOcorrenciaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoOcorrenciaNome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CadAtivo = table.Column<bool>(type: "bit", nullable: false),
                    CadInativo = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoOcorrencia", x => x.TipoOcorrenciaId);
                });

            migrationBuilder.CreateTable(
                name: "Ocorrencia",
                columns: table => new
                {
                    OcorrenciaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoOcorrenciaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataOcorrencia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CadAtivo = table.Column<bool>(type: "bit", nullable: false),
                    CadInativo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Finalizado = table.Column<bool>(type: "bit", nullable: false),
                    DataFinalizado = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ocorrencia", x => x.OcorrenciaId);
                    table.ForeignKey(
                        name: "FK_Ocorrencia_TipoOcorrencia_TipoOcorrenciaId",
                        column: x => x.TipoOcorrenciaId,
                        principalTable: "TipoOcorrencia",
                        principalColumn: "TipoOcorrenciaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ocorrencia_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ocorrencia_TipoOcorrenciaId",
                table: "Ocorrencia",
                column: "TipoOcorrenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ocorrencia_UsuarioId",
                table: "Ocorrencia",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ocorrencia");

            migrationBuilder.DropTable(
                name: "TipoOcorrencia");
        }
    }
}
