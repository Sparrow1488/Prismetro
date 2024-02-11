using Prism.Mvvm;
using Prism.Regions;
using Prismetro.App.Wpf.Contracts;

namespace Prismetro.App.Wpf.ViewModels;

public class GreetingViewModel : BindableBase, INavigationDialogAware
{
    private string? _name;

    public string? Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }
    
    public void OnNavigatedTo(NavigationContext context)
    {
        Name = context.Parameters["Name"].ToString();
    }
}