using Microsoft.AspNetCore.Mvc;

namespace EstacionamentoWebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Contact()
        {
            return View(); // Vai buscar Views/Home/Contact.cshtml
        }
    }
}
