using EducaTu.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [NotMapped]
        [Required(ErrorMessage = "Confirme a nova senha do usuario")]
        [Compare("Senha", ErrorMessage = "Senha não confere com a nova senha")]
        public string? ConfirmarNovaSenha { get; set; }

        [Required(ErrorMessage = "Escolha um perfil")]
        public PerfilEnums? Perfil { get; set; }

        [Required(ErrorMessage = "Digite um número para contato")]
        public string? Celular { get; set; }

        [Required(ErrorMessage = "Digite o CEP")]
        public string? Cep { get; set; }

        [Required(ErrorMessage = "Digite o seu endereço")]
        public string? Endereco { get; set; }

        [Required(ErrorMessage = "Escolha o gênero sexual")]
        public string? Sexo { get; set; }

        [Required(ErrorMessage = "Digite sua data de nascimento")]
        public DateTime DataNascimento { get; set; }

        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public int? IdPlano{ get; set; }

        public bool SenhaValida(string senha)
        {
            return Senha == senha;
        }
    }
}
