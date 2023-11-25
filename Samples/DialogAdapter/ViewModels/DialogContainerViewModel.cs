using System.Threading;
using System.Windows.Input;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prismetro.App.Wpf.Services;
using Prismetro.App.Wpf.Views;

namespace Prismetro.App.Wpf.ViewModels;

public class DialogContainerViewModel : BindableBase
{
    private readonly IDialogCoordinator _coordinator;
    private readonly ShellWindowResolver _shellResolver;
    private DelegateCommand? _closeCommand;
    private CancellationToken _cancellation;
    private CancellationTokenRegistration _cancelRegistration;

    public DialogContainerViewModel(
        IDialogCoordinator coordinator,
        ShellWindowResolver shellResolver)
    {
        _coordinator = coordinator;
        _shellResolver = shellResolver;
    }

    public ICommand CloseCommand => _closeCommand ??= new DelegateCommand(() =>
    {
        var shell = _shellResolver.Window!;

        shell.Dispatcher.Invoke(() =>
        {
            var dialog = shell.FindChild<DialogContainerView>();

            _coordinator.HideMetroDialogAsync(
                shell.DataContext,
                dialog
            );
        });
        
        _cancelRegistration.Dispose();
    });
    
    public void NavigateTo(string region, NavigationParameters? parameter, IRegionManager scopeManager)
    {
        parameter?.TryGetValue(DialogServiceAdapter.CancellationKey, out _cancellation);

        if (_cancellation.IsCancellationRequested) 
            Cancel();
        
        _cancelRegistration = _cancellation.Register(Cancel);
        
        var navigation = scopeManager.Regions[Regions.DialogContainerRegion].NavigationService;
        navigation.RequestNavigate(region, parameter);
    }

    private void Cancel()
    {
        if (CloseCommand.CanExecute(new object()))
            CloseCommand.Execute(new object());
    }
}