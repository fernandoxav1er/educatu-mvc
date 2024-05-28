using EducaTu.Data;
using EducaTu.Models;
using Microsoft.EntityFrameworkCore;

namespace EducaTu.Repository
{
    public interface ICursoRepository
    {
        Task<List<Curso>> GetAll();
        Task<Curso> GetById(int id);
        Task<List<Curso>> ObterCursosPorUsuario(int idUsuario);
        Task<UsuarioCursos> AdicionarUsuarioCursos(UsuarioCursos usuarioCursos);
    }
    public class CursoRepository : ICursoRepository
    {
        private readonly BancoContext _context;

        public CursoRepository(BancoContext context)
        {
            _context = context;
        }
        public async Task<List<Curso>> GetAll()
        {
            return await _context.Cursos.ToListAsync();
        }
        public async Task<Curso> GetById(int id)
        {
            return await _context.Cursos.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<List<Curso>> ObterCursosPorUsuario(int idUsuario)
        {
            var cursosUsarios = await _context.UsuarioCursos.Where(x => x.IdUsuario == idUsuario).Select(c => c.IdCurso).ToListAsync();
            return await _context.Cursos.Where(c => cursosUsarios.Contains(c.Id)).ToListAsync();

        }

        public async Task<UsuarioCursos> AdicionarUsuarioCursos(UsuarioCursos usuarioCursos)
        {
            await _context.UsuarioCursos.AddAsync(usuarioCursos);
            _context.SaveChanges();

            return usuarioCursos;
        }
    }
}
