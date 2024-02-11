using System;
using System.Threading.Tasks;

namespace Prismetro.App.Wpf.Models.Scope;

public class DialogScope<TResult> : DialogScope
{
    private readonly TaskCompletionSource<TResult> _resultSetSource;

    private Action<TResult>? _pushResult;

    public DialogScope(Guid id) : base(id)
    {
        _resultSetSource = new TaskCompletionSource<TResult>();
        _pushResult += OnPushResult;
    }

    public bool HasValue => Result is not null;
    public TResult? Result { get; private set; }

    public Task<TResult> WaitForResultAsync()
    {
        return _resultSetSource.Task;
    }

    private void OnPushResult(TResult result)
    {
        Result = result;
        _resultSetSource.SetResult(result);
    }
    
    public void PushAndCloseResult(TResult result)
    {
        PushResult(result);
        RequestClose();
    }

    public void PushResult(TResult result)
    {
        _pushResult?.Invoke(result);
    }

    public override void Dispose()
    {
        _pushResult -= OnPushResult;
        base.Dispose();
    }
}