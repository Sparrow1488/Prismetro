using System;
using System.Reactive.Subjects;

namespace Prismetro.App.Wpf.Models.Scope;

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

    public void RequestClose() => _closePublisher.OnNext(new object());

    public virtual void Dispose()
    {
        _closePublisher.Dispose();
    }
}