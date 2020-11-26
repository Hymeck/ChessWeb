using System;
using System.Collections.Generic;
using System.Linq;
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
            // MainFunction();
            PlayingWithMove();
        }

        private static void PlayingWithMove()
        {
            using var applicationContext = new ApplicationContext();
            // IRepository<Game> gameRepository = new Repository<Game>(applicationContext);
            // var game1 = gameRepository.Get(1);
            // IRepository<Player> playerRepository = new Repository<Player>(applicationContext);
            // IRepository<ChessGameInfo> infoRepository = new Repository<ChessGameInfo>(applicationContext);
            
            // IRepository<Side> sideRepository = new Repository<Side>(applicationContext);
            // var side1 = sideRepository.Get(1);
            // var game1 = side1.Game;
            
            IRepository<Move> moveRepository = new MoveRepository(applicationContext);
            // var firstMove = new Move {Game = game1, Player = side1.Player, Fen = game1.Fen, MoveNext = "d2d4"};
            // moveRepository.Insert(firstMove);
            // moveRepository.Delete(moveRepository.Get(2));
            ShowEntities(moveRepository);
            
            // WriteLine(GetEntityString(side1));
        }
        private static void MainFunction()
        {
            using var applicationContext = new ApplicationContext();
            IRepository<Player> playerRepository = new Repository<Player>(applicationContext);
            ShowEntities(playerRepository);
            
            IRepository<Color> colorRepository = new Repository<Color>(applicationContext);
            ShowEntities(colorRepository);
            
            IRepository<ChessGameInfo> infoRepository = new Repository<ChessGameInfo>(applicationContext);
            ShowEntities(infoRepository);
            
            IRepository<Game> gameRepository = new Repository<Game>(applicationContext);

            #region AddInfoToFirstGameEntry
            // uncomment if you need to insert info to first default game entry
            // var chessGameInfo = infoRepository.Get(1);
            // var game1 = gameRepository.Get(1);
            // game1.ChessGameInfo = chessGameInfo;
            // gameRepository.Update(game1);
            #endregion AddInfoToFirstGameEntry
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
        }
        
        private static void ShowEntities<T>(IRepository<T> repository) where T : BaseEntity
        {
            foreach (var entity in repository.GetAll())
                WriteLine(GetEntityString(entity));
        }

        private static string GetEntityString(BaseEntity entity) =>
            entity switch
            {
                Game g => $"Game. {g.Id}. FEN: {g.Fen}. Additional info: {g.ChessGameInfo}",
                Side s => $"Side. {s.Id}. GameId: {s.Game.Id}. PlayerNick: {s.Player.Nickname}. Color: {s.Color}",
                Move m => $"Move. {m.Id}. GameId: {m.Game?.Id}. PlayerNick: {m.Player?.Nickname}. FEN before move: {m.Fen}. Move: {m.MoveNext}",
                Color c => $"Color. {c.Id}. Color: {c}",
                Player p => $"Player. {p.Id}. {p.Nickname}, {p.Email}, {p.Password}",
                ChessGameInfo i => $"ChessGameInfo. {i.Id}, {i}",
                _ => $"BaseEntity or it's unknown inheritor. {entity.Id}."
            };
    }
}