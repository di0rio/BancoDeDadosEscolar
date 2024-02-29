using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SGE.Migrations
{
    /// <inheritdoc />
    public partial class inicio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sala",
                columns: table => new
                {
                    SalaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SalaNome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CadAtivo = table.Column<bool>(type: "bit", nullable: false),
                    CadInativo = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sala", x => x.SalaId);
                });

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

            migrationBuilder.CreateTable(
                name: "Turma",
                columns: table => new
                {
                    TurmaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TurmaNome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Turno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Serie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CadAtivo = table.Column<bool>(type: "bit", nullable: false),
                    CadInativo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFim = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TurmaEncerrada = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turma", x => x.TurmaId);
                });

            migrationBuilder.CreateTable(
                name: "Aluno",
                columns: table => new
                {
                    AlunoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Matricula = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlunoNome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Celular = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Logradouro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CEP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CadAtivo = table.Column<bool>(type: "bit", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CadInativo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TipoUsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UrlFoto = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aluno", x => x.AlunoId);
                    table.ForeignKey(
                        name: "FK_Aluno_TipoUsuario_TipoUsuarioId",
                        column: x => x.TipoUsuarioId,
                        principalTable: "TipoUsuario",
                        principalColumn: "TipoUsuarioId");
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioNome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Celular = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CadAtivo = table.Column<bool>(type: "bit", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CadInativo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TipoUsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.UsuarioId);
                    table.ForeignKey(
                        name: "FK_Usuario_TipoUsuario_TipoUsuarioId",
                        column: x => x.TipoUsuarioId,
                        principalTable: "TipoUsuario",
                        principalColumn: "TipoUsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlunoTurma",
                columns: table => new
                {
                    AlunoTurmaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlunoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TurmaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunoTurma", x => x.AlunoTurmaId);
                    table.ForeignKey(
                        name: "FK_AlunoTurma_Aluno_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Aluno",
                        principalColumn: "AlunoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlunoTurma_Turma_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turma",
                        principalColumn: "TurmaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ocorrencia",
                columns: table => new
                {
                    OcorrenciaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoOcorrenciaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlunoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataOcorrencia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CadAtivo = table.Column<bool>(type: "bit", nullable: false),
                    CadInativo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Finalizado = table.Column<bool>(type: "bit", nullable: false),
                    DataFinalizado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Tratativa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ocorrencia", x => x.OcorrenciaId);
                    table.ForeignKey(
                        name: "FK_Ocorrencia_Aluno_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Aluno",
                        principalColumn: "AlunoId",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "ReservaSala",
                columns: table => new
                {
                    ReservaSalaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SalaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataReserva = table.Column<DateOnly>(type: "date", nullable: false),
                    DataFimReserva = table.Column<DateOnly>(type: "date", nullable: false),
                    HoraInicio = table.Column<TimeOnly>(type: "time", nullable: false),
                    HoraFim = table.Column<TimeOnly>(type: "time", nullable: false),
                    CadAtivo = table.Column<bool>(type: "bit", nullable: false),
                    CadInativo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CorReserva = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservaSala", x => x.ReservaSalaId);
                    table.ForeignKey(
                        name: "FK_ReservaSala_Sala_SalaId",
                        column: x => x.SalaId,
                        principalTable: "Sala",
                        principalColumn: "SalaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservaSala_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aluno_TipoUsuarioId",
                table: "Aluno",
                column: "TipoUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_AlunoTurma_AlunoId",
                table: "AlunoTurma",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_AlunoTurma_TurmaId",
                table: "AlunoTurma",
                column: "TurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ocorrencia_AlunoId",
                table: "Ocorrencia",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_Ocorrencia_TipoOcorrenciaId",
                table: "Ocorrencia",
                column: "TipoOcorrenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ocorrencia_UsuarioId",
                table: "Ocorrencia",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservaSala_SalaId",
                table: "ReservaSala",
                column: "SalaId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservaSala_UsuarioId",
                table: "ReservaSala",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_TipoUsuarioId",
                table: "Usuario",
                column: "TipoUsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlunoTurma");

            migrationBuilder.DropTable(
                name: "Ocorrencia");

            migrationBuilder.DropTable(
                name: "ReservaSala");

            migrationBuilder.DropTable(
                name: "Turma");

            migrationBuilder.DropTable(
                name: "Aluno");

            migrationBuilder.DropTable(
                name: "TipoOcorrencia");

            migrationBuilder.DropTable(
                name: "Sala");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "TipoUsuario");
        }
    }
}
