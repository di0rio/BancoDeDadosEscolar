namespace SGE.Models
{
    public class TipoUsuario
    {
        public Guid TipoUsuarioId { get; set; }
        public string Tipo { get; set; }
        public IEnumerable<Aluno>? Alunos { get; set; }
    }
}
