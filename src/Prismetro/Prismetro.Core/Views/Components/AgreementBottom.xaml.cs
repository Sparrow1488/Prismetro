using System.Reactive.Subjects;
using System.Windows;

namespace Prismetro.Core.Views.Components;

public partial class AgreementBottom : IDisposable
{
    private readonly Subject<ButtonResult> _submit;

    public AgreementBottom()
    {
        InitializeComponent();

        _submit = new Subject<ButtonResult>();
    }

    public IObservable<ButtonResult> Submit => _submit;

    private void OnSubmit(object sender, RoutedEventArgs e)
    {
        ButtonResult? result = null;

        if (Equals(sender, AgreeButton))
            result = new AgreeResult();
        if (Equals(sender, CancelButton))
            result = new CancelResult();
        
        _submit.OnNext(result ?? new NullResult());
    }

    public void Dispose()
    {
        _submit.Dispose();
    }
}

public abstract record ButtonResult;

public record NullResult : ButtonResult;

public record AgreeResult : ButtonResult;

public record CancelResult : ButtonResult;