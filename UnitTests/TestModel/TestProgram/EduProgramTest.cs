using EduCode.Model.Board;
using EduCode.Model.Command;
using EduCode.Model.Location;
using EduCode.Model.Program;

namespace UnitTests.TestModel.TestProgram;

public class EduProgramTest
{
    public static IEnumerable<object[]> GetCorrectTextualTracesCommands()
    {
        yield return new object[] { new List<IEduCommand>(), "" };
        yield return new object[] { new List<IEduCommand> { new MoveCommand(10) }, "Move 10." };
        yield return new object[] { new List<IEduCommand> { new TurnCommand("right") }, "Turn right." };
        yield return new object[] { new List<IEduCommand> { new RepeatCommand(2, new List<IEduCommand> { new MoveCommand(10), new TurnCommand("left") }) }, "Move 10, Turn left, Move 10, Turn left." };
    }

    [Theory]
    [MemberData(nameof(GetCorrectTextualTracesCommands))]
    public void CorrectTextualTraces(List<IEduCommand> commands, string expectedTrace)
    {
        EduProgram program = new(commands);

        var actualTrace = program.TextualTrace;
        Assert.Equal(expectedTrace, actualTrace);
    }

    public static IEnumerable<object[]> GetCorrectRunCommands()
    {
        yield return new object[] { new EduBoard(3), new List<IEduCommand>(), new EduBoard(3) };
        yield return new object[] { new EduBoard(3), new List<IEduCommand> { new MoveCommand(-1) }, new EduBoard(3) };
        yield return new object[] { new EduBoard(3), new List<IEduCommand> { new MoveCommand(1) }, new EduBoard(3) { Position = new Position(1, 0) } };
        yield return new object[] { new EduBoard(3), new List<IEduCommand> { new MoveCommand(3) }, new EduBoard(3) { Position = new Position(2, 0) } };
        yield return new object[] { new EduBoard(3), new List<IEduCommand> { new MoveCommand(4) }, new EduBoard(3) { Position = new Position(2, 0) } };
        yield return new object[] { new EduBoard(3), new List<IEduCommand> { new TurnCommand("right") }, new EduBoard(3) { Direction = Direction.South } };
        yield return new object[] { new EduBoard(3), new List<IEduCommand> { new TurnCommand("left") }, new EduBoard(3) { Direction = Direction.North } };
        yield return new object[] { new EduBoard(3), new List<IEduCommand> { new RepeatCommand(2, new List<IEduCommand> { new MoveCommand(1), new TurnCommand("right") }) }, new EduBoard(3) { Position = new Position(1, 1), Direction = Direction.West } };
    }

    [Theory]
    [MemberData(nameof(GetCorrectRunCommands))]
    public void CorrectRun(EduBoard board, List<IEduCommand> commands, EduBoard expectedBoard)
    {
        EduProgram program = new(commands);

        program.Run(board);

        Assert.Equivalent(expectedBoard, board);
    }
}