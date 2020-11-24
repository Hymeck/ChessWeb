using System;
using System.Collections.Generic;
using ChessWeb.Domain.Entities;
using ChessWeb.Persistence.Contexts;
using ChessWeb.Persistence.Implementations;
using ChessWeb.Persistence.Interfaces;
using static System.Console;

namespace ChessWeb.Playground
{
    class Playground
    {
        static void Main(string[] args)
        {
            using var applicationContext = new ApplicationContext();
            IRepository<Player> playerRepository = new Repository<Player>(applicationContext);
            ShowEntities(playerRepository);
            
            IRepository<Color> colorRepository = new Repository<Color>(applicationContext);
            ShowEntities(colorRepository);
            
            IRepository<Game> gameRepository = new Repository<Game>(applicationContext);
            ShowEntities(gameRepository);
            
            IRepository<Side> sideRepository = new Repository<Side>(applicationContext);
            
            #region InsertToSideTable
            // uncomment if you need to insert data to 'Side' table
            // var whiteColor = colorRepository.Get(1);
            // var blackColor = colorRepository.Get(2);
            // var game = gameRepository.Get(1);
            // var player1 = playerRepository.Get(1);
            // var player2 = playerRepository.Get(2);
            // var whiteSide = new Side {Color = whiteColor, Game = game, Player = player1};
            // var blackSide = new Side {Color = blackColor, Game = game, Player = player2};
            // sideRepository.Insert(whiteSide);
            // sideRepository.Insert(blackSide);
            #endregion InsertToSideTable
            ShowEntities(sideRepository);
            
            // PrintInfo(game.ChessGameInfo);
        }

        private static void ShowEntities<T>(IRepository<T> repository) where T : BaseEntity
        {
            foreach (var entity in repository.GetAll())
                WriteLine(GetEntityString(entity));
        }

        // private static void PrintInfo(ChessGameInfo info)
        // {
        //     var printString = info == null
        //         ? "Null"
        //         : $"w s: {info.WhiteKingSquare}; b s: {info.BlackKingSquare}";
        //     WriteLine(printString);
        // }

        private static string GetEntityString(BaseEntity entity) =>
            entity switch
            {
                Game g => $"Game. {g.Id}. FEN: {g.Fen}",
                Side s => $"Side. {s.Id}. GameId: {s.Game.Id}. PlayerNick: {s.Player.Nickname}. Color: {s.Color}",
                Move m => $"Move. {m.Id}. GameId: {m.Game.Id}. PlayerNick: {m.Player.Nickname}. FEN before move: {m.Fen}. Move: {m.MoveNext}",
                Color c => $"Color. {c.Id}. Color: {c}",
                Player p => $"Player. {p.Id}. {p.Nickname}, {p.Email}, {p.Password}",
                _ => $"BaseEntity or it's unmentioned inheritor. {entity.Id}."
            };
    }
}