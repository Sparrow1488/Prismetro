using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Regions;
using Prismetro.Core.Contracts;
using Prismetro.Core.Extensions;
using Prismetro.Core.Models.Scope;

namespace Prismetro.App.Wpf.ViewModels;

public sealed class GreetingViewModel : BindableBase, INavigationDialogAware<string>
{
    private string? _name;
    private DialogScope<string> _scope = null!;

    public string? Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }
    
    public async void OnNavigatedTo(NavigationContext context)
    {
        _scope = this.GetScope(context);
        Name = context.Parameters["Name"].ToString();

        await Task.Delay(3000);
        
        if (!_scope.Completed)
            _scope.PushAndCloseResult($"Hello, {Name}!");
    }
}