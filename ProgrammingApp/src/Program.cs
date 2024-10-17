namespace ProgrammingApp;

public class Program
{
    private readonly List<ICommand> _commands;

    public Program(List<ICommand> commands)
    {
        _commands = commands;
    }

    public string TextualTrace => _commands.Count == 0 ? "" : _commands.Select(c => c.ToString()).Aggregate((a, b) => $"{a}, {b}") + ".";

    public static Program BasicProgram => new(new List<ICommand>
    {
        new MoveCommand(5),
        new TurnCommand("left"),
        new TurnCommand("left"),
        new MoveCommand(1),
        new TurnCommand("left"),
        new MoveCommand(2)
    });

    public static Program AdvancedProgram => new(new List<ICommand>
    {
        new MoveCommand(1),
        new TurnCommand("right"),
        new RepeatCommand(2, new List<ICommand>
        {
            new MoveCommand(1),
            new TurnCommand("right"),
            new MoveCommand(1),
            new TurnCommand("left")
        }),
        new MoveCommand(10),
        new TurnCommand("right")
    });

    public static Program ExpertProgram => new(new List<ICommand>
    {
        new TurnCommand("right"),
        new RepeatCommand(5, new List<ICommand>
        {
            new RepeatCommand(2, new List<ICommand>
            {
                new TurnCommand("right"),
                new MoveCommand(1)
            }),
            new TurnCommand("left"),
        }),
        new MoveCommand(10),
        new TurnCommand("right")
    });

    public void Run(Board board)
    {
        foreach (var command in _commands)
        {
            command.Execute(board);
        }
    }
}