using System.Threading.Tasks;
using Prismetro.App.Wpf.Models.Navigation;
using Prismetro.App.Wpf.Models.Scope;

namespace Prismetro.App.Wpf.Contracts;

public interface IDialogServiceAdapter
{
    Task<DialogScope<TResult>> ShowDialogAsync<TResult>(Navigate<TResult> navigate);
    Task<DialogScope> ShowDialogAsync(Navigate navigate);
}