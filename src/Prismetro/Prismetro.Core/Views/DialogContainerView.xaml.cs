using Prism.Regions;
using Prismetro.Core.Contracts;

namespace Prismetro.Core.Views;

public partial class DialogContainerView : IDialogContainerCoreSupport
{
    public DialogContainerView(IRegionManager regionManager)
    {
        InitializeComponent();
        Core = new DialogContainerCore(regionManager);

        CoreViewer.Content = Core;
    }

    public IDialogContainerCore Core { get; }
}