using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MahApps.Metro.Controls.Dialogs;
using Prism.Common;
using Prism.Ioc;
using Prism.Regions;
using Prismetro.App.Wpf.Contracts;
using Prismetro.App.Wpf.Extensions;
using Prismetro.App.Wpf.Models.Scope;
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
    
    public async Task<DialogScope> ShowDialogAsync(string page, NavigationParameters? parameters)
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
        
        AppendDefaultParameters(parameters ??= new NavigationParameters(), scope);
        
        viewModel.NavigateTo(page, parameters, view.RegionManagerScope);

        return scope;
    }

    private static void AppendDefaultParameters(IParameters parameters, DialogScope scope)
    {
        parameters.SetScope(scope);
    }

    private DialogScope ProvideDialog()
    {
        var dialog = new DialogScope(Guid.NewGuid());
        _dialogs.Add(dialog);
        
        return dialog;
    }
}