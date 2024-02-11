using System;
using System.Windows.Input;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prismetro.App.Wpf.Exceptions;
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
    private IDisposable? _closeSub;
    private bool _destroyed;

    public DialogContainerViewModel(
        IDialogCoordinator coordinator,
        ShellWindowResolver shellResolver)
    {
        _coordinator = coordinator;
        _shellResolver = shellResolver;
    }

    public ICommand CloseCommand => _closeCommand ??= new DelegateCommand(() =>
    {
        RequireDestroyed();
        
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
        RequireDestroyed();
        
        _scope = parameter?.GetScope() 
                 ?? throw new ArgumentException($"{nameof(DialogScope)} not passed into parameters. Parameters {parameter}");

        RegisterRequestsHandler();
        
        var navigation = scopeManager.Regions[Regions.DialogContainerRegion].NavigationService;
        navigation.RequestNavigate(region, parameter);
    }

    private void RegisterRequestsHandler()
    {
        _closeSub = _scope.Close.Subscribe(_ =>
        {
            if (Close())
                Destroy();
        });
    }

    private bool Close()
    {
        var canExecute = CloseCommand.CanExecute(new object());
        
        if (canExecute)
            CloseCommand.Execute(new object());

        return canExecute;
    }

    /// <summary>
    /// Разрушить после закрытия диалогового окна (DialogScope.Close)
    /// </summary>
    private void Destroy()
    {
        _scope.Dispose();
        _closeSub?.Dispose();
        
        _destroyed = true;
    }

    private void RequireDestroyed()
    {
        if (_destroyed) throw new DialogContainerDestroyedException();
    }
}