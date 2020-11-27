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
            // MainFunction();
            // PlayingWithMove();
            PrintAllEntities();
            // using var applicationContext = new ApplicationContext();
            // IRepository<Color> colorRepository = new Repository<Color>(applicationContext);
            // ShowEntities(colorRepository);
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