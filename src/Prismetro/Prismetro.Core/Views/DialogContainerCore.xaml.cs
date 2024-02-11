using Prism.Regions;
using Prismetro.Core.Contracts;

namespace Prismetro.Core.Views;

public partial class DialogContainerCore : IDialogContainerCore
{
    public DialogContainerCore(IRegionManager regionManager)
    {
        InitializeComponent();
        
        RegionManagerScope = regionManager.CreateRegionManager();
        RegionManager.SetRegionManager(RegionContentControl, RegionManagerScope);
    }
    
    public IRegionManager RegionManagerScope { get; }
}