using System.IO;
using EduCode.Model.Location;

namespace EduCode.Model.Board;

public class GridParser
{
    public static void ParseFromFile(string path, out int size, out List<Position> walls, out Position? endPosition)
    {
        Parse(File.ReadAllLines(path).Where(arg => !string.IsNullOrWhiteSpace(arg)), out size, out walls, out endPosition);
    }

    public static void ParseFromString(string input, out int size, out List<Position> walls, out Position? endPosition)
    {
        Parse(input.Split(Environment.NewLine).Where(arg => !string.IsNullOrWhiteSpace(arg)), out size, out walls, out endPosition);
    }

    private static void Parse(IEnumerable<string> lines, out int size, out List<Position> walls, out Position? endPosition)
    {
        walls = new List<Position>();
        endPosition = null;

        var enumerable = lines as string[] ?? lines.ToArray();
        size = enumerable.Length;

        for (var i = 0; i < size; i++)
        {
            var line = enumerable[i].Trim();
            for (var j = 0; j < line.Length; j++)
            {
                if (line[j] == '+') walls.Add(new Position(j, i));
                else if (line[j] == 'x') endPosition = new Position(j, i);
            }
        }
    }
}