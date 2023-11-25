using Prism.Regions;

namespace Prismetro.App.Wpf.Views;

public partial class DialogContainerView
{
    public DialogContainerView(IRegionManager regionManager)
    {
        InitializeComponent();

        RegionManagerScope = regionManager.CreateRegionManager();
        RegionManager.SetRegionManager(RegionContentControl, RegionManagerScope);
    }

    public IRegionManager RegionManagerScope { get; }
}