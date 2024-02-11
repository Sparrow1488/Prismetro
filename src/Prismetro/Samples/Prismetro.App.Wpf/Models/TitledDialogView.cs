using Prismetro.App.Wpf.Views;
using Prismetro.Core.Models.Navigation;

namespace Prismetro.App.Wpf.Models;

public record TitledDialogView : DialogView<TitledDialogContainerView>
{
    public TitledDialogView(string title)
    {
        OnShow = x => x.SetTitle(title);
    }
}
