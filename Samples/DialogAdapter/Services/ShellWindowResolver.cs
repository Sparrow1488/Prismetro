using System.Windows;

namespace Prismetro.App.Wpf.Services;

public class ShellWindowResolver
{
    public ShellWindowResolver(Window? window)
    {
        Window = window;
    }
    
    public Window? Window { get; }
}