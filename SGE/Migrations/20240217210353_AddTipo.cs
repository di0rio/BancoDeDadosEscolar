using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SGE.Migrations
{
    /// <inheritdoc />
    public partial class AddTipo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CadAtivo",
                table: "Aluno",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CadInativo",
                table: "Aluno",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCadastro",
                table: "Aluno",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataNascimento",
                table: "Aluno",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Senha",
                table: "Aluno",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "TipoUsuarioId",
                table: "Aluno",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "UrlFoto",
                table: "Aluno",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "TipoUsuario",
                columns: table => new
                {
                    TipoUsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoUsuario", x => x.TipoUsuarioId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aluno_TipoUsuarioId",
                table: "Aluno",
                column: "TipoUsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Aluno_TipoUsuario_TipoUsuarioId",
                table: "Aluno",
                column: "TipoUsuarioId",
                principalTable: "TipoUsuario",
                principalColumn: "TipoUsuarioId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aluno_TipoUsuario_TipoUsuarioId",
                table: "Aluno");

            migrationBuilder.DropTable(
                name: "TipoUsuario");

            migrationBuilder.DropIndex(
                name: "IX_Aluno_TipoUsuarioId",
                table: "Aluno");

            migrationBuilder.DropColumn(
                name: "CadAtivo",
                table: "Aluno");

            migrationBuilder.DropColumn(
                name: "CadInativo",
                table: "Aluno");

            migrationBuilder.DropColumn(
                name: "DataCadastro",
                table: "Aluno");

            migrationBuilder.DropColumn(
                name: "DataNascimento",
                table: "Aluno");

            migrationBuilder.DropColumn(
                name: "Senha",
                table: "Aluno");

            migrationBuilder.DropColumn(
                name: "TipoUsuarioId",
                table: "Aluno");

            migrationBuilder.DropColumn(
                name: "UrlFoto",
                table: "Aluno");
        }
    }
}
