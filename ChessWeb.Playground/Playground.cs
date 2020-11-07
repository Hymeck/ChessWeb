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
            // AddEntities(colorRepository, YieldColors);
            ShowEntities(colorRepository);
            
            IRepository<Game> gameRepository = new Repository<Game>(applicationContext);
            // AddEntities(gameRepository, YieldGames);
            ShowEntities(gameRepository);
            
            IRepository<Side> sideRepository = new Repository<Side>(applicationContext);
            // AddEntities(sideRepository, YieldSides);
            ShowEntities(sideRepository);
        }

        private static IEnumerable<Color> YieldColors()
        {
            yield return new Color {ColorType = true};
            yield return new Color {ColorType = false};
        }

        private static IEnumerable<Side> YieldSides()
        {
            yield return new Side {ColorId = 1, GameId = 1, PlayerId = 1};
            yield return new Side {ColorId = 2, GameId = 1, PlayerId = 5};
        }

        private static IEnumerable<Game> YieldGames()
        {
            yield return new Game {Fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1"};
        }
        
        private static void AddEntities<T>(
            IRepository<T> repository, 
            Func<IEnumerable<T>> entityFiller) where T : BaseEntity 
        {
            foreach (var entity in entityFiller())
                repository.Insert(entity);
        }
        private static IEnumerable<Player> YieldPlayers()
        {
            // var player1 = new Player {Email = "noonimf@gmail.com", Nickname = "Hymeck", Password = "hymeckpass"};
            // var player2 = new Player {Email = "mr.yyatson@gmail.com", Nickname = "Racoty", Password = "racotypass"};
            // var player3 = new Player {Email = "vadimyaren@yandex.by", Nickname = "Yaren", Password = "yarenpass"};
            // var player4 = new Player {Email = "some_email@gmail.com", Nickname = "Someone", Password = "someonepass"};
             var player5 = new Player {Email = "mr.yatson@gmail.com", Nickname = "Racoty", Password = "racotypass"};

            // yield return player1;
            // yield return player2;
            // yield return player3;
            // yield return player4;
            yield return player5;
        }
        
        private static void AddPlayers(IRepository<Player> repository)
        {
            foreach (var player in YieldPlayers())
                repository.Insert(player);
        }

        private static void ShowEntities<T>(IRepository<T> repository) where T : BaseEntity
        {
            foreach (var entity in repository.GetAll())
                WriteLine(GetEntityString(entity));
        }

        private static void UpdatePlayers(IRepository<Player> repository)
        {
            var player = repository.Get(2);
            player.Email = player.Email.Remove(3, 1);
            repository.Update(player);
        }

        private static void DeletePlayers(IRepository<Player> repository)
        {
            repository.Delete(repository.Get(2));
        }

        private static string GetEntityString(BaseEntity entity) =>
            entity switch
            {
                Game g => $"Game. {g.Id}. FEN: {g.Fen}",
                Side s => $"Side. {s.Id}. GameId: {s.GameId}. PlayerId: {s.PlayerId}. ColorId: {s.ColorId}",
                Move m => $"Move. {m.Id}. GameId: {m.GameId}. PlayerId: {m.PlayerId}. FEN before move: {m.Fen}. Move: {m.MoveNext}",
                Color c => $"Color. {c.Id}. Color: {c}",
                Player p => $"Player. {p.Id}. {p.Nickname}, {p.Email}, {p.Password}",
                _ => $"BaseEntity or it's inheritor. {entity.Id}."
            };
    }
}