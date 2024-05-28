using EducaTu.Helper;
using EducaTu.Models;
using EducaTu.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EducaTu.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICursoRepository _cursoRepository;
        private readonly ISessao _sessao;

        public HomeController(ICursoRepository cursoRepository, ISessao sessao)
        {
            _cursoRepository = cursoRepository;
            _sessao = sessao;
        }

        public async Task<IActionResult> Index()
        {
            var lstCursos = await _cursoRepository.GetAll();
            ViewBag.Cursos = lstCursos;

            return View();
        }

        public async Task<IActionResult> GetCursoDetails(int id)
        {
            Curso curso = await _cursoRepository.GetById(id);
            return PartialView("_CursoModal", curso);
        }

        public async Task<IActionResult> FavoritarCurso(int cursoId)
        {
            try
            {
                var usuario = _sessao.GetSessaoUsuario();

                List<Curso> cursosUsuario = await _cursoRepository.ObterCursosPorUsuario(usuario.Id);
                var existecurso = cursosUsuario.Where(x => x.Id == cursoId).FirstOrDefault();

                if (existecurso == null)
                {
                    UsuarioCursos obj = new()
                    {
                        IdCurso = cursoId,
                        IdUsuario = usuario.Id
                    };

                    await _cursoRepository.AdicionarUsuarioCursos(obj);

                    TempData["MensagemSucesso"] = $"Favoritado com sucesso, entre na aba 'Meus Cursos'";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["MensagemErro"] = $"Você já favoritou esse curso, acesse em 'Meus Cursos'";
                    return RedirectToAction("Index");
                }

            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, {erro.Message}";
                return RedirectToAction("Index");
            }
        }
 
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
