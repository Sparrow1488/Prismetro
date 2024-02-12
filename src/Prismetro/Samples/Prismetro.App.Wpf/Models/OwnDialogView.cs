using Prismetro.App.Wpf.Views;
using Prismetro.Core.Models.Navigation;
using Prismetro.Core.Views.Custom;

namespace Prismetro.App.Wpf.Models;

public record TitledDialogView : DialogView<LaidDialogContainer>
{
    public TitledDialogView(string title)
    {
        OnShow = x => x.Header = new DialogHeader(title);
    }
}
