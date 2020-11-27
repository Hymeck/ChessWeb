using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessWeb.Domain.Entities;
using ChessWeb.Persistence.Implementations;
using ChessWeb.Persistence.Contexts;
using ChessWeb.Domain.Interfaces.UnitsOfWork;
using static System.Console;

namespace ChessWeb.Playground
{
    class Playground
    {
        private static IUnitOfWork _unitOfWork = new UnitOfWork(new ApplicationContext());
        static void Main(string[] args)
        {
            // PlayingWithSides();
            // PlayingWithMoves();
            PrintAllEntities();
        }

        private static void PlayingWithSides()
        {
            var game1 = _unitOfWork.Games.Get(1);
            var whitePlayer = _unitOfWork.Players.Get(1);
            var blackPlayer = _unitOfWork.Players.Get(2);
            
            var whiteColor = _unitOfWork.Colors.Get(1);
            var blackColor = _unitOfWork.Colors.Get(2);
            
            var whiteSide = new Side
            {
                Game = game1,
                Color = whiteColor,
                Player = whitePlayer
            };
            var blackSide = new Side
            {
                Game = game1,
                Color = blackColor,
                Player = blackPlayer
            };


            _unitOfWork.Sides.Add(whiteSide);
            _unitOfWork.Sides.Add(blackSide);
            _unitOfWork.Complete();
        }

        private static void PlayingWithMoves()
        {
            var game1 = _unitOfWork.Games.Get(1);
            var whitePlayer = _unitOfWork.Players.Get(1);

            var firstMove = new Move
            {
                Player = whitePlayer,
                Game = game1,
                Fen = game1.Fen,
                MoveNext = "e2e4"
            };
            
            _unitOfWork.Moves.Add(firstMove);
            _unitOfWork.Complete();
        }
        
        private static void PrintAllEntities()
        {
            PrintAll(_unitOfWork.Colors.GetAll());
            PrintAll(_unitOfWork.Players.GetAll());
            PrintAll(_unitOfWork.ChessGameInfos.GetAll());
            PrintAll(_unitOfWork.Games.GetAll());
            PrintAll(_unitOfWork.Sides.GetAll());
            PrintAll(_unitOfWork.Moves.GetAll());
        }

        private static void PrintAll<T>(IEnumerable<T> collection) where T : BaseEntity
        {
            WriteLine(typeof(T).Name + " table:");
            foreach (var e in collection)
                WriteLine(GetEntityString(e));
            WriteLine("--------------");
        }

        private static string GetEntityString(BaseEntity entity) =>
            entity switch
            {
                Game g => $"Game. {g.Id}. FEN: {g.Fen}. Additional info: id {g.Id}, {g.ChessGameInfo}",
                Side s => $"Side. {s.Id}. GameId: {s.Game.Id}. PlayerNick: {s.Player.Nickname}. Color: {s.Color}",
                Move m => $"Move. {m.Id}. GameId: {m.Game?.Id}. PlayerNick: {m.Player?.Nickname}. FEN before move: {m.Fen}. Move: {m.MoveNext}",
                Color c => $"Color. {c.Id}. Color: {c}",
                Player p => $"Player. {p.Id}. {p.Nickname}, {p.Email}, {p.Password}",
                ChessGameInfo i => $"ChessGameInfo. {i.Id}, {i}",
                _ => $"BaseEntity or it's unknown inheritor. {entity.Id}."
            };
    }
}