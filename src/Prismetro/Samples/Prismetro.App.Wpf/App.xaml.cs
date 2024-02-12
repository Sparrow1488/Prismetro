using System.Windows;
using Prism.Ioc;
using Prismetro.App.Wpf.Defaults;
using Prismetro.App.Wpf.ViewModels;
using Prismetro.App.Wpf.Views;
using Prismetro.Core.Extensions;

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