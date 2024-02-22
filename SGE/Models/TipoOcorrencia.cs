namespace SGE.Models
{
    public class TipoOcorrencia
    {
        public Guid TipoOcorrenciaId { get; set; }
        public string TipoOcorrenciaNome { get; set; }
        public bool CadAtivo { get; set; }
        public DateTime? CadInativo { get; set; }
    }
}
