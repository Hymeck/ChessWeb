using System;
using System.Collections.Generic;
using ChessWeb.Domain.Entities;
using ChessWeb.Persistence.Contexts;
using ChessWeb.Persistence.Implementations;
using ChessWeb.Persistence.Interfaces;

namespace ChessWeb.Playground
{
    class Playground
    {
        static void Main(string[] args)
        {
            using var applicationContext = new ApplicationContext();
            IRepository<Player> playerRepository = new Repository<Player>(applicationContext);
            
            // AddPlayers(playerRepository);
            // UpdatePlayers(playerRepository);
            // DeletePlayers(playerRepository);
            // AddPlayers(playerRepository);
            ShowPlayers(playerRepository);
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

        private static void ShowPlayers(IRepository<Player> repository)
        {
            foreach (var player in repository.GetAll())
            {
                Console.WriteLine($"{player.Id}. {player.Nickname}, {player.Email}, {player.Password}");
            }
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
    }
}