﻿using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using EduCode.Model.Board;
using EduCode.Model.Command;
using EduCode.Model.Exceptions;
using EduCode.Model.Location;

namespace EduCode.Model.Program;

public class EduProgram
{
    private readonly List<IEduCommand> _commands;

    public EduProgram(List<IEduCommand> commands)
    {
        _commands = commands;
    }

    public string TextualTrace => _commands.Count == 0 ? "" : _commands.Select(c => c.ToString()).Aggregate((a, b) => $"{a}, {b}") + ".";

    public int CommandCount => _commands.Count;
    
    public int MaximumDepth => _commands.Count == 0 ? 0 : _commands.Max(c => c.MaximumDepth);

    public static EduProgram BasicProgram => new(new List<IEduCommand>
    {
        new MoveCommand(5),
        new TurnCommand("left"),
        new TurnCommand("left"),
        new MoveCommand(1),
        new TurnCommand("left"),
        new MoveCommand(2)
    });

    public static EduProgram AdvancedProgram => new(new List<IEduCommand>
    {
        new MoveCommand(1),
        new TurnCommand("right"),
        new RepeatCommand(2, new List<IEduCommand>
        {
            new MoveCommand(1),
            new TurnCommand("right"),
            new MoveCommand(1),
            new TurnCommand("left")
        }),
        new MoveCommand(10),
        new TurnCommand("right")
    });

    public static EduProgram ExpertProgram => new(new List<IEduCommand>
    {
        new TurnCommand("right"),
        new RepeatCommand(5, new List<IEduCommand>
        {
            new RepeatCommand(2, new List<IEduCommand>
            {
                new TurnCommand("right"),
                new MoveCommand(1)
            }),
            new TurnCommand("left"),
        }),
        new MoveCommand(10),
        new TurnCommand("right")
    });

    public void Run(EduBoard board, ref List<Position> trace)
    {
        trace.Add(board.Position);
        foreach (var command in _commands)
        {
            command.Execute(board, ref trace);
        }
    }

    public override string ToString()
    {
        return BuildBlock(_commands, 0);
    }

    private static string BuildBlock(List<IEduCommand> commands, int indentLevel)
    {
        StringBuilder sb = new();
        var indent = new string(' ', indentLevel * 4);

        foreach (var command in commands)
        {
            if (command is RepeatCommand repeatCommand)
            {
                sb.Append(indent);
                sb.Append($"Repeat {repeatCommand.Times} times");
                sb.Append('\n');
                sb.Append(BuildBlock(repeatCommand.Commands, indentLevel + 1));
            }
            else if (command is RepeatUntilCommand repeatUntilCommand)
            {
                sb.Append(indent);
                sb.Append($"Repeat until {repeatUntilCommand.Condition}");
                sb.Append('\n');
                sb.Append(BuildBlock(repeatUntilCommand.Commands, indentLevel + 1));
            }
            else
            {
                sb.Append(indent);
                sb.Append(command);
                sb.Append('\n');
            }
        }

        return sb.ToString();
    }
}