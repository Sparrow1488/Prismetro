using System.Windows;
using MahApps.Metro.Controls.Dialogs;
using Prism.Ioc;
using Prismetro.Core.Contracts;
using Prismetro.Core.Services;

namespace Prismetro.Core.Extensions;

public static class ContainerRegistryExtensions
{
    public static IContainerRegistry AddPrismetro(this IContainerRegistry registry, Func<Window> shellResolver)
    {
        registry.RegisterSingleton<IDialogCoordinator, DialogCoordinator>();
        registry.RegisterSingleton<IDialogServiceAdapter, DialogServiceAdapter>();
        registry.RegisterSingleton<ShellWindowResolver>(_ => new ShellWindowResolver(shellResolver.Invoke()));

        return registry;
    }
}