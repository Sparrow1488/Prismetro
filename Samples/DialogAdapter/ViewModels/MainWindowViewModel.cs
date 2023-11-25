using System.Windows.Input;
using Prism.Commands;
using Prismetro.App.Wpf.Contracts;
using Prismetro.App.Wpf.Services;

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
        _dialogService.ShowDialog(Regions.LoginRegion, null);
    });
}