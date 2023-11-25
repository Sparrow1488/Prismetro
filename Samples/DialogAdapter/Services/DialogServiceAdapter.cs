using System;
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
    public const string CancellationKey = "CANCELLATION";
    
    private readonly IDialogCoordinator _coordinator;
    private readonly ShellWindowResolver _shellResolver;
    private readonly IContainerProvider _container;

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

    public async Task ShowDialogAsync(string region, NavigationParameters? parameters, CancellationToken ctk = default)
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

        AppendDefaultParameters(parameters ??= new NavigationParameters(), ctk);
        
        viewModel.NavigateTo(region, parameters, view.RegionManagerScope);
    }

    private static void AppendDefaultParameters(IParameters parameters, CancellationToken ctk)
    {
        parameters.Add(CancellationKey, ctk);
    }
}