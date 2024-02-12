using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Prismetro.Module.Keyboards.Views;

public abstract class Keyboard : UserControl
{
    public static readonly DependencyProperty SubmitCommandProperty = DependencyProperty.Register(
        nameof(SubmitCommand),
        typeof(ICommand),
        typeof(Keyboard)
    );
    
    public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
        nameof(Value),
        typeof(string),
        typeof(Keyboard)
    );

    public ICommand? SubmitCommand
    {
        get => (ICommand?) GetValue(SubmitCommandProperty);
        set => SetValue(SubmitCommandProperty, value);
    }
    
    public string Value
    {
        get => (string?) GetValue(ValueProperty) ?? string.Empty;
        set => SetValue(ValueProperty, value);
    }
}