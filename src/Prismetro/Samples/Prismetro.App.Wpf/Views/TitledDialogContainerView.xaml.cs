using Prism.Regions;
using Prismetro.Core.Contracts;
using Prismetro.Core.Views;

namespace Prismetro.App.Wpf.Views;

public partial class TitledDialogContainerView
{
    public TitledDialogContainerView(IRegionManager regionManager)
    {
        InitializeComponent();
        CoreViewer.Content = Core = new DialogContainerCore(regionManager);
    }
    
    public override IDialogContainerCore Core { get; }

    public void SetTitle(string title)
    {
        TitleBlock.Text = title;
    }
}