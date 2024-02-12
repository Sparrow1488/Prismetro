using System.Windows.Input;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prismetro.Core.Exceptions;
using Prismetro.Core.Extensions;
using Prismetro.Core.Models.Scope;
using Prismetro.Core.Services;
using Prismetro.Core.Views;

namespace Prismetro.Core.ViewModels;

public sealed class DialogContainerViewModel<TContainer> : BindableBase
    where TContainer : DialogContainerBase
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

    public ICommand CloseCommand => _closeCommand ??= new DelegateCommand(() => _scope.RequestClose());
    
    public void NavigateTo(string region, NavigationParameters? parameter, IRegionManager scopeManager)
    {
        RequireDestroyed();
        
        _scope = parameter?.GetScope() 
                 ?? throw new ArgumentException($"{nameof(DialogScope)} not passed into parameters. Parameters {parameter}");

        RegisterRequestsHandler();
        
        var navigation = scopeManager.Regions[PrismetroRegions.DialogContainerRegion].NavigationService;
        navigation.RequestNavigate(region, parameter);
    }

    private void RegisterRequestsHandler()
    {
        _closeSub = _scope.Close.Subscribe(_ =>
        {
            if (OnScopeClose())
                Destroy();
        });
    }

    private bool OnScopeClose()
    {
        RequireDestroyed();
        
        var shell = _shellResolver.Window!;

        shell.Dispatcher.Invoke(async () =>
        {
            var dialog = shell.FindChild<TContainer>();

            await _coordinator.HideMetroDialogAsync(
                shell.DataContext,
                dialog
            );
        });

        // TODO: update when validators
        return true;
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