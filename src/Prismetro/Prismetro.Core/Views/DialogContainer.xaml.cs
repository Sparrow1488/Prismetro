using Prism.Regions;
using Prismetro.Core.Contracts;

namespace Prismetro.Core.Views;

public partial class DialogContainer
{
    public DialogContainer(IRegionManager regionManager)
    {
        InitializeComponent();
        CoreViewer.Content = Core = new DialogContainerCore(regionManager);
    }

    public override IDialogContainerCore Core { get; }
}