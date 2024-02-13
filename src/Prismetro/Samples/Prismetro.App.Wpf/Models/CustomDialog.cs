using MahApps.Metro.IconPacks;
using Prism.Commands;
using Prismetro.Core.Models.Navigation;
using Prismetro.Core.Views;
using Prismetro.Core.Views.Components;

namespace Prismetro.App.Wpf.Models;

public record CustomDialog : DialogView<LaidDialogContainer>
{
    public CustomDialog(string title, bool hideCloseButton = false)
    {
        OnShow = (container, scope) =>
        {
            container.Header = new DefaultHeader(
                title,
                hideCloseButton,
                new PackIconMicrons {Kind = PackIconMicronsKind.Hide}
            )
            {
                CloseCommand = new DelegateCommand(scope.RequestClose)
            };
        };
    }
}
