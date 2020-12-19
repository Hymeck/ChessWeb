using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessWeb.Application.DTO;
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
        private readonly IGameService _gameService;

        public ApiGameController(ApplicationDbContext context, IChessGameService chessGameService, IGameService gameService)
        {
            _context = context;
            _chessGameService = chessGameService;
            _gameService = gameService;
        }
        
        // GET: api/games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameDto>>> GetGames() =>
            await _context.Games
                .Select(x => GameDto.FromGame(x))
                .ToListAsync();
        
        // GET: api/games/active
        [HttpGet]
        [Route("active")]
        public async Task<ActionResult<IEnumerable<GameDto>>> GetActiveGames() =>
            await _context.Games
                .Where(x => x.Status == 1)
                .Select(x => GameDto.FromGame(x))
                .ToListAsync();
        
        // GET: api/games/id
        [HttpGet]
        [Route("active/{id}")]
        public async Task<ActionResult<GameDto>> GetActiveGame(long id)
        {
            var games = await GetActiveGames();
            var game = games.Value.FirstOrDefault(x => x.GameId == id);
            return game ?? (ActionResult<GameDto>) NotFound();
        }
        
        // GET: api/games/waiting
        [HttpGet]
        [Route("waiting")]
        public async Task<ActionResult<IEnumerable<GameDto>>> GetWaitingGames() =>
            await _context.Games
                .Where(x => x.Status == 0)
                .Select(x => GameDto.FromGame(x))
                .ToListAsync();

        // GET: api/games/1
        [HttpGet("{id}")]
        public async Task<ActionResult<GameDto>> GetGame(long id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null)
                return NotFound();
            return new GameDto(game);
        }
        
        // GET: api/games/1/userId/e2e4
        [HttpGet("{gameId}/{userId}/{move}")]
        public async Task<ActionResult<GameDto>> MakeMove(long gameId, string userId, string move)
        {
            var game = await _chessGameService.MakeMove(gameId, userId, move);
            var gameDto = GameDto.FromGame(game);
            return gameDto ?? (ActionResult<GameDto>) NotFound();
        }

        // GET api/games/create
        [HttpGet("create")]
        public async Task<ActionResult<GameDto>> CreateGame()
        {
            await _gameService.CreateGameAsync();
            return Ok();
        }
        
        // GET api/games/last
        [HttpGet("last")]
        public async Task<ActionResult<GameDto>> GetLastGame()
        {
            var game = (await _gameService.GetAllAsync())
                .OrderByDescending(x => x.Id)
                .First();
            return GameDto.FromGame(game);
        }
    }
}