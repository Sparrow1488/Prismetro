using System.Windows.Controls;
using Prism.Regions;
using Prismetro.App.Wpf.Defaults;
using Prismetro.Core.Extensions;
using Prismetro.Core.Models.Navigation;

namespace Prismetro.App.Wpf.Models;

public record GreetingNavigate(string Name, ValidationRule? SendTextValidation = null) 
    : Navigate<string>(
        Regions.GreetingRegion,
        new NavigationParameters()
            .AddPair(NavigateKeys.Greeting.Name, Name)
            .AddPair(NavigateKeys.Greeting.TextValidation, SendTextValidation)
    );