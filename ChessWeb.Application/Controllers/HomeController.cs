using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ChessWeb.Application.ViewModels;
using ChessWeb.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace ChessWeb.Application.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMailSender _mailSender;
        public HomeController(ILogger<HomeController> logger, IMailSender mailSender)
        {
            _logger = logger;
            _mailSender = mailSender;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}