using Prism.Regions;

namespace Prismetro.Core.Models.Navigation;

public record Navigate(string Page, NavigationParameters? Parameters);

public record Navigate<TResult>(
    string Page, 
    NavigationParameters? Parameters 
) : Navigate(Page, Parameters);
