using Prismetro.Core.Exceptions;

namespace Prismetro.Core.Models.Scope;

public class DialogScope<TResult> : DialogScope
{
    private readonly TaskCompletionSource<TResult?> _resultSetSource;

    private Action<TResult>? _pushResult;

    public DialogScope(Guid id) : base(id)
    {
        _resultSetSource = new TaskCompletionSource<TResult?>();
        _pushResult += OnPushResult;
    }

    public bool HasValue => Result is not null;
    public TResult? Result { get; private set; }
    public bool Completed => _resultSetSource.Task.IsCompleted;

    public Task<TResult?> WaitForResultAsync()
    {
        return _resultSetSource.Task;
    }

    private void OnPushResult(TResult result)
    {
        if (Completed) throw new DialogResultAlreadySetException();
        
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
        if (Disposed) throw new DialogScopeDisposedException();
        
        _pushResult?.Invoke(result);
    }

    public override void Dispose()
    {
        if (Disposed) return;
        
        if (!Completed)
            _resultSetSource.SetResult(Result);
        
        _pushResult -= OnPushResult;
        base.Dispose();
    }
}