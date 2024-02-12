using System;
using System.Windows;
using System.Windows.Controls;

namespace Prismetro.Module.Keyboards.Views;

public partial class TestKeyboard
{
    public TestKeyboard()
    {
        InitializeComponent();
        
        // TODO: нужно как то пробросить Value binding на TextBox
        // TODO: TextBox/RichTextBox лучше выделить в отдельный control
    }

    private void OnTextInput(object sender, TextChangedEventArgs e)
    {
        if (sender is TextBox textBox)
            Value = textBox.Text;
        else throw new Exception();
    }

    private void SubmitValue(object sender, RoutedEventArgs e)
    {
        SubmitCommand?.Execute(Value);
    }
}