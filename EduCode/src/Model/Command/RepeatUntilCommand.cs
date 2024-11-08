using EduCode.Model.Board;
using EduCode.Model.Condition;
using EduCode.Model.Location;

namespace EduCode.Model.Command;

public class RepeatUntilCommand : IEduCommand
{
    private readonly IEduCondition _condition;
    private readonly List<IEduCommand> _commands;

    public RepeatUntilCommand(IEduCondition condition, List<IEduCommand> commands)
    {
        _condition = condition;
        _commands = commands;
    }

    public List<IEduCommand> Commands => _commands;
    public IEduCondition Condition => _condition;

    public void Execute(EduBoard board, ref List<Position> trace)
    {
        while (!_condition.Evaluate(board))
        {
            foreach (var command in _commands)
            {
                command.Execute(board, ref trace);
            }
        }
    }

    public int MaximumDepth => _commands.Count == 0 ? 0 : _commands.Max(c => c.MaximumDepth) + 1;

    public override string ToString()
    {
        return $"Repeat until {_condition.ToString()}: {{{_commands.Select(c => c.ToString()).Aggregate((a, b) => $"{a}, {b}") + ""}}}";
    }
}