using EducaTu.Enums;
using System.ComponentModel.DataAnnotations;

namespace EducaTu.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Digite o nome do usuário")]
        public string? NomeCompleto { get; set; }
        
        [Required(ErrorMessage = "Digite o e-mail do usuário")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é valido!")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Digite o login do usuário")]
        public string? Login { get; set; }

        [Required(ErrorMessage = "Digite a senha do usuário")]
        public string? Senha { get; set; }

        [Required(ErrorMessage = "Informe o perfil do usuário")]
        public PerfilEnums? Perfil { get; set; }

        public string? Celular { get; set; }
        public string? Cep { get; set; }
        public string? Endereco { get; set; }
        public string? Sexo { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }

        public bool SenhaValida(string senha)
        {
            return Senha == senha;
        }
    }
}
