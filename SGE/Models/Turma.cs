namespace SGE.Models
{
    public class Turma
    {
        public Guid TurmaId { get; set; }
        public string TurmaNome { get; set; }
        public string Turno { get; set; }
        public string Serie { get; set; }
        public bool CadAtivo { get; set; }
        public DateTime? CadInativo { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public bool TurmaEncerrada { get; set; }
        public ICollection<AlunoTurma>? AlunoTurmas { get; set; }
    }
}
