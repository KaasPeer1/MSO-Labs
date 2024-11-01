using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using EduCode.Model.Location;
using EduCode.ViewModel;
using Vector = System.Windows.Vector;

namespace EduCode.View;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        MainViewModel viewModel = new();
        DataContext = viewModel;

        var boardRenderer = new BoardRenderer(viewModel, (int)ImgBoard.Width, (int)ImgBoard.Height);
        ImgBoard.DataContext = boardRenderer;
        boardRenderer.DrawBoard();
    }
}

public class BoardRenderer : INotifyPropertyChanged
{
    private readonly MainViewModel _viewModel;
    private ImageSource? _boardImage;
    private readonly int _width, _height;

    public BoardRenderer(MainViewModel viewModel, int width, int height)
    {
        _viewModel = viewModel;
        _width = width;
        _height = height;

        viewModel.PropertyChanged += (sender, e) =>
        {
            if (e.PropertyName == "Program") return;
            DrawBoard();
        };
    }

    public ImageSource? BoardImage
    {
        get => _boardImage;
        private set => SetField(ref _boardImage, value);
    }

    public void DrawBoard()
    {
        var renderTargetBitmap = new RenderTargetBitmap(_width, _height, 96, 96, PixelFormats.Pbgra32);
        var drawingVisual = new DrawingVisual();

        using (var drawingContext = drawingVisual.RenderOpen())
        {
            DrawBoard(drawingContext);
        }

        renderTargetBitmap.Render(drawingVisual);
        BoardImage = renderTargetBitmap;
    }

    private void DrawBoard(DrawingContext context)
    {
        DrawBorder(context);
        DrawGridLines(context);
        DrawCharacter(context);
    }

    private void DrawBorder(DrawingContext context)
    {
        Pen pen = new(Brushes.Black, 1);
        context.DrawLine(pen, new Point(0,0), new Point(0,_height));
        context.DrawLine(pen, new Point(0,0), new Point(_width,0));
        context.DrawLine(pen, new Point(_width,0), new Point(_width,_height));
        context.DrawLine(pen, new Point(0,_height), new Point(_width,_height));
    }

    private void DrawGridLines(DrawingContext context)
    {
        Pen pen = new(Brushes.Black, 1);
        for (int i = 1; i < _viewModel.Size; i++)
        {
            double x = i * _width / _viewModel.Size;
            double y = i * _height / _viewModel.Size;
            context.DrawLine(pen, new Point(x,0), new Point(x,_height));
            context.DrawLine(pen, new Point(0,y), new Point(_width,y));
        }
    }

    private void DrawCharacter(DrawingContext context)
    {
        Pen pen = new(Brushes.Black, 1);

        int xPos = _viewModel.Position.X * _width / _viewModel.Size + _width / _viewModel.Size / 2;
        int yPos = _viewModel.Position.Y * _height / _viewModel.Size + _height / _viewModel.Size / 2;
        Point center = new(xPos, yPos);
        int xyRadius = _width / _viewModel.Size / 2;

        context.DrawEllipse(Brushes.Red, pen, center, xyRadius, xyRadius);
        context.DrawLine(pen, center, center + GetDirectionVector() * xyRadius);
    }

    private Vector GetDirectionVector()
    {
        return _viewModel.Direction switch
        {
            Direction.North => new Vector(0, -1),
            Direction.East => new Vector(1, 0),
            Direction.South => new Vector(0, 1),
            Direction.West => new Vector(-1, 0),
            _ => throw new InvalidOperationException("Invalid direction.")
        };
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}