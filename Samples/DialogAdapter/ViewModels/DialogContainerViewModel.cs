using System;
using System.Windows.Input;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prismetro.App.Wpf.Extensions;
using Prismetro.App.Wpf.Models.Scope;
using Prismetro.App.Wpf.Services;
using Prismetro.App.Wpf.Views;

namespace Prismetro.App.Wpf.ViewModels;

public class DialogContainerViewModel : BindableBase
{
    private readonly IDialogCoordinator _coordinator;
    private readonly ShellWindowResolver _shellResolver;
    private DelegateCommand? _closeCommand;
    private DialogScope _scope = null!;

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

        shell.Dispatcher.Invoke(async () =>
        {
            var dialog = shell.FindChild<DialogContainerView>();

            await _coordinator.HideMetroDialogAsync(
                shell.DataContext,
                dialog
            );
        });
    });
    
    public void NavigateTo(string region, NavigationParameters? parameter, IRegionManager scopeManager)
    {
        _scope = parameter?.GetScope() 
                 ?? throw new ArgumentException("DialogScope not passed into parameters. Parameters " + parameter);

        RegisterRequestsHandler();
        
        var navigation = scopeManager.Regions[Regions.DialogContainerRegion].NavigationService;
        navigation.RequestNavigate(region, parameter);
    }

    private void RegisterRequestsHandler()
    {
        _scope.Close.Subscribe(_ => Close());
    }

    private void Close()
    {
        if (CloseCommand.CanExecute(new object()))
            CloseCommand.Execute(new object());
    }
}