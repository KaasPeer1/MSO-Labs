using EduCode.Model.Board;

namespace EduCode.Model.Condition;

public class EduCondition
{
    private Predicate<EduBoard> _predicate;

    private EduCondition(Predicate<EduBoard> predicate)
    {
        _predicate = predicate;
    }

    public string StringRepresentation { get; set; }

    public static EduCondition Parse(string condition)
    {
        EduCondition result;
        switch (condition)
        {
            case "WallAhead":
                result = new EduCondition(board => board.WallAhead());
                break;
            case "GridEdge":
                result = new EduCondition(board => board.GridEdge());
                break;
            default:
                throw new ArgumentException("Invalid condition");
        }
        result.StringRepresentation = condition;
        return result;
    }

    public bool Evaluate(EduBoard board)
    {
        return _predicate(board);
    }

    public override string ToString()
    {
        return StringRepresentation;
    }
}