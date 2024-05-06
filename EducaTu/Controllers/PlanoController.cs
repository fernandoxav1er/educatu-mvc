using EducaTu.Models;
using EducaTu.Repository;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EducaTu.Controllers
{
    public class PlanoController : Controller
    {
        private readonly IPlanoRepository _planoRepository;

        public PlanoController(IPlanoRepository planoRepository)
        {
            _planoRepository = planoRepository;
        }
        public async Task<IActionResult> Index(UsuarioModel usuario)
        {
            var planoUser = await _planoRepository.GetByIdAsync(usuario.IdPlano);

            ViewBag.User = usuario;
            ViewBag.PlanoByUser = planoUser;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SalvarPagamento(UsuarioPlano usuarioPlanoModel)
        {
            try
            {
                await _planoRepository.AddUsuarioPlanoAsync(usuarioPlanoModel);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, {erro.Message}";
                return RedirectToAction("Index", "Login");
            }

            //return View("Index");
        }
    }
}
