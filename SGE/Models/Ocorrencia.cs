namespace SGE.Models
{
    public class Ocorrencia
    {
        public Guid OcorrenciaId { get; set; }
        public Guid TipoOcorrenciaId { get; set; }
        public TipoOcorrencia? TipoOcorrencia { get; set; }
        public Guid UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
        public Guid AlunoId { get; set; }
        public Aluno? Aluno { get; set; }
        public DateTime DataOcorrencia { get; set; }
        public string Descricao { get; set; }
        public bool CadAtivo { get; set; }
        public DateTime? CadInativo { get; set; }
        public bool Finalizado { get; set; }
        public DateTime? DataFinalizado { get; set; }
        public string? Tratativa { get; set; }
    }
}
