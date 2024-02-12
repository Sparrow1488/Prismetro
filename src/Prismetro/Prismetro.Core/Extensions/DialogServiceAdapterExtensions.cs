using Prismetro.Core.Contracts;
using Prismetro.Core.Models.Navigation;
using Prismetro.Core.Models.Scope;
using Prismetro.Core.Views.Components;

namespace Prismetro.Core.Extensions;

public static class DialogServiceAdapterExtensions
{
    public static Task<DialogScope<ButtonResult>> ShowMessageAsync(this IDialogServiceAdapter service, string text)
    {
        return service.ShowMessageAsync(text, string.Empty);
    }
    
    public static Task<DialogScope<ButtonResult>> ShowMessageAsync(this IDialogServiceAdapter service, string text, string title)
    {
        return service.ShowDialogAsync(
            new MessageDialogNavigate(text),
            new MessageDialogView(title)
        );
    }
}