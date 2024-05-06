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

        public async Task<IActionResult> Cadastro()
        {
            var planos = await _planoRepository.GetAllAsync();
            ViewBag.Planos = planos;
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

                            if (usuario.Perfil == Enums.PerfilEnums.Aluno && usuario.IdPlano == null)
                            {
                                TempData["MensagemErro"] = $"Ops, erro no seu login, por favor entrar em contato com um administrador";
                                return View("Index");
                            }

                            if (usuario.Perfil == Enums.PerfilEnums.Aluno && usuarioPlano == null)
                            {
                                return RedirectToAction("Index", "Plano", usuario);
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
                    TempData["MensagemSucesso"] = $"Cadastro com sucesso, faça o login na aplicação @{usuarioModel.Login}.";
                    return RedirectToAction("Index", "Login");
                }

                var planos = await _planoRepository.GetAllAsync();
                ViewBag.Planos = planos;

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
