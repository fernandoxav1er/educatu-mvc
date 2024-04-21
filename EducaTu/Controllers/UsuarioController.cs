using Microsoft.AspNetCore.Mvc;

namespace EducaTu.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
