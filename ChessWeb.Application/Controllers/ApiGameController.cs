using System.Threading.Tasks;
using ChessWeb.Domain.Entities;
using ChessWeb.Persistence.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace ChessWeb.Application.Controllers
{
    [Route("api/games")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GameController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetTodoItem(long id)
        {
            var game = await _context.Games.FindAsync(id);

            return game ?? (ActionResult<Game>) NotFound();
        }
    }
}