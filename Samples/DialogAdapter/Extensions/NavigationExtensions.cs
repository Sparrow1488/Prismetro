using Prism.Common;
using Prism.Regions;
using Prismetro.App.Wpf.Models.Scope;
using Prismetro.App.Wpf.Services;

namespace Prismetro.App.Wpf.Extensions;

public static class NavigationExtensions
{
    public static DialogScope GetScope(this NavigationContext context)
    {
        return context.Parameters.GetScope();
    }
    
    public static DialogScope GetScope(this IParameters parameters)
    {
        return (DialogScope) parameters[DParams.DialogScopeKey];
    }
    
    public static void SetScope(this IParameters parameters, DialogScope scope)
    {
        parameters.Add(DParams.DialogScopeKey, scope);
    }
}