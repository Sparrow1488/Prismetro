using System.Threading;
using System.Threading.Tasks;
using Prism.Regions;

namespace Prismetro.App.Wpf.Contracts;

public interface IDialogServiceAdapter
{
    void ShowDialog(string region, NavigationParameters? parameters);
    Task ShowDialogAsync(string region, NavigationParameters? parameters, CancellationToken ctk = default);
}