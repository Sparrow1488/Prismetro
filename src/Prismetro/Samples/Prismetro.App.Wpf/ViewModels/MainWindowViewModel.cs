using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Prismetro.App.Wpf.Commands;
using Prismetro.App.Wpf.Models;
using Prismetro.App.Wpf.Validation;
using Prismetro.Core.Contracts;
using Prismetro.Core.Extensions;

namespace Prismetro.App.Wpf.ViewModels;

// ReSharper disable file MemberCanBePrivate.Global
// ReSharper disable file UnusedAutoPropertyAccessor.Global

public class MainWindowViewModel
{
    private readonly IDialogServiceAdapter _dialogService;

    public MainWindowViewModel(IDialogServiceAdapter dialogService)
    {
        _dialogService = dialogService;
        NavigateCommand = new AsyncDelegateCommand(NavigateAsync);
    }
    
    public ICommand NavigateCommand { get; }

    private Task NavigateAsync()
    {
        return ShowMessageAsync();
    }

    private async Task ShowMessageAsync()
    {
        var result = await (await _dialogService.ShowMessageAsync("Добрый день!", "Сообщение")).WaitForResultAsync();
    }

    private async Task ShowGreetingDialogAsync()
    {
        using var scope = await _dialogService.ShowDialogAsync(
            new GreetingNavigate("Sparrow", new SendValidationRule()), 
            new CustomDialog("Greeting Dialog")
        );

        var result = await scope.WaitForResultAsync();
        
        if (!string.IsNullOrWhiteSpace(result))
            MessageBox.Show(result);
    }
}