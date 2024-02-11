using Prism.Regions;
using Prismetro.App.Wpf.Contracts;
using Prismetro.App.Wpf.Extensions;

namespace Prismetro.App.Wpf.ViewModels;

public class LoginViewModel : INavigationDialogAware
{
    public void OnNavigatedTo(NavigationContext context)
    {
        var scope = context.GetScope();
        scope.RequestClose();
    }
}