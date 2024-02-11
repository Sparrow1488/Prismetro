using System.Windows;
using System.Windows.Media;
using ControlzEx.Theming;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Prism.Common;
using Prism.Ioc;
using Prism.Regions;
using Prismetro.Core.Contracts;
using Prismetro.Core.Exceptions;
using Prismetro.Core.Extensions;
using Prismetro.Core.Models.Navigation;
using Prismetro.Core.Models.Scope;
using Prismetro.Core.ViewModels;
using Prismetro.Core.Views;

namespace Prismetro.Core.Services;

public class DialogServiceAdapter : IDialogServiceAdapter
{
    private readonly IDialogCoordinator _coordinator;
    private readonly ShellWindowResolver _shellResolver;
    private readonly IContainerProvider _container;
    private readonly List<DialogScope> _dialogs = new(); // TODO: очищать после Dispose/Close

    public DialogServiceAdapter(
        IDialogCoordinator coordinator, 
        ShellWindowResolver shellResolver,
        IContainerProvider container)
    {
        _coordinator = coordinator;
        _shellResolver = shellResolver;
        _container = container;
    }

    private static DialogView<DialogContainerView> DefaultDialogView => new();

    public Task<DialogScope<TResult>> ShowDialogAsync<TResult>(Navigate<TResult> navigate)
    {
        return ShowDialogAsync(navigate, DefaultDialogView);
    }

    public async Task<DialogScope<TResult>> ShowDialogAsync<TResult, TContainer>(
        Navigate<TResult> navigate, 
        DialogView<TContainer> view
    ) where TContainer : IDialogContainerCoreSupport
    {
        return (DialogScope<TResult>) await ShowDialogCoreAsync(navigate, CreateDialogScope<TResult>, view);
    }

    public Task<DialogScope> ShowDialogAsync(Navigate navigate)
    {
        return ShowDialogAsync(navigate, DefaultDialogView);
    }

    public Task<DialogScope> ShowDialogAsync<TContainer>(
        Navigate navigate, 
        DialogView<TContainer> view
    ) where TContainer : IDialogContainerCoreSupport
    {
        return ShowDialogCoreAsync(navigate, CreateDialogScope, view);
    }

    private async Task<DialogScope> ShowDialogCoreAsync<TContainer>(
        Navigate navigate, 
        Func<DialogScope> scopeCreation, 
        DialogView<TContainer> dialogView
    ) where TContainer : IDialogContainerCoreSupport
    {
        if (_shellResolver.Window is null) 
            throw new InvalidOperationException("Shell Window should be resolve");

        using var containerScope = _container.CreateScope();
        
        var viewModel = containerScope.Resolve<DialogContainerViewModel>();
        var view = containerScope.Resolve<TContainer>();

        if (view is FrameworkElement element and BaseMetroDialog dialog)
        {
            element.DataContext = viewModel;
            
            ApplyDialogView((MetroWindow) _shellResolver.Window!, dialogView.WindowDarkModeOverlayBrush);
        
            await _coordinator.ShowMetroDialogAsync(
                _shellResolver.Window.DataContext, 
                dialog
            );
        }
        else
        {
            throw new DialogContainerException($"Passed {nameof(DialogView<TContainer>)} is not assignable to {nameof(IDialogContainerCoreSupport)} and {nameof(BaseMetroDialog)}");
        }

        var scope = scopeCreation.Invoke();
        var parameters = navigate.Parameters ?? new NavigationParameters();
        
        AppendDefaultParameters(parameters, scope);
        
        viewModel.NavigateTo(navigate.Page, parameters, view.Core.RegionManagerScope);

        return scope;
    }

    private static void AppendDefaultParameters(IParameters parameters, DialogScope scope)
    {
        parameters.SetScope(scope);
    }
    
    private DialogScope<TResult> CreateDialogScope<TResult>() 
        => (DialogScope<TResult>) RegisterDialogScope(new DialogScope<TResult>(Guid.NewGuid()));

    private DialogScope CreateDialogScope() 
        => RegisterDialogScope(new DialogScope(Guid.NewGuid()));

    private DialogScope RegisterDialogScope(DialogScope scope)
    {
        _dialogs.Add(scope);
        return scope;
    }
    
    private static void ApplyDialogView(MetroWindow window, Brush? darkModeOverlay)
    {
        var currentTheme = ThemeManager.Current.DetectTheme();
        if (darkModeOverlay != null && (currentTheme?.Name.Contains("Dark") ?? false))
        {
            window.OverlayBrush = darkModeOverlay;
        }
    }
}