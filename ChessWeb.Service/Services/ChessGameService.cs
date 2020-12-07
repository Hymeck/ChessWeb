using System;
using System.Diagnostics;
using System.Linq;
using ChessEngine;
using ChessEngine.Console;
using ChessEngine.Domain;
using ChessEngine.Exceptions;
using ChessWeb.Domain.Entities;
using ChessWeb.Service.Interfaces;
using Color = ChessWeb.Domain.Entities.Color;

namespace ChessWeb.Service.Services
{
    public class ChessGameService : IChessGameService
    {
        // private IUnitOfWork _unitOfWork;
        //
        // public ChessGameService(IUnitOfWork unitOfWork) => 
        //     _unitOfWork = unitOfWork;
        //
        // public Game MakeMove(Game game, Move move, Side side)
        // {
        //     // todo: uncomment this checks
        //     // if (side.User.Id != move.User.Id)
        //     //     return game;
        //     // if (game.Id != move.Game.Id || game.Id != side.Game.Id || move.Game.Id != side.Game.Id)
        //     //     return game;
        //     // if (move.Fen != game.Fen)
        //     //     return game;
        //     var fen = game.Fen;
        //     var chessGame = new ChessGame(fen);
        //     if (side.Color.ToChar() != (char) chessGame.ActiveColor)
        //         return game;
        //
        //     var moveNext = move.MoveNext;
        //     try
        //     {
        //         var squares = MoveInputParser.ParseMove(moveNext);
        //         var nextChessGame = chessGame.Move(squares);
        //
        //         game.Fen = nextChessGame.ToString();
        //
        //         _unitOfWork.Games.Update(game);
        //         _unitOfWork.Complete();
        //         return game;
        //     }
        //
        //     catch (FormatException e)
        //     {
        //         Debug.WriteLine(e.Message + "\n" +e.StackTrace);
        //         return game;
        //     }
        //
        //     catch (ChessGameException e)
        //     {
        //         Debug.WriteLine(e.Message + "\n" +e.StackTrace);
        //         return game;
        //     }
        // }
        //
        // public void AddToGame(User user, Game game, Color color)
        // {
        //     var gameSides = _unitOfWork.Sides.GetAll().Where(x => x.Game == game);
        //     
        //     if (gameSides.Count() >= 2)
        //         return;
        //
        //     var sideUser = gameSides.FirstOrDefault(x => x.User.UserName == user.UserName);
        //     if (sideUser != null)
        //         return;
        //     
        //     var colorSide = gameSides.FirstOrDefault(x => x.Color == color);
        //     if (colorSide != null) 
        //         return;
        //     
        //     var side = new Side
        //     {
        //         Color = color,
        //         Game = game,
        //         User = user
        //     };
        //     _unitOfWork.Sides.Add(side);
        //     _unitOfWork.Complete();
        // }
        public Game MakeMove(Game game, Move move, Side side)
        {
            throw new NotImplementedException();
        }

        public void AddToGame(User user, Game game, Color color)
        {
            throw new NotImplementedException();
        }
    }
}