using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace SGE.Models
{
    public class Aluno
    {
        public Guid AlunoId { get; set; }
        public string Matricula { get; set; }
        public string AlunoNome { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string CEP { get; set; }
        public string Senha { get; set; }
        public DateTime DataNascimento { get; set; }
        public bool CadAtivo { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? CadInativo { get; set; }
        public Guid TipoUsuarioId { get; set; }
        public TipoUsuario? TipoUsuario { get; set; }
        public string? UrlFoto { get; set; }
        public ICollection<AlunoTurma>? AlunoTurmas { get; set; }
    }
}
