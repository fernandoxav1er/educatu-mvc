using EducaTu.Data;
using EducaTu.Models;

namespace EducaTu.Repository
{
    public interface IUsuarioRepository
    {
        UsuarioModel BuscaPorLogin(string login);
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
    }
}
