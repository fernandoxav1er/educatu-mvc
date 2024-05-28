using EducaTu.Helper;
using EducaTu.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EducaTu.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ICursoRepository _cursoRepository;
        private readonly ISessao _sessao;

        public UsuarioController(ICursoRepository cursoRepository,
            ISessao sessao)
        {
            _cursoRepository = cursoRepository;
            _sessao = sessao;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        public async Task<IActionResult> Visualizacao(int cursoId)
        {
            var curso = await _cursoRepository.GetById(cursoId);
            ViewBag.Curso = curso;

            return View();
        }  
        
        public async Task<IActionResult> MeusCursos()
        {
            var usuario = _sessao.GetSessaoUsuario();
            if (usuario != null)
            {
                var lstCursos = await _cursoRepository.ObterCursosPorUsuario(usuario.Id);
                ViewBag.Cursos = lstCursos;
            }
            return View();
        }

    }
}
