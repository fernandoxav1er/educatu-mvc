using EducaTu.Models;
using EducaTu.Repository;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EducaTu.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPlanoRepository _planoRepository;

        public LoginController(IUsuarioRepository usuarioRepository,
                                IPlanoRepository planoRepository)
        {
            _usuarioRepository = usuarioRepository;
            _planoRepository = planoRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Entrar(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel? usuario = _usuarioRepository.BuscaPorLogin(loginModel.Login);
                    if (usuario != null)
                    {
                        if (usuario.SenhaValida(loginModel.Senha))
                        {
                            var usuarioPlano = await _planoRepository.GetByUserAsync(usuario.Id);
                            if (usuario.Perfil == Enums.PerfilEnums.Aluno && usuarioPlano == null)
                            {
                                return RedirectToAction("Index", "Plano");
                            }

                            return RedirectToAction("Index", "Home");
                        }
                    }
                    TempData["MensagemErro"] = $"Usuário e/ou senha inválido(s). Por favor, tente novamente.";
                }
                return View("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CadastroAsync(UsuarioModel usuarioModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _usuarioRepository.Adicionar(usuarioModel);
                    return RedirectToAction("Index", "Login");
                }
                return View();
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, {erro.Message}";
                return RedirectToAction("Index");
            }
        }

    }
}
