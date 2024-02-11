using Prism.Regions;
using Prismetro.Core.Contracts;
using Prismetro.Core.Models.Scope;

namespace Prismetro.Core.Extensions;

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