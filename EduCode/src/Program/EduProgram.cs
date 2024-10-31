using EduCode.Board;
using EduCode.Command;

namespace EduCode.Program;

public class EduProgram
{
    private readonly List<IEduCommand> _commands;

    public EduProgram(List<IEduCommand> commands)
    {
        _commands = commands;
    }

    public string TextualTrace => _commands.Count == 0 ? "" : _commands.Select(c => c.ToString()).Aggregate((a, b) => $"{a}, {b}") + ".";

    public int CommandCount => _commands.Count;
    
    public int MaximumDepth => _commands.Count == 0 ? 0 : _commands.Max(c => c.MaximumDepth);

    public static EduProgram BasicProgram => new(new List<IEduCommand>
    {
        new MoveCommand(5),
        new TurnCommand("left"),
        new TurnCommand("left"),
        new MoveCommand(1),
        new TurnCommand("left"),
        new MoveCommand(2)
    });

    public static EduProgram AdvancedProgram => new(new List<IEduCommand>
    {
        new MoveCommand(1),
        new TurnCommand("right"),
        new RepeatCommand(2, new List<IEduCommand>
        {
            new MoveCommand(1),
            new TurnCommand("right"),
            new MoveCommand(1),
            new TurnCommand("left")
        }),
        new MoveCommand(10),
        new TurnCommand("right")
    });

    public static EduProgram ExpertProgram => new(new List<IEduCommand>
    {
        new TurnCommand("right"),
        new RepeatCommand(5, new List<IEduCommand>
        {
            new RepeatCommand(2, new List<IEduCommand>
            {
                new TurnCommand("right"),
                new MoveCommand(1)
            }),
            new TurnCommand("left"),
        }),
        new MoveCommand(10),
        new TurnCommand("right")
    });

    public void Run(EduBoard board)
    {
        foreach (var command in _commands)
        {
            command.Execute(board);
        }
    }
}