using EducaTu.Data;
using EducaTu.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EducaTu.Repository
{
    public interface IUsuarioRepository
    {
        UsuarioModel? BuscaPorLogin(string login);
        Task<bool> ExisteLogin(int id);
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
            usuarioModel.DataCadastro = DateTime.Now;
            usuarioModel.DataAtualizacao = DateTime.Now;

            await _context.Usuario.AddAsync(usuarioModel);
            _context.SaveChanges();

            return usuarioModel;

        }

        public async Task<bool> ExisteLogin(int id)
        {
            var usuario = await _context.Usuario.FirstOrDefaultAsync(x => x.Id == id);
            
            if (usuario == null) return false;

            return true;
        }
    }
}
