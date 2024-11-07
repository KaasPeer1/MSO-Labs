using EduCode.Model.Board;
using EduCode.Model.Location;

namespace EduCode.Model.Command;

public interface IEduCommand
{
    public Position[] Execute(EduBoard board);
    public int MaximumDepth { get; }
}