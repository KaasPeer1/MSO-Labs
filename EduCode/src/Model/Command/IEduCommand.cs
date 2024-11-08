using EduCode.Model.Board;
using EduCode.Model.Location;

namespace EduCode.Model.Command;

public interface IEduCommand
{
    public void Execute(EduBoard board, ref List<Position> trace);
    public int MaximumDepth { get; }
}