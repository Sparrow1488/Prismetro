using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Prismetro.App.Wpf.Commands;

// MARK: наброски на скорую руку

public class AsyncDelegateCommand : ICommand
{
    private readonly Func<Task> _executeMethod;
    private readonly Func<bool> _canExecuteMethod;
    public event EventHandler? CanExecuteChanged;
    
    public AsyncDelegateCommand(Func<Task> executeMethod) : this(executeMethod, () => true)
    {
    }

    public AsyncDelegateCommand(Func<Task> executeMethod, Func<bool> canExecuteMethod)
    {
        _executeMethod = executeMethod;
        _canExecuteMethod = canExecuteMethod;
    }

    public bool CanExecute(object? parameter)
    {
        return _canExecuteMethod.Invoke();
    }

    public async void Execute(object? parameter)
    {
        await _executeMethod.Invoke();
    }
}

public class AsyncDelegateCommand<T> : ICommand
{
    private readonly Func<T, Task> _executeMethod;
    private readonly Func<T, bool> _canExecuteMethod;
    public event EventHandler? CanExecuteChanged;
    
    public AsyncDelegateCommand(Func<T, Task> executeMethod) : this(executeMethod, _ => true)
    {
    }

    public AsyncDelegateCommand(Func<T, Task> executeMethod, Func<T, bool> canExecuteMethod)
    {
        _executeMethod = executeMethod;
        _canExecuteMethod = canExecuteMethod;
    }

    public bool CanExecute(object? parameter)
    {
        return _canExecuteMethod.Invoke((T) parameter!);
    }

    public async void Execute(object? parameter)
    {
        await _executeMethod.Invoke((T) parameter!);
    }
}