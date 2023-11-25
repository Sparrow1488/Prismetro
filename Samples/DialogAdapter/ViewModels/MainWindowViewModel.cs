using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Prismetro.App.Wpf.Contracts;

namespace Prismetro.App.Wpf.ViewModels;

public class MainWindowViewModel
{
    private readonly IDialogServiceAdapter _dialogService;
    private DelegateCommand? _navigate;

    public MainWindowViewModel(IDialogServiceAdapter dialogService)
    {
        _dialogService = dialogService;
    }

    public ICommand Navigate => _navigate ??= new DelegateCommand(() =>
    {
        var source = new CancellationTokenSource();
        _dialogService.ShowDialogAsync(Regions.LoginRegion, null, source.Token);

        Task.Delay(2500).ContinueWith(_ => source.Cancel()); // Auto close after delayed
    });
}