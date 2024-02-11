using MahApps.Metro.Controls.Dialogs;
using Prism.Common;
using Prism.Ioc;
using Prism.Regions;
using Prismetro.Core.Contracts;
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
    private readonly List<DialogScope> _dialogs = new();

    public DialogServiceAdapter(
        IDialogCoordinator coordinator, 
        ShellWindowResolver shellResolver,
        IContainerProvider container)
    {
        _coordinator = coordinator;
        _shellResolver = shellResolver;
        _container = container;
    }

    public async Task<DialogScope<TResult>> ShowDialogAsync<TResult>(Navigate<TResult> navigate)
    {
        return (DialogScope<TResult>) await ShowDialogCoreAsync(navigate.Page, navigate.Parameters, CreateDialogScope<TResult>);
    }

    public Task<DialogScope> ShowDialogAsync(Navigate navigate)
    {
        return ShowDialogCoreAsync(navigate.Page, navigate.Parameters, CreateDialogScope);
    }

    private async Task<DialogScope> ShowDialogCoreAsync(string page, NavigationParameters? parameters, Func<DialogScope> scopeCreation)
    {
        if (_shellResolver.Window is null) 
            throw new InvalidOperationException("Shell Window should be resolve");

        using var containerScope = _container.CreateScope();
        
        var viewModel = containerScope.Resolve<DialogContainerViewModel>();
        var view = containerScope.Resolve<DialogContainerView>();

        view.DataContext = viewModel;
        
        await _coordinator.ShowMetroDialogAsync(
            _shellResolver.Window.DataContext, 
            view
        );

        var scope = scopeCreation.Invoke();
        
        AppendDefaultParameters(parameters ??= new NavigationParameters(), scope);
        
        viewModel.NavigateTo(page, parameters, view.RegionManagerScope);

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
}