using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SGE.Migrations
{
    /// <inheritdoc />
    public partial class addAlunoOcorrencia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AlunoId",
                table: "Ocorrencia",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Ocorrencia_AlunoId",
                table: "Ocorrencia",
                column: "AlunoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ocorrencia_Aluno_AlunoId",
                table: "Ocorrencia",
                column: "AlunoId",
                principalTable: "Aluno",
                principalColumn: "AlunoId",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ocorrencia_Aluno_AlunoId",
                table: "Ocorrencia");

            migrationBuilder.DropIndex(
                name: "IX_Ocorrencia_AlunoId",
                table: "Ocorrencia");

            migrationBuilder.DropColumn(
                name: "AlunoId",
                table: "Ocorrencia");
        }
    }
}
