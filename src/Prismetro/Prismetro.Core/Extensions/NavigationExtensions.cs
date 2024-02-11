using Prism.Common;
using Prism.Regions;
using Prismetro.Core.Models.Scope;

namespace Prismetro.Core.Extensions;

public static class NavigationExtensions
{
    private const string DialogScopeKey = "PRISMETRO_DIALOG_SCOPE";
    
    public static DialogScope GetScope(this NavigationContext context)
    {
        return context.Parameters.GetScope();
    }
    
    public static DialogScope GetScope(this IParameters parameters)
    {
        return (DialogScope) parameters[DialogScopeKey];
    }
    
    public static void SetScope(this IParameters parameters, DialogScope scope)
    {
        parameters.Add(DialogScopeKey, scope);
    }
}