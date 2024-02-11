using System.Windows;

namespace Prismetro.Core.Services;

public class ShellWindowResolver
{
    public ShellWindowResolver(Window? window)
    {
        Window = window;
    }
    
    public Window? Window { get; }
}