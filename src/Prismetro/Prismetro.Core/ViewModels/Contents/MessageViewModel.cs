using Prism.Mvvm;
using Prism.Regions;
using Prismetro.Core.Contracts;
using Prismetro.Core.Defaults;
using Prismetro.Core.Views.Components;

namespace Prismetro.Core.ViewModels.Contents;

public class MessageViewModel : BindableBase, INavigationDialogAware<ButtonResult>
{
    private string? _text;

    public string Text
    {
        get => _text ?? string.Empty;
        set => SetProperty(ref _text, value);
    }
    
    public void OnNavigatedTo(NavigationContext context)
    {
        Text = (string) context.Parameters[NavigateKeys.Message.Text];
    }
}