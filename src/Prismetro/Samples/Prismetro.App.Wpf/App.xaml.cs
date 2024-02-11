using System.Windows;
using MahApps.Metro.Controls.Dialogs;
using Prism.Ioc;
using Prismetro.App.Wpf.ViewModels;
using Prismetro.App.Wpf.Views;
using Prismetro.Core;
using Prismetro.Core.Contracts;
using Prismetro.Core.Extensions;
using Prismetro.Core.Services;

namespace Prismetro.App.Wpf;

public partial class App
{
    protected override void RegisterTypes(IContainerRegistry registry)
    {
        // Register navigation pages
        registry.RegisterForNavigation<GreetingView, GreetingViewModel>(Regions.GreetingRegion);

        registry.AddPrismetro(() => Current.MainWindow!);
    }

    protected override Window CreateShell() => Container.Resolve<MainWindow>();
}