using EducaTu.Data;
using EducaTu.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EducaTu.Repository
{
    public interface IUsuarioRepository
    {
        UsuarioModel? BuscaPorLogin(string login);
        bool ExisteUsuarioPorLogin(string login);
        bool ExisteUsuarioPorId(int id);
        Task<UsuarioModel> Adicionar(UsuarioModel usuarioModel);
    }

    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly BancoContext _context;
        public UsuarioRepository(BancoContext bancoContext)
        {
            _context = bancoContext;
        }

        public UsuarioModel? BuscaPorLogin(string login)
        {
            UsuarioModel? usuario = _context.Usuario.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper());

            if (usuario == null) throw new System.Exception("Não encontramos o usuário informado!");

            return usuario;
        }

        public async Task<UsuarioModel> Adicionar(UsuarioModel usuarioModel)
        {
            var busca = ExisteUsuarioPorLogin(usuarioModel.Login);
            if (busca) throw new System.Exception("Nome de login não disponível, tente outro.");

            usuarioModel.DataCadastro = DateTime.Now;
            usuarioModel.DataAtualizacao = DateTime.Now;

            await _context.Usuario.AddAsync(usuarioModel);
            _context.SaveChanges();

            return usuarioModel;

        }

        public bool ExisteUsuarioPorId(int id)
        {
            var usuario =  _context.Usuario.FirstOrDefaultAsync(x => x.Id == id);
            
            if (usuario == null) return false;

            return true;
        }
        public bool ExisteUsuarioPorLogin(string login)
        {
            UsuarioModel? usuario =  _context.Usuario.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper());

            if (usuario == null) return false;

            return true;
        }
    }
}
