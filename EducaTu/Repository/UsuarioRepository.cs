using EducaTu.Data;
using EducaTu.Models;
using System.Data;

namespace EducaTu.Repository
{
    public interface IUsuarioRepository
    {
        UsuarioModel BuscaPorLogin(string login);
        UsuarioModel Adicionar(UsuarioModel usuarioModel);
    }

    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly BancoContext _context;
        public UsuarioRepository(BancoContext bancoContext)
        {
            _context = bancoContext;
        }

        public UsuarioModel BuscaPorLogin(string login)
        {
            UsuarioModel usuario = _context.Usuario.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper());

            if (usuario == null) throw new System.Exception("Não encontramos o usuário informado!");
            return usuario;
        }

        public UsuarioModel Adicionar(UsuarioModel usuarioModel)
        {
            usuarioModel.DataCadastro = DateTime.Now;
            usuarioModel.DataAtualizacao = DateTime.Now;

            _context.Usuario.Add(usuarioModel);
            _context.SaveChanges();

            return usuarioModel;

        }
    }
}
