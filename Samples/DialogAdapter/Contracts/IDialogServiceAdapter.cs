using System.Threading.Tasks;
using Prism.Regions;
using Prismetro.App.Wpf.Models.Scope;

namespace Prismetro.App.Wpf.Contracts;

public interface IDialogServiceAdapter
{
    /// <summary>
    /// Навигация по диалогам через реализацию Prism (с методами NavigatedTo)
    /// </summary>
    Task<DialogScope> ShowDialogAsync(string page, NavigationParameters? parameters);
}