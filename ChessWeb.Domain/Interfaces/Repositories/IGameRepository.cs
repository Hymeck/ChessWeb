using System.Collections.Generic;
using System.Threading.Tasks;
using ChessWeb.Domain.Entities;

namespace ChessWeb.Domain.Interfaces.Repositories
{
    public interface IGameRepository : IGenericRepository<Game>
    {
        Task<IEnumerable<Game>> GetAllAsync();
        Task<IEnumerable<Game>> GetUserGamesAsync(User user);
        Task CreateAsync();
        Task<Game> GetAsync(long id);
        Task DeleteAsync(long id);
        bool Any();
    }
}