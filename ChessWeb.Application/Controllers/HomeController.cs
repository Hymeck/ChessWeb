using Microsoft.AspNetCore.Mvc;

namespace ChessWeb.Application.Controllers
{
    public class HomeController : Controller
    {
        public HomeController() {}

        public IActionResult Index() => 
            View();
    }
}