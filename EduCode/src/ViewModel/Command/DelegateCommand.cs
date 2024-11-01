using System.Windows.Input;

namespace EduCode.ViewModel.Command;

public class DelegateCommand : ICommand
{
    private readonly Action<object?> _execute;
    private readonly Predicate<object?>? _canExecute;

    public event EventHandler? CanExecuteChanged;

    public DelegateCommand(Action<object?> execute) : this(execute, null)
    {
    }

    public DelegateCommand(Action<object?> execute, Predicate<object?>? canExecute)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    public bool CanExecute(object? parameter)
    {
        return _canExecute == null || _canExecute(parameter);
    }

    public void Execute(object? parameter)
    {
        _execute(parameter);
    }
}