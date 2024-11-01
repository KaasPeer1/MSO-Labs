using EduCode.Model.Board;

namespace EduCode.Model.Command;

public interface IEduCommand
{
    public void Execute(EduBoard board);
    public int MaximumDepth { get; }
}