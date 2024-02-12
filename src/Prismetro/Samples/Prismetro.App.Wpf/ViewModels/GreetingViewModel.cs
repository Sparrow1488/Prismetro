using System.Windows.Controls;
using System.Windows.Input;
using Prism.Commands;
using Prism.Regions;
using Prismetro.App.Wpf.Validation;
using Prismetro.Core.Contracts;
using Prismetro.Core.Extensions;
using Prismetro.Core.Models.Scope;

namespace Prismetro.App.Wpf.ViewModels;

public sealed class GreetingViewModel : ValidationViewModel, INavigationDialogAware<string>
{
    private string? _name;
    private DialogScope<string> _scope = null!;
    private ICommand? _send;
    private string? _sendText;

    public string? Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    public string? Text
    {
        get => _sendText;
        set => SetProperty(ref _sendText, value);
    }

    public ICommand SendCommand => _send ??= new DelegateCommand<string>(
            OnSend,
            _ => this[nameof(Text)] == string.Empty
        ).ObservesProperty(() => Text);

    public void OnNavigatedTo(NavigationContext context)
    {
        _scope = this.GetScope(context);
        
        Name = context.Parameters[NavigateKeys.Greeting.Name].ToString();
        if (context.Parameters.TryGetValue<ValidationRule>(NavigateKeys.Greeting.TextValidation, out var validator))
            AddValidator(nameof(Text), () => Text, validator);
    }
    
    private void OnSend(string text)
    {
        if (!_scope.Completed)
            _scope.PushAndCloseResult(Text!);
    }
}