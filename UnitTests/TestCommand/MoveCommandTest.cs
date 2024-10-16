using ProgrammingApp;

namespace TestProgrammingApp.TestCommand;

public class MoveCommandTest
{
    public static IEnumerable<object[]> GetTestData()
    {
        yield return new object[] { new Board(), 0, new Board() };
        yield return new object[] { new Board(), 1, new Board { Position = new Position(1, 0) } };
        yield return new object[] { new Board(direction: Direction.South), 1, new Board(direction:Direction.South) { Position = new Position(0, 1) } };
        yield return new object[] { new Board(), -3, new Board { Position = new Position(0, 0) } };
    }

    [Theory]
    [MemberData(nameof(GetTestData))]
    public void TestExecute(Board board, int amount, Board expected)
    {
        MoveCommand moveCommand = new MoveCommand(amount);
        moveCommand.Execute(board);
        Assert.Equivalent(expected, board);
    }
}