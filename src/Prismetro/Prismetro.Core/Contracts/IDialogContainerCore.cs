using Prism.Regions;

namespace Prismetro.Core.Contracts;

public interface IDialogContainerCore
{
    IRegionManager RegionManagerScope { get; }
}