using System;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using Prism.Regions;

namespace Prismetro.App.Wpf.Contracts;

public interface IDialogServiceAdapter
{
    void ShowDialog(string region, NavigationParameters? parameters);
    Task<DialogScope> ShowDialogAsync(string region, NavigationParameters? parameters, CancellationToken ctk = default);
}

public class DialogScope
{
    private readonly Subject<object?> _subject;

    public DialogScope(Guid id)
    {
        Id = id;
        _subject = new Subject<object?>();
    }
    
    public Guid Id { get; }
    public IObservable<object?> CloseMessages => _subject;

    public void RequestClose() => _subject.OnNext(new object());
}