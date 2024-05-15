using EducaTu.Helper;
using EducaTu.Models;
using EducaTu.Repository;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EducaTu.Controllers
{
    public class PlanoController : Controller
    {
        private readonly IPlanoRepository _planoRepository;
        private readonly ISessao _sessao;

        public PlanoController(IPlanoRepository planoRepository,
                                ISessao sessao)
        {
            _planoRepository = planoRepository;
            _sessao = sessao;
        }
        public async Task<IActionResult> Index()
        {
            UsuarioModel usuarioLogado = _sessao.GetSessaoUsuario();
            var planoUser = await _planoRepository.GetByIdAsync(usuarioLogado.IdPlano);

            ViewBag.User = usuarioLogado;
            ViewBag.PlanoByUser = planoUser;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SalvarPagamento(UsuarioPlano usuarioPlanoModel)
        {
            try
            {
                await _planoRepository.AddUsuarioPlanoAsync(usuarioPlanoModel);
                _sessao.RemoverSessaoUsuario();

                TempData["MensagemSucesso"] = $"Cadastro com sucesso, faça o login!";

                return RedirectToAction("Index", "Login");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, {erro.Message}";
                return RedirectToAction("Index", "Login");
            }

        }
    }
}
