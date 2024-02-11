using Prism.Regions;

namespace Prismetro.Core.Contracts;

public interface INavigationDialogAware : INavigationAware
{
    void INavigationAware.OnNavigatedFrom(NavigationContext navigationContext) { }
    bool INavigationAware.IsNavigationTarget(NavigationContext navigationContext) => true;
}

public interface INavigationDialogAware<TResult> : INavigationDialogAware
{
    
}