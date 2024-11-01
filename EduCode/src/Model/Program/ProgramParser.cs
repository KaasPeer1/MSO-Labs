using System.IO;
using System.Text.RegularExpressions;
using EduCode.Model.Command;

namespace EduCode.Model.Program;

public class ProgramParser
{
    public static EduProgram ParseFile(string path)
    {
        string[] lines = File.ReadAllLines(path);
        return Parse(lines);
    }

    public static EduProgram? ParseString(string program)
    {
        if (string.IsNullOrWhiteSpace(program)) return null;
        string[] lines = program.Split(Environment.NewLine);
        return Parse(lines);
    }

    private static EduProgram Parse(string[] lines)
    {
        Stack<int> indentStack = new();
        indentStack.Push(0);
        int currentLineIndex = 0;
        var commands = ParseBlock(lines, indentStack, ref currentLineIndex);
        return new EduProgram(commands);
    }

    private static List<IEduCommand> ParseBlock(string[] lines, Stack<int> indentStack, ref int currentLineIndex)
    {
        List<IEduCommand> commands = new();

        while (currentLineIndex < lines.Length)
        {
            var line = lines[currentLineIndex];

            if (string.IsNullOrWhiteSpace(line))
            {
                currentLineIndex++;
                continue;
            }

            var actualIndent = GetIndentation(line);
            var expectedIndent = indentStack.Peek();

            if (actualIndent < expectedIndent)
            {
                indentStack.Pop();
                break;
            }
            if (actualIndent > expectedIndent)
            {
                throw new FormatException("Invalid indentation");
            }

            commands.Add(ParseCommand(line, lines, indentStack, ref currentLineIndex));
            currentLineIndex++;
        }

        return commands;
    }

    private static IEduCommand ParseCommand(string line, string[] lines, Stack<int> indentStack, ref int currentLineIndex)
    {
        var trimmedLine = line.Trim();
        var parts = trimmedLine.Split(' ');

        return parts[0] switch
        {
            "Move" => ParseMove(trimmedLine),
            "Turn" => ParseTurn(trimmedLine),
            "Repeat" => ParseRepeat(trimmedLine, lines, indentStack, ref currentLineIndex),
            _ => throw new FormatException("Invalid command")
        };
    }

    private static IEduCommand ParseMove(string line)
    {
        const string pattern = @"Move (\d+)";
        if (!Regex.IsMatch(line, pattern)) throw new FormatException("Invalid move command");

        return new MoveCommand(int.Parse(line.Split(' ')[1]));
    }

    private static IEduCommand ParseTurn(string line)
    {
        const string pattern = @"Turn (left|right)";
        if (!Regex.IsMatch(line, pattern)) throw new FormatException("Invalid turn command");

        return new TurnCommand(line.Split(' ')[1]);
    }

    private static IEduCommand ParseRepeat(string line, string[] lines, Stack<int> indentStack, ref int currentLineIndex)
    {
        const string pattern = @"Repeat (\d+) times";
        if (!Regex.IsMatch(line, pattern)) throw new FormatException("Invalid repeat command");

        indentStack.Push(GetIndentation(lines[currentLineIndex + 1]));
        currentLineIndex++;

        var count = int.Parse(line.Split(' ')[1]);
        var block = ParseBlock(lines, indentStack, ref currentLineIndex);
        currentLineIndex--;
        return new RepeatCommand(count, block);
    }

    private static int GetIndentation(string line)
    {
        var indent = 0;
        foreach (var c in line)
        {
            if (c == ' ')
            {
                indent++;
            }
            else
            {
                break;
            }
        }

        return indent;
    }
}