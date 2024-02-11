using Prism.Common;
using Prism.Regions;
using Prismetro.Core.Models.Scope;
using Prismetro.Core.Services;

namespace Prismetro.Core.Extensions;

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