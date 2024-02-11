using System;
using System.Reactive.Subjects;

namespace Prismetro.App.Wpf.Models.Scope;

public class DialogScope
{
    private readonly Subject<object?> _subject;

    public DialogScope(Guid id)
    {
        Id = id;
        _subject = new Subject<object?>();
    }

    public Guid Id { get; }
    public IObservable<object?> Close => _subject;

    public void RequestClose() => _subject.OnNext(new object());
}