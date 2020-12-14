using System;
using System.Threading.Tasks;
using ChessWeb.SignalR.Client;
using ChessWeb.SignalR.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;

namespace ChessWeb.Application.Controllers
{
    
    public class SandboxController : Controller
    {
        private readonly IHubContext<SandboxHub> _hubContext;
    
        public SandboxController(IHubContext<SandboxHub> hubContext)
        {
            _hubContext = hubContext;
        }
        
        public ActionResult Index()
        {
            return View();
        }
    }
}