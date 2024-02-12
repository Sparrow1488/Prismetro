using Prism.Regions;

namespace Prismetro.Core.Extensions;

public static class NavigationParametersExtensions
{
    public static NavigationParameters AddPair(this NavigationParameters parameters, string key, object? value)
    {
        parameters.Add(key, value);
        return parameters;
    }
}