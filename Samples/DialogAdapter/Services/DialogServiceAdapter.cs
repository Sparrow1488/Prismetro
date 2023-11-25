using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MahApps.Metro.Controls.Dialogs;
using Prism.Common;
using Prism.Ioc;
using Prism.Regions;
using Prismetro.App.Wpf.Contracts;
using Prismetro.App.Wpf.ViewModels;
using Prismetro.App.Wpf.Views;

namespace Prismetro.App.Wpf.Services;

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
    
    public void ShowDialog(string region, NavigationParameters? parameters)
    {
        ShowDialogAsync(region, parameters, CancellationToken.None).ConfigureAwait(false);
    }

    public async Task<DialogScope> ShowDialogAsync(string region, NavigationParameters? parameters, CancellationToken ctk = default)
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

        var scope = ProvideDialog();
        
        AppendDefaultParameters(parameters ??= new NavigationParameters(), scope, ctk);
        
        viewModel.NavigateTo(region, parameters, view.RegionManagerScope);

        return scope;
    }

    private static void AppendDefaultParameters(IParameters parameters, DialogScope scope, CancellationToken ctk)
    {
        parameters.Add(DParams.DialogScopeKey, scope);
        parameters.Add(DParams.CancellationKey, ctk);
    }

    private DialogScope ProvideDialog()
    {
        var dialog = new DialogScope(Guid.NewGuid());
        _dialogs.Add(dialog);
        
        return dialog;
    }
}