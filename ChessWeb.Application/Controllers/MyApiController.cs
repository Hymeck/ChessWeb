using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessWeb.Domain.Interfaces.UnitsOfWork;
using ChessWeb.Persistence.Contexts;
using ChessWeb.Persistence.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChessWeb.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MyApiController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        private readonly ILogger<MyApiController> _logger;

        public MyApiController(IUnitOfWork unitOfWork, ILogger<MyApiController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
    }
}