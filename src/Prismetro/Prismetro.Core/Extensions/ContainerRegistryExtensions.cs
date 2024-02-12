using System.Windows;
using MahApps.Metro.Controls.Dialogs;
using Prism.Ioc;
using Prismetro.Core.Contracts;
using Prismetro.Core.Services;
using Prismetro.Core.ViewModels.Contents;
using Prismetro.Core.Views.Contents;

namespace Prismetro.Core.Extensions;

public static class ContainerRegistryExtensions
{
    public static IContainerRegistry AddPrismetro(this IContainerRegistry registry, Func<Window> shellResolver)
    {
        registry.RegisterSingleton<IDialogCoordinator, DialogCoordinator>();
        registry.RegisterSingleton<IDialogServiceAdapter, DialogServiceAdapter>();
        registry.RegisterSingleton<ShellWindowResolver>(_ => new ShellWindowResolver(shellResolver.Invoke()));
        
        registry.RegisterForNavigation<MessageContent, MessageViewModel>(PrismetroRegions.MessageDialogRegion);

        return registry;
    }
}