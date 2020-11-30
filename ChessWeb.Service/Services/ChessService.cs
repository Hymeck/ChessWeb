using System;
using System.Diagnostics;
using ChessEngine;
using ChessEngine.Console;
using ChessEngine.Domain;
using ChessEngine.Exceptions;
using ChessWeb.Domain.Entities;
using ChessWeb.Domain.Interfaces.UnitsOfWork;
using ChessWeb.Service.Interfaces;

namespace ChessWeb.Service.Services
{
    public class ChessService : IChessService
    {
        private IUnitOfWork _unitOfWork;

        public ChessService(IUnitOfWork unitOfWork) => 
            _unitOfWork = unitOfWork;

        public Game MakeMove(Game game, Move move, Side side)
        {
            // todo: uncomment this checks
            // if (side.Player.Id != move.Player.Id)
            //     return game;
            // if (game.Id != move.Game.Id || game.Id != side.Game.Id || move.Game.Id != side.Game.Id)
            //     return game;
            // if (move.Fen != game.Fen)
            //     return game;
            var fen = game.Fen;
            var chessGame = new ChessGame(fen);
            if (side.Color.ToChar() != (char) chessGame.ActiveColor)
                return game;

            var moveNext = move.MoveNext;
            try
            {
                var squares = MoveInputParser.ParseMove(moveNext);
                var nextChessGame = chessGame.Move(squares);
                // // invalid move
                // if (nextChessGame == chessGame)
                //     return game;

                game.Fen = nextChessGame.ToString();

                _unitOfWork.Games.Update(game);
                _unitOfWork.Complete();

                return game;
            }

            catch (FormatException e)
            {
                Debug.WriteLine(e.Message + "\n" +e.StackTrace);
                return game;
            }

            catch (ChessGameException e)
            {
                Debug.WriteLine(e.Message + "\n" +e.StackTrace);
                return game;
            }
        }
    }
}