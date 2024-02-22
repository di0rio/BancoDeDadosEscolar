namespace SGE.Models
{
    public class Usuario
    {
        public Guid UsuarioId { get; set; }
        public string UsuarioNome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Celular { get; set; }
        public bool CadAtivo { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? CadInativo { get; set; }
        public Guid TipoUsuarioId { get; set; }
        public TipoUsuario? TipoUsuario { get; set; }
    }
}
