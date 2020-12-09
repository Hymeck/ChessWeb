namespace ChessWeb.Client
{
    public interface IBoardPrinter
    {
        string[] GetBoard(string fen);
    }
}