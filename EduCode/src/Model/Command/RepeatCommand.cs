﻿using System.Collections;
using EduCode.Model.Board;
using EduCode.Model.Location;

namespace EduCode.Model.Command;

public class RepeatCommand : IEduCommand
{
    private readonly List<IEduCommand> _commands = new();

    public RepeatCommand(int times, List<IEduCommand> commands)
    {
        Commands = commands;
        Times = times;

        for (var i = 0; i < times; i++)
        {
            _commands.AddRange(commands);
        }
    }

    public List<IEduCommand> Commands { get; set; }
    public int Times { get; set; }

    public int MaximumDepth => _commands.Count == 0 ? 0 : _commands.Max(c => c.MaximumDepth) + 1;

    public Position[] Execute(EduBoard board)
    {
        List<Position> positions = new ();
        foreach (var command in _commands)
        {
            positions.AddRange(command.Execute(board));
        }
        return positions.ToArray();
    }

    public override string ToString()
    {
        return _commands.Select(c => c.ToString()).Aggregate((a, b) => $"{a}, {b}") + "";
    }
}