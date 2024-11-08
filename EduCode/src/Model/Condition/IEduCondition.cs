using EduCode.Model.Board;

namespace EduCode.Model.Condition;

public interface IEduCondition
{
    public bool Evaluate(EduBoard board);
}