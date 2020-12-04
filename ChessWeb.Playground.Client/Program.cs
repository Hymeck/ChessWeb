using System;
using ChessWeb.Client;
using static System.Console;

namespace ChessWeb.Playground.Client
{
    class Program
    {
        private static readonly string host = "http://localhost:5000/";
        private static readonly string user = "Hymeck";
        static void Main(string[] args)
        {
            var program = new Program();
            program.Start();
            ReadKey();
        }

        private void Start()
        {
            var client = new ChessClient(host, user);
            WriteLine(client.Host);
            try
            {
                client.GetCurrentGame();
            }

            catch (System.Net.WebException e)
            {
                WriteLine(e.Message);
                WriteLine(e.StackTrace);
            }
        }
    }
}