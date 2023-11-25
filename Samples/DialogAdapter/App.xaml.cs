using System.Windows;
using MahApps.Metro.Controls.Dialogs;
using Prism.Ioc;
using Prismetro.App.Wpf.Contracts;
using Prismetro.App.Wpf.Services;
using Prismetro.App.Wpf.ViewModels;
using Prismetro.App.Wpf.Views;

namespace Prismetro.App.Wpf;

public partial class App
{
    protected override void RegisterTypes(IContainerRegistry registry)
    {
        registry.RegisterForNavigation<LoginView, LoginViewModel>(Regions.LoginRegion);
        
        registry.RegisterSingleton<IDialogCoordinator, DialogCoordinator>();
        registry.RegisterSingleton<IDialogServiceAdapter, DialogServiceAdapter>();
        registry.RegisterSingleton<ShellWindowResolver>(_ => new ShellWindowResolver(Current.MainWindow));
    }

    protected override Window CreateShell() => Container.Resolve<MainWindow>();
}