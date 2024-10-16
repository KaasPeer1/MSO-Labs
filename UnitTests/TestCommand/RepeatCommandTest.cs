using ProgrammingApp;

namespace TestProgrammingApp.TestCommand;

public class RepeatCommandTest
{
    public static IEnumerable<object[]> GetCommands()
    {
        yield return new object[] { 2, new List<ICommand>(), new Board() };
        yield return new object[] { 2, new List<ICommand> { new MoveCommand(-1) }, new Board() };
        yield return new object[] { 0, new List<ICommand> { new MoveCommand(1), new TurnCommand("left") }, new Board() };
        yield return new object[] { 1, new List<ICommand> { new MoveCommand(1), new TurnCommand("left") }, new Board(direction:Direction.North) { Position = new Position(1, 0) } };
        yield return new object[] { 2, new List<ICommand> { new MoveCommand(1), new TurnCommand("right") }, new Board(direction: Direction.West) { Position = new Position(1, 1) } };
    }

    [Theory]
    [MemberData(nameof(GetCommands))]
    public void TestRepeatCommand(int times, List<ICommand> commands, Board expectedBoard)
    {
        RepeatCommand repeatCommand = new(times, commands);
        Board board = new();

        repeatCommand.Execute(board);

        Assert.Equivalent(expectedBoard, board);
    }
}