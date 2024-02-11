using System;
using System.Threading.Tasks;

namespace Prismetro.App.Wpf.Models.Scope;

public class DialogScope<TResult> : DialogScope
{
    public DialogScope(Guid id) : base(id)
    {
    }

    public Task<TResult?> WaitForResultAsync()
    {
        return Task.FromResult((TResult) default);
    }
}