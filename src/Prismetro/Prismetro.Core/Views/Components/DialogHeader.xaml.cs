using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Prismetro.Core.Views.Components;

public partial class DialogHeader
{
    public static readonly DependencyProperty CloseCommandProperty = DependencyProperty.Register(
        nameof(CloseCommand),
        typeof(ICommand),
        typeof(DialogHeader)
    );
    
    public DialogHeader(string title, bool hideCloseButton = false, object? closeButtonContent = null) : this()
    {
        Title.Text = title;
        
        if (hideCloseButton)
            CloseButton.Visibility = Visibility.Collapsed;

        if (closeButtonContent is not null)
            SetCloseButton(btn => btn.Content = closeButtonContent);

        DataContext = this;
    }
    
    public DialogHeader()
    {
        InitializeComponent();
    }

    public ICommand? CloseCommand
    {
        get => (ICommand?) GetValue(CloseCommandProperty);
        set => SetValue(CloseCommandProperty, value);
    }
    
    public DialogHeader SetCloseButton(Action<Button> action)
    {
        Dispatcher.Invoke(() => action.Invoke(CloseButton));
        return this;
    }

    public DialogHeader SetCloseButton(Func<Button, Button> action)
    {
        Dispatcher.Invoke(() => CloseButton = action.Invoke(CloseButton));
        return this;
    }
}