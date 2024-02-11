using Prism.Regions;
using Prismetro.App.Wpf.Contracts;
using Prismetro.App.Wpf.Models.Scope;

namespace Prismetro.App.Wpf.Extensions;

public static class NavigationDialogAwareExtensions
{
    public static DialogScope GetScope(this INavigationDialogAware _, NavigationContext context)
    {
        return context.GetScope();
    }
    
    public static DialogScope<T> GetScope<T>(this INavigationDialogAware<T> _, NavigationContext context)
    {
        return (DialogScope<T>) context.GetScope();
    }
}