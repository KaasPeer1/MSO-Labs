namespace ProgrammingApp;

public class Program
{
    private readonly List<ICommand> _commands;

    public Program(List<ICommand> commands)
    {
        _commands = commands;
    }

    public string TextualTrace => _commands.Count == 0 ? "" : _commands.Select(c => c.ToString()).Aggregate((a, b) => $"{a}, {b}") + ".";


    public void Run(Board board)
    {
        foreach (var command in _commands)
        {
            command.Execute(board);
        }
    }
}