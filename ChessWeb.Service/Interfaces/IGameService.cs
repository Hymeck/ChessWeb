using System.Collections;
using System.Collections.Generic;
using ChessWeb.Domain.Entities;

namespace ChessWeb.Service.Interfaces
{
    public interface IGameService
    {
        IEnumerable<Game> GetAll();
        void CreateGame();
        void Join(User user, long sideId);
        bool Any();
        IEnumerable<Game> GetUserGames(User user);
    }
}