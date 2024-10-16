namespace ProgrammingApp;

public class RepeatCommand : ICommand
{
    private readonly List<ICommand> _commands = new ();

    public RepeatCommand(int times, List<ICommand> commands)
    {
        for (var i = 0; i < times; i++)
        {
            _commands.AddRange(commands);
        }
    }

    public void Execute(Board board)
    {
        foreach (var command in _commands)
        {
            command.Execute(board);
        }
    }

    public override string ToString()
    {
        return _commands.Select(c => c.ToString()).Aggregate((a, b) => $"{a}, {b}") + "";
    }
}