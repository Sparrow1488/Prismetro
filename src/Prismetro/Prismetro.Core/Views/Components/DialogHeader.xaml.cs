using System.Windows;

namespace Prismetro.App.Wpf.Views;

public partial class DialogHeader
{
    public DialogHeader(string title, bool hideCloseButton = false) : this()
    {
        Title.Text = title;
        if (hideCloseButton)
            CloseButton.Visibility = Visibility.Collapsed;
    }
    
    public DialogHeader()
    {
        InitializeComponent();
    }
}