using System.Collections.Generic;
using ChessWeb.Domain.Entities;

namespace ChessWeb.Application.ViewModels.Game
{
    public class GameViewModel
    {
        public long Id { get; set; }
        public string Fen { get; set; }
        public IEnumerable<Side> Sides { get; set; }

        public GameViewModel(Domain.Entities.Game game, IEnumerable<Side> sides)
        {
            Id = game.Id;
            Fen = game.Fen;
            Sides = sides;
        }
    }
}