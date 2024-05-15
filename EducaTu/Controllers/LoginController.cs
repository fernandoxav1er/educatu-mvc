using EducaTu.Helper;
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
        private readonly ISessao _sessao;

        public LoginController(IUsuarioRepository usuarioRepository,
                                IPlanoRepository planoRepository,
                                ISessao sessao)
        {
            _usuarioRepository = usuarioRepository;
            _planoRepository = planoRepository;
            _sessao = sessao;
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

        public IActionResult Sair()
        {
            _sessao.RemoverSessaoUsuario();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Entrar(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _usuarioRepository.BuscaPorLogin(loginModel.Login);
                    if (usuario != null)
                    {
                        if (usuario.SenhaValida(loginModel.Senha))
                        {

                            if (usuario.Perfil == Enums.PerfilEnums.Aluno && usuario.IdPlano == null)
                            {
                                TempData["MensagemErro"] = $"Ops, erro no seu login, por favor entrar em contato com um administrador";
                                return View("Index");
                            }
                            
                            _sessao.CriarSessaoUsuario(usuario);

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
                    _sessao.CriarSessaoUsuario(usuarioModel);
                    return RedirectToAction("Index", "Plano");
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
