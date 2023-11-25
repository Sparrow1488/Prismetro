using System.Threading.Tasks;
using Prism.Regions;
using Prismetro.App.Wpf.Contracts;
using Prismetro.App.Wpf.Services;

namespace Prismetro.App.Wpf.ViewModels;

public class LoginViewModel : INavigationDialogAware
{
    public void OnNavigatedTo(NavigationContext navigationContext)
    {
        // Handle NavigationContext

        var scope = navigationContext.Parameters.GetValue<DialogScope>(DParams.DialogScopeKey);
        Task.Delay(2500).ContinueWith(_ => scope.RequestClose());
    }
}