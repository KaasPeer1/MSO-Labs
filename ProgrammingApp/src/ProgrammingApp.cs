namespace ProgrammingApp;

internal class ProgrammingApp
{
    static void Main(string[] args)
    {
        Board board = new(3);
        Program program = new (new List<ICommand>
        {
            new MoveCommand(10),
            new TurnCommand("right"),
            new RepeatCommand(2, new List<ICommand>
            {
                new MoveCommand(10),
                new TurnCommand("right")
            }),
            new MoveCommand(10),
            new TurnCommand("right")
        });
        Console.WriteLine($"Starting state {board}");
        program.Run(board);
        Console.WriteLine($"End state {board}");
    }
}