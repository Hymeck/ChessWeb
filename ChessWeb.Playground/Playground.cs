using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessWeb.Domain.Entities;
using ChessWeb.Persistence.Implementations;
using ChessWeb.Persistence.Contexts;
using ChessWeb.Domain.Interfaces.UnitsOfWork;
using ChessWeb.Service.Interfaces;
using ChessWeb.Service.Services;
using ChessWeb.Client;
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
            // PlayingWithPlayerAdding();
            PrintAllEntities();
            // MakeMove();
        }

        private static void PlayinWithClient()
        {
            var host = "someHost";
            var user = "Hymeck";
            var client = new ChessClient(host, user);
        }

        private static void PlayingWithPlayerAdding()
        {
            var game = _unitOfWork.Games.Get(1);
            var user = _unitOfWork.Players.Get(2);
            var color = _unitOfWork.Colors.Get(2);
            IChessService chessService = new ChessService(_unitOfWork);
            chessService.AddToGame(user, game, color);
        }
        
        
        private static void MakeMove()
        {
            var game1 = _unitOfWork.Games.Get(1);
            var sides = _unitOfWork.Sides.GetAll().Where(x => x.Game == game1);

            var move1 = _unitOfWork.Moves.Get(1);
            WriteLine($"Current move: {GetEntityString(move1)}");
            var side = sides.FirstOrDefault(x => x.User == move1.User);
            WriteLine("Moving side: " + GetEntityString(side));
            WriteLine("Moving player: " + GetEntityString(side.User));
            
            IChessService chessService = new ChessService(_unitOfWork);
            var nextGame = chessService.MakeMove(game1, move1, side);
            WriteLine($"Game after move: {GetEntityString(nextGame)}");
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
                User = whitePlayer
            };
            var blackSide = new Side
            {
                Game = game1,
                Color = blackColor,
                User = blackPlayer
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
                User = whitePlayer,
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
            PrintAll(_unitOfWork.Games.GetAll());
            PrintAll(_unitOfWork.Sides.GetAll());
            PrintAll(_unitOfWork.Moves.GetAll());
        }

        private static void PrintAll<T>(IEnumerable<T> collection) where T : class
        {
            WriteLine(typeof(T).Name + " table:");
            foreach (var e in collection)
                WriteLine(GetEntityString(e));
            WriteLine("--------------");
        }

        private static string GetEntityString<T>(T entity) where T : class =>
            entity switch
            {
                Game g => $"Game. {g.Id}. FEN: {g.Fen}",
                Side s => $"Side. {s.Id}. GameId: {s.Game?.Id}. PlayerNick: {s.User?.UserName}. Color: {s.Color}",
                Move m => $"Move. {m.Id}. GameId: {m.Game?.Id}. PlayerNick: {m.User?.UserName}. FEN before move: {m.Fen}. Move: {m.MoveNext}",
                Color c => $"Color. {c.Id}. Color: {c}",
                User u => $"User. {u.Id}. {u.UserName}, {u.Email}",
                _ => "BaseEntity or it's unknown inheritor"
            };
    }
}