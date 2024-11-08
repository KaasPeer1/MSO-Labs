using EduCode.Model.Board;

namespace EduCode.Model.Condition;

public class EduCondition : IEduCondition
{
    private readonly Predicate<EduBoard> _predicate;

    private EduCondition(Predicate<EduBoard> predicate)
    {
        _predicate = predicate;
    }

    public string? StringRepresentation { get; set; }

    public static EduCondition Parse(string condition)
    {
        var result = condition switch
        {
            "WallAhead" => new EduCondition(board => board.WallAhead()),
            "GridEdge" => new EduCondition(board => board.GridEdge()),
            _ => throw new ArgumentException("Invalid condition")
        };
        result.StringRepresentation = condition;
        return result;
    }

    public bool Evaluate(EduBoard board)
    {
        return _predicate(board);
    }

    public override string ToString()
    {
        return StringRepresentation ?? "";
    }
}