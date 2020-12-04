using System.Collections.Generic;
using ChessWeb.Domain.Entities;

namespace ChessWeb.Application.ViewModels.Game
{
    public class GameViewModel
    {
        public long Id { get; set; }
        public string Fen { get; set; }
        public string Status { get; set; }
        public IEnumerable<Side> Sides { get; set; }

        public GameViewModel(Domain.Entities.Game game, IEnumerable<Side> sides)
        {
            Id = game.Id;
            Fen = game.Fen;
            Status = FromGameStatus(game?.GameSummary.Status);
            Sides = sides;
        }
        
        private static string FromGameStatus(GameStatus gameStatus) =>
            gameStatus.Status switch
            {
                0 => "Ожидание",
                1 => "Игра",
                2 => "Ничья",
                3 => "Победа белого игрока",
                4 => "Победа черного игрока",
                _ => "Не определено"
            };
    }
}