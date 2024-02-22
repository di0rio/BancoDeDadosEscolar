namespace SGE.Models
{
    public class AlunoTurma
    {
        public Guid AlunoTurmaId { get; set; }
        public Guid AlunoId { get; set; }
        public Aluno? Aluno { get; set; }
        public Guid TurmaId { get; set; }
        public Turma? Turma { get; set; }

    }
}
