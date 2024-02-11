using Prism.Regions;

namespace Prismetro.App.Wpf.Contracts;

public interface INavigationDialogAware : INavigationAware
{
    void INavigationAware.OnNavigatedFrom(NavigationContext navigationContext) { }
    bool INavigationAware.IsNavigationTarget(NavigationContext navigationContext) => true;
}