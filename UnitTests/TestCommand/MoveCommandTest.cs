using EduCode.Board;
using EduCode.Command;
using EduCode.Location;

namespace UnitTests.TestCommand;

public class MoveCommandTest
{
    public static IEnumerable<object[]> GetTestData()
    {
        yield return new object[] { new EduBoard(3), 0, new EduBoard(3) };
        yield return new object[] { new EduBoard(3), 1, new EduBoard(3) { Position = new Position(1, 0) } };
        yield return new object[] { new EduBoard(3, direction: Direction.South), 1, new EduBoard(3, direction: Direction.South) { Position = new Position(0, 1) } };
        yield return new object[] { new EduBoard(3), -3, new EduBoard(3) { Position = new Position(0, 0) } };
    }

    [Theory]
    [MemberData(nameof(GetTestData))]
    public void TestExecute(EduBoard board, int amount, EduBoard expected)
    {
        MoveCommand moveCommand = new MoveCommand(amount);
        moveCommand.Execute(board);
        Assert.Equivalent(expected, board);
    }
}