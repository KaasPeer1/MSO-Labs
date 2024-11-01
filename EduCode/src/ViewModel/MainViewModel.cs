using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using EduCode.Model.Board;
using EduCode.Model.Location;
using EduCode.Model.Program;
using EduCode.ViewModel.Command;

namespace EduCode.ViewModel;

public class MainViewModel : ViewModelBase
{
    private readonly EduBoard _board = new(5);
    private EduProgram? _program;
    private string _output = "";
    private string _commandsText = "";

    public MainViewModel()
    {
        _board.PropertyChanged += (sender, e) => OnPropertyChanged(e.PropertyName);
    }

    public Position Position => _board.Position;
    public Direction Direction => _board.Direction;
    public int Size => _board.Size;

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

    public ICommand LoadCommand => new DelegateCommand(LoadProgram);
    public ICommand RunCommand => new DelegateCommand(RunProgram);
    public ICommand ResetCommand => new DelegateCommand(ResetBoard);
    public ICommand MetricsCommand => new DelegateCommand(OutputMetrics);

    private void LoadProgram(object? o)
    {
        Program = (o as string) switch
        {
            "basic" => EduProgram.BasicProgram,
            "advanced" => EduProgram.AdvancedProgram,
            "expert" => EduProgram.ExpertProgram,
            _ => throw new ArgumentException("Can't load this program.")
        };
        CommandsText = Program.ToString();
    }

    private void RunProgram(object? o)
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

        if (Program == null) return;
        Program.Run(_board);
        Output = $"Textual trace: {Program.TextualTrace}\nEnd state: {_board}";
    }

    private void ResetBoard(object? o)
    {
        _board.Reset();
        Output = "";
    }

    private void OutputMetrics(object? o)
    {
        if (Program == null) return;
        Output = $"Command count: {Program.CommandCount}\nMaximum command depth: {Program.MaximumDepth}";
    }
}