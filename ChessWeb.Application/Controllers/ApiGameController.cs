using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessWeb.Application.DTO;
using ChessWeb.Domain.Entities;
using ChessWeb.Persistence.Contexts;
using ChessWeb.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChessWeb.Application.Controllers
{
    [Route("api/games")]
    [ApiController]
    public class ApiGameController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IChessGameService _chessGameService;

        public ApiGameController(ApplicationDbContext context, IChessGameService chessGameService)
        {
            _context = context;
            _chessGameService = chessGameService;
        }
        
        // GET: api/games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameDto>>> GetGames() =>
            await _context.Games
                .Select(x => GameDto.FromGame(x))
                .ToListAsync();

        // GET: api/games/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameDto>> GetGame(long id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null)
                return NotFound();
            return new GameDto(game);
        }
        
        // GET: api/games/5
        [HttpGet("{gameId}/{username}/{move}")]
        public async Task<ActionResult<GameDto>> GetGame(long gameId, string username, string move)
        {
            var game = await _chessGameService.MakeMove(gameId, username, move);
            var gameDto = GameDto.FromGame(game);
            return gameDto ?? (ActionResult<GameDto>) NotFound();
        }
        
        
    }

    public class Test
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Move { get; set; }

        public Test(long id, string username, string move)
        {
            Id = id;
            Username = username;
            Move = move;
        }
    }
}