using Prism.Regions;

namespace Prismetro.App.Wpf.Contracts;

public interface IDialogServiceAdapter
{
    void ShowDialog(string region, NavigationParameters? parameters);
}