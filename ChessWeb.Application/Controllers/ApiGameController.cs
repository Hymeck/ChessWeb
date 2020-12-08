using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessWeb.Application.DTO;
using ChessWeb.Domain.Entities;
using ChessWeb.Persistence.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChessWeb.Application.Controllers
{
    [Route("api/games")]
    [ApiController]
    public class ApiGameController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiGameController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        // GET: api/games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameDto>>> GetGames() =>
            await _context.Games
                .Select(x => new GameDto(x))
                .ToListAsync();

        // GET: api/games/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetGame(long id)
        {
            var game = await _context.Games.FindAsync(id);

            return game ?? (ActionResult<Game>) NotFound();
        }
    }
}