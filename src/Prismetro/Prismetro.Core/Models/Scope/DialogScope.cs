using System.Reactive.Subjects;

namespace Prismetro.Core.Models.Scope;

public class DialogScope : IDisposable
{
    private readonly Subject<object?> _closePublisher;

    public DialogScope(Guid id)
    {
        Id = id;
        _closePublisher = new Subject<object?>();
    }

    public Guid Id { get; }
    public IObservable<object?> Close => _closePublisher;
    protected bool Disposed { get; set; }

    public void RequestClose() => _closePublisher.OnNext(new object());

    public virtual void Dispose()
    {
        if (Disposed) return;
        
        _closePublisher.Dispose();
        Disposed = true;
    }
}