using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Prism.Ioc;
using Prismetro.App.Wpf.Commands;
using Prismetro.App.Wpf.Models;
using Prismetro.Core.Contracts;

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
        var scope = await _dialogService.ShowDialogAsync(new GreetingNavigate("Sparrow"));

        var result = await scope.WaitForResultAsync();
        MessageBox.Show(result);
    }
}