using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using EduCode.Model.Board;
using EduCode.Model.Location;
using EduCode.Model.Program;
using EduCode.ViewModel.Command;
using Microsoft.Win32;

namespace EduCode.ViewModel;

public class MainViewModel : ViewModelBase
{
    private readonly EduBoard _board = new(5, null);
    private EduProgram? _program;
    private string _output = "";
    private string _commandsText = "";
    private Position[]? _trace;

    public MainViewModel()
    {
        _board.PropertyChanged += (sender, e) => OnPropertyChanged(e.PropertyName);
    }

    public Position Position => _board.Position;
    public Direction Direction => _board.Direction;
    public int Size => _board.Size;
    public List<Position> Walls => _board.Walls;
    public Position? EndPosition => _board.EndPosition;

    public EduProgram? Program
    {
        get => _program;
        private set => SetField(ref _program, value);
    }

    public string Output
    {
        get => _output;
        private set => SetField(ref _output, value);
    }

    public string CommandsText
    {
        get => _commandsText;
        private set => SetField(ref _commandsText, value);
    }

    public Position[]? Trace
    {
        get => _trace;
        private set => SetField(ref _trace, value);
    }

    public ICommand SaveProgramCommand => new DelegateCommand(SaveProgram);
    public ICommand LoadProgramCommand => new DelegateCommand(LoadProgram);
    public ICommand LoadProgramFromFileCommand => new DelegateCommand(LoadProgramFromFile);
    public ICommand LoadExerciseFromFileCommand => new DelegateCommand(LoadExerciseFromFile);
    public ICommand RunCommand => new DelegateCommand(RunProgram);
    public ICommand ResetCommand => new DelegateCommand(ResetBoard);
    public ICommand MetricsCommand => new DelegateCommand(OutputMetrics);

    private void SaveProgram(object? o)
    {
        LoadProgramFromText(o);
        if (Program == null)
        {
            MessageBox.Show("No program to save.");
            return;
        }

        var dialog = new SaveFileDialog
        {
            Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
        };

        if (dialog.ShowDialog() == true)
        {
            File.WriteAllText(dialog.FileName, CommandsText);
        }
    }

    private void LoadProgramFromFile(object? o)
    {
        var dialog = new OpenFileDialog
        {
            Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
        };

        if (dialog.ShowDialog() == true)
        {
            LoadProgramFromText(File.ReadAllText(dialog.FileName));
        }
    }

    private void LoadProgram(object? o)
    {
        Program = (o as string) switch
        {
            "basic" => EduProgram.BasicProgram,
            "advanced" => EduProgram.AdvancedProgram,
            "expert" => EduProgram.ExpertProgram,
            _ => ProgramParser.ParseString(o as string ?? "")
        };
        CommandsText = Program?.ToString() ?? "";
    }

    private void RunProgram(object? o)
    {
        LoadProgramFromText(o);

        if (Program == null) return;
        Trace = Program.Run(_board);
        StringBuilder sb = new();
        sb.Append($"Textual trace: {Program.TextualTrace}\nEnd state: {_board}\n");
        if (EndPosition.HasValue) sb.Append(_board.IsInEndPosition() ? "Congratulations! You solved the Exercise." : "Not quite! Try again.");
        Output = sb.ToString();
    }

    private void LoadProgramFromText(object? o)
    {
        try
        {
            Program = ProgramParser.ParseString(o as string ?? "");
        }
        catch (Exception e)
        {
            Program = null;
            Output = $"Error parsing program: {e.Message}";
        }
        CommandsText = Program?.ToString() ?? "";
    }

    private void ResetBoard(object? o)
    {
        _board.Reset();
        Trace = null;
        Output = "";
    }

    private void LoadExerciseFromFile(object? o)
    {
        LoadProgram("");

        var dialog = new OpenFileDialog
        {
            Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
        };

        if (dialog.ShowDialog() == true)
        {
            LoadExercise(File.ReadAllText(dialog.FileName));
        }
    }

    private void LoadExercise(string input)
    {
        GridParser.ParseFromString(input, out var size, out var walls, out var endPosition);
        _board.Reset(size, walls, endPosition);
        ResetBoard(null);
    }

    private void OutputMetrics(object? o)
    {
        if (Program == null) return;
        Output = $"Command count: {Program.CommandCount}\nMaximum command depth: {Program.MaximumDepth}";
    }
}