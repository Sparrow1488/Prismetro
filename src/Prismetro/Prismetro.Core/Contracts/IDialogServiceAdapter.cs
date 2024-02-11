using Prismetro.Core.Models.Navigation;
using Prismetro.Core.Models.Scope;

namespace Prismetro.Core.Contracts;

public interface IDialogServiceAdapter
{
    Task<DialogScope<TResult>> ShowDialogAsync<TResult>(Navigate<TResult> navigate);
    Task<DialogScope> ShowDialogAsync(Navigate navigate);
}