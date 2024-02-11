using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Prismetro.App.Wpf.Commands;
using Prismetro.App.Wpf.Contracts;

namespace Prismetro.App.Wpf.ViewModels;

public class MainWindowViewModel
{
    private readonly IDialogServiceAdapter _dialogService;

    public MainWindowViewModel(IDialogServiceAdapter dialogService)
    {
        _dialogService = dialogService;
        NavigateCommand = new AsyncDelegateCommand(Navigate);
    }
    
    public ICommand NavigateCommand { get; }

    private Task Navigate()
    {
        using var source = new CancellationTokenSource();
        return _dialogService.ShowDialogAsync(Regions.LoginRegion, null, source.Token);
    }
}