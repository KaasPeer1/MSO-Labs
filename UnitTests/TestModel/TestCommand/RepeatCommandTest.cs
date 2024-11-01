using EduCode.Model.Board;
using EduCode.Model.Command;
using EduCode.Model.Location;

namespace UnitTests.TestModel.TestCommand;

public class RepeatCommandTest
{
    public static IEnumerable<object[]> GetCommands()
    {
        yield return new object[] { 2, new List<IEduCommand>(), new EduBoard(3) };
        yield return new object[] { 2, new List<IEduCommand> { new MoveCommand(-1) }, new EduBoard(3) };
        yield return new object[] { 0, new List<IEduCommand> { new MoveCommand(1), new TurnCommand("left") }, new EduBoard(3) };
        yield return new object[] { 1, new List<IEduCommand> { new MoveCommand(1), new TurnCommand("left") }, new EduBoard(3, direction: Direction.North) { Position = new Position(1, 0) } };
        yield return new object[] { 2, new List<IEduCommand> { new MoveCommand(1), new TurnCommand("right") }, new EduBoard(3, direction: Direction.West) { Position = new Position(1, 1) } };
    }

    [Theory]
    [MemberData(nameof(GetCommands))]
    public void TestRepeatCommand(int times, List<IEduCommand> commands, EduBoard expectedBoard)
    {
        RepeatCommand repeatCommand = new(times, commands);
        EduBoard board = new(3);

        repeatCommand.Execute(board);

        Assert.Equivalent(expectedBoard, board);
    }
}