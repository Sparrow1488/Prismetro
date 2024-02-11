using Prism.Regions;

namespace Prismetro.Core.Views;

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