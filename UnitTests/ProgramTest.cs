using ProgrammingApp;

namespace TestProgrammingApp;

public class ProgramTest
{
    public static IEnumerable<object[]> GetCorrectTextualTracesCommands()
    {
        yield return new object[] { new List<ICommand>(), "" };
        yield return new object[] { new List<ICommand> { new MoveCommand(10) }, "Move 10." };
        yield return new object[] { new List<ICommand> { new TurnCommand("right") }, "Turn right." };
        yield return new object[] { new List<ICommand> { new RepeatCommand(2, new List<ICommand> { new MoveCommand(10), new TurnCommand("left") }) }, "Move 10, Turn left, Move 10, Turn left." };
    }

    [Theory]
    [MemberData(nameof(GetCorrectTextualTracesCommands))]
    public void CorrectTextualTraces(List<ICommand> commands, string expectedTrace)
    {
        Program program = new(commands);

        var actualTrace = program.TextualTrace;

        Assert.Equal(expectedTrace, actualTrace);
    }

    public static IEnumerable<object[]> GetCorrectRunCommands()
    {
        yield return new object[] { new Board(3), new List<ICommand>(), new Board(3) };
        yield return new object[] { new Board(3), new List<ICommand> { new MoveCommand(-1) }, new Board(3) };
        yield return new object[] { new Board(3), new List<ICommand> { new MoveCommand(1) }, new Board(3) { Position = new Position(1, 0) } };
        yield return new object[] { new Board(3), new List<ICommand> { new MoveCommand(3) }, new Board(3) { Position = new Position(2, 0) } };
        yield return new object[] { new Board(3), new List<ICommand> { new MoveCommand(4) }, new Board(3) { Position = new Position(2, 0) } };
        yield return new object[] { new Board(3), new List<ICommand> { new TurnCommand("right") }, new Board(3) { Direction = Direction.South } };
        yield return new object[] { new Board(3), new List<ICommand> { new TurnCommand("left") }, new Board(3) { Direction = Direction.North } };
        yield return new object[] { new Board(3), new List<ICommand> { new RepeatCommand(2, new List<ICommand> { new MoveCommand(1), new TurnCommand("right") }) }, new Board(3) { Position = new Position(1, 1), Direction = Direction.West } };
    }

    [Theory]
    [MemberData(nameof(GetCorrectRunCommands))]
    public void CorrectRun(Board board, List<ICommand> commands, Board expectedBoard)
    {
        Program program = new(commands);

        program.Run(board);

        Assert.Equivalent(expectedBoard, board);
    }
}