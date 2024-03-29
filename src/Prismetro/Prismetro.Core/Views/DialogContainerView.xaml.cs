using Prism.Regions;
using Prismetro.Core.Contracts;

namespace Prismetro.Core.Views;

public partial class DialogContainerView
{
    public DialogContainerView(IRegionManager regionManager)
    {
        InitializeComponent();
        CoreViewer.Content = Core = new DialogContainerCore(regionManager);
    }

    public override IDialogContainerCore Core { get; }
}