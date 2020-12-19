using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace ChessWeb.Client
{
    public sealed class ChessClient
    {
        // private const string Host = "http://localhost:5000/";
        private const string Host = "http://chessbsuir.herokuapp.com/";
        
        private const string UserPath = "api/users/";
        private const string GamePath = "api/games/";
        private const string ActiveGames = "active/";
        private const string WaitingGames = "waiting/";
        private const string Create = "create/";
        private const string Last = "last/";

        public readonly string Username;
        public readonly string Password;
        private readonly string userId;
        public readonly IBoardHandler BoardHandler;

        private const string pattern = @"""(\w+)\"":""?([^,""}]*)""?";

        public ChessClient(string username, string password, IBoardHandler boardHandler)
        {
            Username = username;
            Password = password;
            userId = CallGetUserId();
            BoardHandler = boardHandler;
        }
        
        public ChessClient(string username, string password) : this(username, password, new BoardHandler()) {}

        public GameInfo GetGame(string gameId) =>
            new(ParseJson(CallGames(gameId)));

        public GameInfo MakeMove(string gameId, string move) =>
            new(ParseJson(CallMakeMove(gameId, move)));

        public GameInfo Join(string gameId, bool isWhite)
        {
            var response = CallJoin(gameId, isWhite);
            return GetGame(gameId);
        }

        /// <summary>
        /// Core function. Connects to the host with specified args and returns a response as a json string
        /// </summary>
        /// <param name="args"></param>
        private string CallServer(string args = "")
        {
            var request = WebRequest.Create(Host + args);
            var response = request.GetResponse();
            using var stream = response.GetResponseStream();
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        #region User methods

        private string CallJoin(string gameId, bool isWhite) =>
            CallUsers($"{gameId}/{userId}/{isWhite}");
        private string CallUsers(string args = "") =>
            CallServer(UserPath + args);

        private string CallGetUserId() =>
            CallUsers($"{Username}/{Password}");

        private string CallMakeMove(string gameId, string move) => 
            CallGames($"{gameId}/{userId}/{move}");

        #endregion User methods
        
        #region Game methods
        private string CallGames(string args = "") =>
            CallServer(GamePath + args);

        private string CallActiveGames(string id = "") =>
            CallGames(ActiveGames);

        private string CallWaitingGames(string id = "") =>
            CallGames(WaitingGames);
        #endregion Game methods

        public GameInfo CreateGame()
        {
            CallGames(Create);
            return GetLastGame();
        }

        public GameInfo LastActiveUserGame
        {
            get
            {
                var response = CallUsers($"{userId}/last");

                return new(ParseJson(response));
            }
        }

        public GameInfo GetLastGame()
        {
            var gameJson = CallGames(Last);
            return new(ParseJson(gameJson));
        }

        private NameValueCollection ParseJson(string json)
        {
            var list = new NameValueCollection();

            foreach (Match m in Regex.Matches(json, pattern))
                if (m.Groups.Count == 3)
                    list[m.Groups[1].Value] = m.Groups[2].Value;

            return list;
        }
    }
}