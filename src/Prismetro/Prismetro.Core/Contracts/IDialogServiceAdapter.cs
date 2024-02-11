using Prismetro.Core.Models.Navigation;
using Prismetro.Core.Models.Scope;

namespace Prismetro.Core.Contracts;

public interface IDialogServiceAdapter
{
    Task<DialogScope<TResult>> ShowDialogAsync<TResult>(Navigate<TResult> navigate);
    Task<DialogScope<TResult>> ShowDialogAsync<TResult, TContainer>(Navigate<TResult> navigate, DialogView<TContainer> view)
        where TContainer : IDialogContainerCoreSupport;
    
    Task<DialogScope> ShowDialogAsync(Navigate navigate);
    Task<DialogScope> ShowDialogAsync<TContainer>(Navigate navigate, DialogView<TContainer> view)
        where TContainer : IDialogContainerCoreSupport;
}