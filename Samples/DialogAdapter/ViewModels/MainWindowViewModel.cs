using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Prismetro.App.Wpf.Commands;
using Prismetro.App.Wpf.Contracts;
using Prismetro.App.Wpf.Models.Navigation;

namespace Prismetro.App.Wpf.ViewModels;

public class MainWindowViewModel
{
    private readonly IDialogServiceAdapter _dialogService;

    public MainWindowViewModel(IDialogServiceAdapter dialogService)
    {
        _dialogService = dialogService;
        NavigateCommand = new AsyncDelegateCommand(NavigateAsync);
    }
    
    public ICommand NavigateCommand { get; }

    private async Task NavigateAsync()
    {
        using var source = new CancellationTokenSource();
        using var scope = await _dialogService.ShowDialogAsync(new GreetingNavigate("Sparrow"));

        var result = await scope.WaitForResultAsync();
        MessageBox.Show(result);
    }
}