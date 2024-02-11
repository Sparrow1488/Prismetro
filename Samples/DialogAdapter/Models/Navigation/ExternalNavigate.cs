using Prism.Regions;

namespace Prismetro.App.Wpf.Models.Navigation;

public record GreetingNavigate(string Name) : Navigate<string>(Regions.GreetingRegion, new NavigationParameters
{
    { "Name", Name }
});