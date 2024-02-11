using Prism.Regions;
using Prismetro.Core.Models.Navigation;

namespace Prismetro.App.Wpf.Models;

public record GreetingNavigate(string Name) : Navigate<string>(Regions.GreetingRegion, new NavigationParameters
{
    { "Name", Name }
});