using System.Diagnostics;
using ChessWeb.Application.Constants;
using ChessWeb.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChessWeb.Application.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger) => 
            _logger = logger;

        public IActionResult Index(int statusCode)
        {
            ViewBag.Error = statusCode;
            _logger.LogError($"Error. Status code {statusCode}"); 
            return View();
        }

        [Route(Routes.ErrorRoute)]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}