using EducaTu.Data;
using EducaTu.Models;
using Microsoft.EntityFrameworkCore;

namespace EducaTu.Repository
{
    public interface IPlanoRepository
    {
        Task<List<PlanoModel>> GetAllAsync();
        Task<PlanoModel> GetByIdAsync(int? id);
        Task<UsuarioPlano> GetByUserAsync(int idUsuario);
        Task<UsuarioPlano> AddUsuarioPlanoAsync(UsuarioPlano plano);
    }
    public class PlanoRepository : IPlanoRepository
    {
        private readonly BancoContext _context;

        public PlanoRepository(BancoContext context)
        {
            _context = context;
        }

        public async Task<List<PlanoModel>> GetAllAsync()
        {
            return await _context.Planos.ToListAsync();
        }

        public async Task<PlanoModel> GetByIdAsync(int? id)
        {
            return await _context.Planos.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<UsuarioPlano?> GetByUserAsync(int idUsuario)
        {
            UsuarioPlano? usuarioPlano = await _context.UsuarioPlano.FirstOrDefaultAsync(x => x.Id == idUsuario);
            return usuarioPlano;
        }

        public async Task<UsuarioPlano> AddUsuarioPlanoAsync(UsuarioPlano plano)
        {
            await _context.UsuarioPlano.AddAsync(plano);
            _context.SaveChanges();
            return plano;
        }
    }
}
