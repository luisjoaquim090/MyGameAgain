using Console.Utils;
internal class Program
{
    private static void Main(string[] args)
    {
        var p_args = Args.ProcessArgs(args);
        string? title = (string?)p_args.FirstOrDefault(e => e.Item1.Equals(Args.ArgTypes.Title))?.Item2;
        int? X = (int?)p_args.FirstOrDefault(e => e.Item1.Equals(Args.ArgTypes.gridX))?.Item2;
        int? Y = (int?)p_args.FirstOrDefault(e => e.Item1.Equals(Args.ArgTypes.gridY))?.Item2;
        int? Z = (int?)p_args.FirstOrDefault(e => e.Item1.Equals(Args.ArgTypes.gridZ))?.Item2;
        do {
            System.Console.Clear();
            var game_board = new Game.Board(title, X, Y, Z);
            game_board.ToConsole();
        } while (System.Console.ReadKey().Key == ConsoleKey.R);
    }
}