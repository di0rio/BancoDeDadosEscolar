using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace SGE.Models
{
    public class Aluno
    {
        public Guid AlunoId { get; set; }

        [Required(ErrorMessage = "O campo Matrícula é obrigatório")]
        [Display(Name = "Matrícula")]
        public string Matricula { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        [MinLength(3, ErrorMessage = "O campo Nome deve ter no " +
                       "mínimo 3 caracteres")]
        [StringLength(100, ErrorMessage = "O campo Nome deve ter no " +
            "máximo 100 caracteres")]
        [Display(Name = "Nome do Aluno(a)")]
        public string AlunoNome { get; set; }

        [Required(ErrorMessage = "O campo Email é obrigatório")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Celular é obrigatório")]
        public string Celular { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string CEP { get; set; }
        public string Senha { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento")]
        [Required(ErrorMessage = "O campo Data de Nascimento é obrigatório")]
        public DateTime DataNascimento { get; set; }

        public bool CadAtivo { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? CadInativo { get; set; }
        public Guid? TipoUsuarioId { get; set; }
        public TipoUsuario? TipoUsuario { get; set; }
        public string? UrlFoto { get; set; }
        public ICollection<AlunoTurma>? AlunoTurmas { get; set; }
    }
}
